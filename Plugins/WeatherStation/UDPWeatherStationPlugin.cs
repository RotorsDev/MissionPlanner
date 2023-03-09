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
        //private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private int PORT;
        private IPEndPoint iPEndPoint;
        private UdpClient udpClient;
        private TimeSpan connectionTimeout;
        private DateTime lastWeatherUpdate;

        public class WeatherItem
        {
            public string Name { get; set; }
            public double Value { get; set; }
            public int Index { get; set; }
            public double Offset { get; set; }
            public double Multiplier { get; set; }
            public string Unit { get; set; }
            public bool Visible { get; set; }

            public WeatherItem(string name, double value, int index, double offset, double multiplier, string unit, bool visible)
            {
                Name = name;
                Value = value;
                Index = index;
                Offset = offset;
                Multiplier = multiplier;
                Unit = unit;
                Visible = visible;
            }
            public WeatherItem(string name, int index, string unit, bool visible) : this(name, 0, index, 0, 1, unit, visible) {}
            public WeatherItem(string name, int index, string unit) : this(name, index, unit, true) {}
        }
        public List<WeatherItem> tableConfig; // make it public so other plugins can access the data
        private Image imageOriginal;
        private bool neverConnected;

        private string ConfigSaveKey = "RACWeatherStationTuningValues";
        private string ConfigVersionKey = "RACWeatherStationVersion";
        private int ConfigVersion = 2;
        internal class TuningItem
        {
            public string Name { get; set; }
            public double Offset { get; set; }
            public double Multiplier { get; set; }
            public bool Visible { get; set; }
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
            get { return "0.3.0"; }
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

                // Setup disconnected message label
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
                tableConfig = new List<WeatherItem>
                {
                    new WeatherItem("Station heading", 0, "°", false) {},
                    new WeatherItem("Wind speed", 1, CurrentState.SpeedUnit) {},
                    new WeatherItem("Wind direction", 2, "°") {},
                    new WeatherItem("Air pressure", 3, "mBar") {},
                    new WeatherItem("Internal temperature", 4, "°C", false) {},
                    new WeatherItem("Humidity", 5, "%") {},
                    new WeatherItem("External temperature", 6, "°C") {},
                    new WeatherItem("Battery voltage", 7, "V") {}
                };

                // Value multipliers
                if (Settings.Instance.ContainsKey(ConfigSaveKey) // There are values
                    &&
                    Settings.Instance.ContainsKey(ConfigVersionKey) // There is a version number
                    &&
                    int.Parse(Settings.Instance[ConfigVersionKey]) == ConfigVersion) // Version number is correct
                {
                    // Load saved tuning values
                    List<TuningItem> saved = Settings.Instance[ConfigSaveKey].FromJSON<List<TuningItem>>();
                    foreach (TuningItem savedItem in saved)
                    {
                        WeatherItem tableItem = tableConfig.Where(tc => tc.Name == savedItem.Name).FirstOrDefault();
                        tableItem.Offset = savedItem.Offset;
                        tableItem.Multiplier = savedItem.Multiplier;
                        tableItem.Visible = savedItem.Visible;
                    }
                }
                // Save values and version number
                    List<TuningItem> toSave = new List<TuningItem>();
                    foreach (var tableItem in tableConfig)
                    {
                        toSave.Add(new TuningItem()
                        {
                            Name = tableItem.Name,
                            Offset = tableItem.Offset,
                            Multiplier = tableItem.Multiplier,
                            Visible = tableItem.Visible
                        });
                    }
                    Settings.Instance[ConfigSaveKey] = toSave.ToJSON();
                    Settings.Instance[ConfigVersionKey] = "" + ConfigVersion;
                    Settings.Instance.Save();

                // Create control
                weatherTable = new TableLayoutPanel();
                weatherTable.Name = "tableWeather";
                weatherTable.ColumnCount = 2;
                weatherTable.RowCount = tableConfig.Count(x => x.Visible) + 1; // +1 row for time

                // Add time row
                AddRow("Time", string.Empty);

                // Add other rows
                foreach (WeatherItem item in tableConfig)
                {
                    if (item.Visible)
                    {
                        AddRow(item.Name, item.Unit);
                    }
                }

                // Format control
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

        /// <summary>
        /// Populates the next empty row in weatherTable
        /// </summary>
        /// <param name="name">Name to be displayed</param>
        /// <param name="unit">Unit to be displayed</param>
        private void AddRow(string name, string unit)
        {
            // Find next empty row
            int i = 0;
            while (i < weatherTable.RowCount && weatherTable.GetControlFromPosition(0, i) != null)
                i++;

            // Column 0: Name
            Label l1 = new Label();
            l1.Text = name;
            l1.TextAlign = ContentAlignment.MiddleRight;
            l1.Padding = new Padding(5);
            l1.AutoSize = true;
            l1.Dock = DockStyle.Fill;
            weatherTable.Controls.Add(l1, 0, i);

            // Column 1: Value and unit
            Label l2 = new Label();
            l2.Text = $"{Math.Round(0d, 2)} {unit}";
            l2.TextAlign = ContentAlignment.MiddleLeft;
            l2.Padding = new Padding(5);
            l2.AutoSize = true;
            l2.Dock = DockStyle.Fill;
            weatherTable.Controls.Add(l2, 1, i);
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
		// Loop is called in regular intervals (set by loopratehz)
        {
            if (DateTime.Now.Subtract(lastWeatherUpdate) > connectionTimeout)
                DisplayDisconnectedMessage();
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

            // Log message
            Console.WriteLine($"UDP broadcast on port {PORT}: {message}");
            //LogMessage(message);

            // Update tableConfig values
            UpdateData(message);

            // Update UI
            UpdateUI();
        }

        /// <summary>
        /// Log received UDP broadcast message
        /// </summary>
        /// <param name="message">Received UDP broadcast message</param>
        //private void LogMessage(string message)
        //{
        //    // Log in csv format
        //    string logLine = $"\"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}\"";
        //    foreach (string item in message.Split('|'))
        //        logLine += $",\"{item}\"";
        //    log.Info(logLine);
        //}

        /// <summary>
        /// Update the values in the weather data table
        /// </summary>
        /// <param name="message">UDP message string</param>
        private void UpdateData(string message)
        {
            //MainV2.instance.BeginInvoke((MethodInvoker)(() => // We have to access CurrentState, so...
            //{
            // message -> tableConfig
            message = message.Replace('.', (0.1).ToString()[1])
                             .Replace(',', (0.1).ToString()[1]); // suck it invariant culture!

            foreach (WeatherItem item in tableConfig)
            {
                item.Value = double.Parse(message.Split('|')[item.Index]); // parse value from message

                MainV2.instance.BeginInvoke((MethodInvoker)(() => // TODO: can we get away with this?
                {
                    if (item.Name.Contains("speed") && item.Unit != CurrentState.SpeedUnit) // Windspeed is a special case
                    {
                        item.Value *= CurrentState.multiplierspeed; // Apply set unit multiplier
                        item.Unit = CurrentState.SpeedUnit; // Apply set unit text
                    }

                    item.Value *= item.Multiplier; // Apply multiplier
                    item.Value += item.Offset; // Apply offset
		    
		    if (item.Name.Contains("heading") || item.Name.Contains("direction")) // Constrain degrees to 0-360
                    {
                        if (item.Value < 0)
                            item.Value += 360;
                        else if (item.Value >= 360)
                            item.Value -= 360;
                    }
		    
                    // Write values for protar
                    switch (item.Name)
                    {
                        case "Wind speed":
                            Host.cs.g_wind_vel = (float)item.Value;
                            break;
                        case "Wind direction":
                            Host.cs.g_wind_dir = (float)item.Value;
                            break;
                        case "Air pressure":
                            Host.cs.g_press = (float)item.Value;
                            break;
                        case "Humidity":
                            Host.cs.g_humidity = (float)item.Value;
                            break;
                        case "External temperature":
                            Host.cs.g_temp = (float)item.Value;
                            break;
                        default:
                            break;
                    }
                }));
            }
            //}));
        }

        /// <summary>
        /// Update the UI with the current values in the weather data table
        /// </summary>
        private void UpdateUI()
        {
            // tableConfig -> weatherTable
            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                // Update time row
                weatherTable.GetControlFromPosition(1, 0).Text = $"{DateTime.Now.ToShortDateString()} {DateTime.Now.ToLongTimeString()}";

                // Update other rows
                for (int i = 1; i < weatherTable.RowCount; i++) // Skip 1st row (time)
                {
                    WeatherItem item = tableConfig.Where(x => x.Name == weatherTable.GetControlFromPosition(0, i).Text).FirstOrDefault();
                    weatherTable.GetControlFromPosition(1, i).Text = $"{item.Value} {item.Unit}";
                }

                // Update wind arrow
                Bitmap rotatedBmp = new Bitmap(imageOriginal.Width, imageOriginal.Height);
                rotatedBmp.SetResolution(imageOriginal.HorizontalResolution, imageOriginal.VerticalResolution);
                Graphics graphics = Graphics.FromImage(rotatedBmp);
                graphics.TranslateTransform(rotatedBmp.Width / 2, rotatedBmp.Height / 2);
                float newAngle = (float)tableConfig.Where(x => x.Name == "Wind direction").FirstOrDefault().Value; // Arrow points where the wind is blowing from
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
        /// Display message on UI when connection to the weather station has been lost for the set time
        /// </summary>
        private void DisplayDisconnectedMessage()
        {
            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                // Update disconnected message label
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
