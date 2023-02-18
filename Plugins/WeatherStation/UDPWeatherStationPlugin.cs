using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using MissionPlanner.Controls.PreFlight;
using MissionPlanner.Controls;
using System.Linq;
using System.Drawing;
using MissionPlanner;
using MissionPlanner.Plugin;
using System.Globalization;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Reflection;

namespace UDPWeatherStation
{
    public class UDPWeatherStationPlugin : Plugin
    {

        private int PORT;
        private IPEndPoint iPEndPoint;
        private UdpClient udpClient;
        private TimeSpan connectionTimeout;
        private DateTime lastWeatherUpdate;

        private List<(string Name, int Index, double Offset, double Multiplier, string Unit)> tableConfig;
        private Image imageOriginal;
        private bool neverConnected;

        private string TuningConfigKey = "RACWeatherStationTuningValues";
        internal class TuningItem
        {
            public string Name { get; set; }
            public double Offset { get; set; }
            public double Multiplier { get; set; }
        }

        private Label disconnectedLabel;
        private PictureBox pBoxArrow;
        private TableLayoutPanel weatherTable;
        private FlowLayoutPanel flowPanel;
        private TabPage tabPage;

        #region Plugin info
        public override string Name
        {
            get { return "UDP Weather Station"; }
        }

        public override string Version
        {
            get { return "0.1"; }
        }

        public override string Author
        {
            get { return "Daniel Szilagyi"; }
        }
        #endregion

        //[DebuggerHidden]
        public override bool Init()
		// Init called when the plugin dll is loaded
        {
            loopratehz = 1;  // Loop runs every second (The value is in Hertz, so 2 means every 500ms, 0.1f means every 10 second...)
            return true;	 // If it is false then plugin will not load
        }

        public override bool Loaded()
        // Loaded called after the plugin dll successfully loaded
        {
            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                // Setup tabpage
                tabPage = new TabPage();
                tabPage.Name = "tabWeather";
                tabPage.Text = "Weather";
                int index = 1;
                List<string> list = Host.config.GetList("tabcontrolactions").ToList();
                list.Insert(index, "tabWeather");
                Host.config.SetList("tabcontrolactions", list);
                Host.MainForm.FlightData.TabListOriginal.Insert(index, tabPage);
                Host.MainForm.FlightData.tabControlactions.TabPages.Insert(index, tabPage);

                // Setup flow panel
                flowPanel = new FlowLayoutPanel();
                flowPanel.Name = "flowPanelWeather";
                flowPanel.FlowDirection = FlowDirection.TopDown;
                flowPanel.AutoSize = true;
                flowPanel.Dock = DockStyle.Fill;
                flowPanel.Resize += FlowPanel_Resize;
                tabPage.Controls.Add(flowPanel);

                // Setup disonnected message label
                disconnectedLabel = new Label();
                disconnectedLabel.Name = "labelDisconnected";
                disconnectedLabel.Text = "disconnected";
                disconnectedLabel.TextAlign = ContentAlignment.MiddleCenter;
                disconnectedLabel.Width = flowPanel.ClientSize.Width;
                disconnectedLabel.Padding = new Padding(5);
                flowPanel.Controls.Add(disconnectedLabel);

                // Setup wind arrow picture
                pBoxArrow = new PictureBox();
                pBoxArrow.Name = "pictureBoxArrow";
                pBoxArrow.Width = flowPanel.ClientSize.Width;
                pBoxArrow.Height = 128;
                pBoxArrow.SizeMode = PictureBoxSizeMode.Zoom;
                pBoxArrow.Image = UDPWeatherStation.Properties.Resources.arrow;
                imageOriginal = pBoxArrow.Image; // Save 0 rotation image
                flowPanel.Controls.Add(pBoxArrow);

                // Setup data table
                // állomás iránya (nem kell) | szélsebesség 'm/s' | szélirány '°' | légnyomás 'miliBar' | belső hőmérséklet (nem kell) | páratartalom '%' | hőmérséklet '°C' | akksifesz 'Volt' |
                tableConfig = new List<(string, int, double, double, string)>
                {
                    //("Station heading", 0, 0.0, 1.0, "°"),
                    ("Wind speed", 1, 0.0, 1.0, CurrentState.SpeedUnit),
                    ("Wind direction", 2, 0.0, 1.0, "°"),
                    ("Air pressure", 3, 0.0, 1.0, "mBar"),
                    //("Internal temperature", 4, 0.0, 1.0, "°C"),
                    ("Humidity", 5, 0.0, 1.0, "%"),
                    ("External temperature", 6, 0.0, 1.0, "°C"),
                    ("Battery voltage", 7, 0.0, 1.0, "V")
                };

                // Value multipliers
                if (Settings.Instance.ContainsKey(TuningConfigKey))
                {
                    // Load saved tuning values
                    List<TuningItem> saved = Settings.Instance[TuningConfigKey].FromJSON<List<TuningItem>>();
                    foreach (TuningItem savedItem in saved)
                    {
                        (string Name, int Index, double Offset, double Multiplier, string Unit) tableItem = tableConfig.Where(tc => tc.Name == savedItem.Name).FirstOrDefault();
                        tableItem.Offset = savedItem.Offset;
                        tableItem.Multiplier = savedItem.Multiplier;
                    }
                }
                else
                {
                    // Save current multipliers
                    List<TuningItem> toSave = new List<TuningItem>();
                    foreach (var tableItem in tableConfig)
                    {
                        toSave.Add(new TuningItem()
                        {
                            Name = tableItem.Name,
                            Offset = tableItem.Offset,
                            Multiplier = tableItem.Multiplier
                        });
                    }
                    Settings.Instance[TuningConfigKey] = toSave.ToJSON();
                    Settings.Instance.Save();
                }

                // Create control
                weatherTable = new TableLayoutPanel();
                weatherTable.Name = "tableWeather";
                weatherTable.ColumnCount = 2;
                weatherTable.RowCount = tableConfig.Count + 1; // +1 row for time
                for (int i = 0; i < weatherTable.RowCount; i++)
                {
                    // Column 0: Name
                    Label l1 = new Label();
                    l1.Text = i == 0 ? "Time" : tableConfig[i - 1].Name;
                    l1.TextAlign = ContentAlignment.MiddleRight;
                    l1.Padding = new Padding(5);
                    l1.AutoSize = true;
                    l1.Dock = DockStyle.Fill;
                    weatherTable.Controls.Add(l1, 0, i);

                    // Column 1: Value and unit
                    Label l2 = new Label();
                    l2.Text = i == 0 ? string.Empty : $"{Math.Round(0d, 2)} {tableConfig[i - 1].Unit}";
                    l2.TextAlign = ContentAlignment.MiddleLeft;
                    l2.Padding = new Padding(5);
                    l2.AutoSize = true;
                    l2.Dock = DockStyle.Fill;
                    weatherTable.Controls.Add(l2, 1, i);
                }
                weatherTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                weatherTable.Width = flowPanel.ClientSize.Width;
                for (int i = 0; i < weatherTable.ColumnCount; i++)
                    weatherTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / weatherTable.ColumnCount));
                weatherTable.Height = weatherTable.RowCount * ((new Label()).Height + 1) + 1;
                flowPanel.Controls.Add(weatherTable);

                // Refresh UI
                ThemeManager.ApplyThemeTo(tabPage);
                flowPanel.Refresh();

                // Setup UDP broadcast listener
                PORT = 3333;
                iPEndPoint = new IPEndPoint(IPAddress.Any, PORT);
                udpClient = new UdpClient(PORT);
                udpClient.BeginReceive(new AsyncCallback(ProcessMessage), null);
                connectionTimeout = TimeSpan.FromSeconds(2);
                neverConnected = true;
            }));

            return true;     //If it is false plugin will not start (loop will not called)
        }

        private void FlowPanel_Resize(object sender, EventArgs e)
        {
            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                // Adjust control widths in flowPanel
                foreach (Control item in flowPanel.Controls)
                    item.Width = flowPanel.ClientSize.Width;
                flowPanel.Refresh();
            }));
        }

        public override bool Loop()
		// Loop is called in regular intervalls (set by loopratehz)
        {
            if (DateTime.Now.Subtract(lastWeatherUpdate) > connectionTimeout)
                DisplayDisconnectedMessage();
            // állomás iránya (nem kell) | szélsebesség 'm/s' | szélirány '°' | légnyomás 'miliBar' | belső hőmérséklet (nem kell) | páratartalom '%' | hőmérséklet '°C' | akksifesz 'Volt' |
            //String message = "100 | 50 | 200 | 1234 | 15 | 45 | 5 | 11.2 |";
            //DisplayMessage(message);

            return true;	//Return value is not used
        }

        /// <summary>
        /// UDP Client callback function
        /// </summary>
        private void ProcessMessage(IAsyncResult result)
        {
            // Get message
            string message = Encoding.UTF8.GetString(udpClient.EndReceive(result, ref iPEndPoint));

            // Restart listener
            udpClient.BeginReceive(new AsyncCallback(ProcessMessage), null);

            // Save update time
            lastWeatherUpdate = DateTime.Now;

            // Flip bool for first message
            neverConnected = false;

            // Process message
            Console.WriteLine($"UDP broadcast on port {PORT}: {message}");
            LogMessage(message);
            DisplayMessage(message);
        }

        /// <summary>
        /// Log received UDP broadcast message
        /// </summary>
        /// <param name="message">Received UDP broadcast message</param>
        private void LogMessage(string message)
        {
            // Log in csv format
            //string logLine = $"\"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}\"";
            //foreach (string item in message.Split('|'))
            //    logLine += $",\"{item}\"";
            //log.Info(logLine);
        }

        /// <summary>
        /// Put received UDP broadcast message to the UI
        /// </summary>
        /// <param name="message">Received UDP broadcast message</param>
        private void DisplayMessage(string message)
        {
            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                message = message
                .Replace('.', (0.1).ToString()[1])
                .Replace(',', (0.1).ToString()[1]); // suck it invariant culture!

                // Update table data
                for (int i = 0; i < weatherTable.RowCount; i++)
                {
                    string content;
                    if (i == 0) // Time
                    {
                        content = $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}";
                    }
                    else if (tableConfig[i - 1].Name.Contains("speed")) // Wind speed
                    {
                        content = $"{Math.Round(double.Parse(message.Split('|')[tableConfig[i - 1].Index]) * CurrentState.multiplierspeed * tableConfig[i - 1].Multiplier + tableConfig[i - 1].Offset, 2)} {CurrentState.SpeedUnit}";
                    }
                    else // Everything else
                    {
                        content = $"{Math.Round(double.Parse(message.Split('|')[tableConfig[i - 1].Index]) * tableConfig[i - 1].Multiplier + tableConfig[i - 1].Offset, 2)} {tableConfig[i - 1].Unit}";
                    }
                    weatherTable.GetControlFromPosition(1, i).Text = content;
                }


                for (int i = 1; i< weatherTable.RowCount; i++)
                {
                    double value = Math.Round(double.Parse(message.Split('|')[tableConfig[i - 1].Index]) * tableConfig[i - 1].Multiplier + tableConfig[i - 1].Offset, 2);
                    switch (tableConfig[i - 1].Name)
                    {
                        case "Wind speed":
                            Host.cs.g_wind_vel = (float)value;
                            break;
                        case "Wind direction":
                            Host.cs.g_wind_dir = (float)value;
                            break;
                        case "Air pressure":
                            Host.cs.g_press = (float)value;
                            break;
                        case "Humidity":
                            Host.cs.g_humidity = (float)value;
                            break;
                        case "External temperature":
                            Host.cs.g_temp = (float)value;
                            break;
                        default:
                            break;
                    }
                }

                // Update wind arrow
                Bitmap rotatedBmp = new Bitmap(imageOriginal.Width, imageOriginal.Height);
                rotatedBmp.SetResolution(imageOriginal.HorizontalResolution, imageOriginal.VerticalResolution);
                Graphics graphics = Graphics.FromImage(rotatedBmp);
                graphics.TranslateTransform(rotatedBmp.Width / 2, rotatedBmp.Height / 2);
                int index = tableConfig.Where(item => item.Name.Contains("Wind direction")).FirstOrDefault().Index;
                float newAngle = (float.Parse(message.Split('|')[index]) + 180) % 360; // Arrow points where the wind is blowing to
                graphics.RotateTransform(newAngle);
                graphics.TranslateTransform(-(rotatedBmp.Width / 2), -(rotatedBmp.Height / 2));
                graphics.DrawImage(imageOriginal, new PointF(0, 0));
                pBoxArrow.Image = rotatedBmp;

                // Change control visibility & refresh
                disconnectedLabel.Hide();
                pBoxArrow.Show();
                weatherTable.Show();
                flowPanel.Refresh();
            }));
        }

        /// <summary>
        /// Dsiplay message on UI when connection to the weather station has been lost for the set time
        /// </summary>
        private void DisplayDisconnectedMessage()
        {
            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                // Update disnonnected message label
                string msg = "Weather station disconnected";
                if (neverConnected) // Hide arrow and table if no initial data
                {
                    pBoxArrow.Hide();
                    weatherTable.Hide();
                }
                else msg += $" ({(int)DateTime.Now.Subtract(lastWeatherUpdate).TotalSeconds}s ago)";
                disconnectedLabel.Text = msg;

                // Change control visibility & refresh
                disconnectedLabel.Show();
                //pBoxArrow.Hide();
                //weatherTable.Hide();
                flowPanel.Refresh();
            }));
        }

        public override bool Exit()
		// Exit called when plugin is terminated (usually when Mission Planner is exiting)
        {
            return true;	// Return value is not used
        }
    }
}
