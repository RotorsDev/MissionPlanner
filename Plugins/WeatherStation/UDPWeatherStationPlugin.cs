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
using System.Windows.Forms.VisualStyles;
using System.Security.Policy;
using MissionPlanner.Attributes;
using System.Security.Cryptography;
using System.Runtime.InteropServices.WindowsRuntime;
using static MissionPlanner.Utilities.LTM;
using System.ComponentModel;

namespace UDPWeatherStation
{
    public class UDPWeatherStationPlugin : Plugin
    {

        public class MovingAverage
        {
            private Queue<Decimal> samples = new Queue<Decimal>();
            private int windowSize = 30;
            private Decimal sampleAccumulator;
            public Decimal Average { get; private set; }

            /// <summary>
            /// Computes a new windowed average each time a new sample arrives
            /// </summary>
            /// <param name="newSample"></param>
            public void ComputeAverage(Decimal newSample)
            {
                if (newSample == 0) newSample = 1;
                sampleAccumulator += newSample;
                samples.Enqueue(newSample);

                if (samples.Count > windowSize)
                {
                    sampleAccumulator -= samples.Dequeue();
                }

                Average = sampleAccumulator / samples.Count;
            }
        }

        private int PORT;
        private IPEndPoint iPEndPoint;
        private UdpClient udpClient;
        private TimeSpan connectionTimeout;
        private DateTime lastWeatherUpdate;
        

        [AttributeUsage(AttributeTargets.All)]
        public class WeatherDataAttribute : Attribute
        {
            // Private fields.
            private string name;
            private string unit;
            private int position;
            private bool visible;
            private double multiplier;
            private double offset;

            // This constructor defines two required parameters: name and level.

            public WeatherDataAttribute(string name, string unit, int position, bool visibility = true)
            {
                this.name = name;
                this.unit = unit;
                this.position = position;
                this.visible = visibility;
            }

            // Define Name property.
            // This is a read-only attribute.

            public virtual string Name
            {
                get { return name; }
            }

            // Define Level property.
            // This is a read-only attribute.

            public virtual string Unit
            {
                get { return unit; }
            }

            // Define Reviewed property.
            // This is a read/write attribute.

            public virtual int Position
            {
                get { return position; }
            }
            public virtual bool Visible
            {
                get { return visible; }
                set { visible = value; }
            }

        }

        //Contains the last received data packet from the WeatherStation
        public class WeatherStationData
        {

            private double ws;
            private MovingAverage diravg = new MovingAverage();

            [ReadOnly(false)]
            [WeatherDataAttribute("WindSpeed", "m/s", 1)]
            public double windSpeed
            {
                get
                {
                    return ws * windspeedMultiplier + windspeedOffset;
                }
                set
                {
                    ws = value;
                }
            }
            [ReadOnly(false)]
            [WeatherDataAttribute("WindDir", "deg", 2)]
            public double windDirection
            {
                get
                {
                    //return (double)diravg.Average; 
                    return (double)Math.Round(diravg.Average);
                }
                set
                {
                    diravg.ComputeAverage((decimal)(value + winddirOffset));
                }
            }
            [ReadOnly(false)]
            [WeatherDataAttribute("QFE", "mbar", 3)]
            public double QFE { get; set; }
            [ReadOnly(false)]
            [WeatherDataAttribute("Temp", "C", 6)]
            public double extTemp { get; set; }
            [ReadOnly(false)]
            [WeatherDataAttribute("Humidity", "%", 5)]
            public double humidity { get; set; }
            [ReadOnly(false)]
            [WeatherDataAttribute("Internal Temp", "C", 4,false)]
            public double intTemp { get; set; }
            [ReadOnly(false)]
            [WeatherDataAttribute("Battery", "Volts", 7)]
            public double batteryVoltage { get; set; }
            [ReadOnly(false)]
            [WeatherDataAttribute("Heading", "deg", 0,false)]
            public double stationHeading { get; set; }
            [ReadOnly(false)]
            [WeatherDataAttribute("Impulses", "", 8,false)]
            public double speedImpulses { get; set; }

            public double windspeedOffset = 0;
            public double windspeedMultiplier = 1.0;
            public double winddirOffset = 0;


            public WeatherStationData() { }

            public int getFieldPosition(string name)
            {
                int retval = -1;
                try
                {
                    var typeofthing = typeof(WeatherStationData).GetProperty(name);
                    if (typeofthing != null)
                    {
                        var attrib = typeofthing.GetCustomAttributes(false).OfType<WeatherDataAttribute>().ToArray();
                        if (attrib.Length > 0)
                            retval = attrib.OfType<WeatherDataAttribute>().First().Position;
                    }
                }
                catch
                {
                }
                return retval;
            }

            public string getFieldHumanName(string name)
            {
                string retval = "";
                try
                {
                    var typeofthing = typeof(WeatherStationData).GetProperty(name);
                    if (typeofthing != null)
                    {
                        var attrib = typeofthing.GetCustomAttributes(false).OfType<WeatherDataAttribute>().ToArray();
                        if (attrib.Length > 0)
                            retval = attrib.OfType<WeatherDataAttribute>().First().Name;
                    }
                }
                catch
                {
                }
                return retval;

            }

            public string getFieldUnit(string name)
            {
                string retval = "";
                try
                {
                    var typeofthing = typeof(WeatherStationData).GetProperty(name);
                    if (typeofthing != null)
                    {
                        var attrib = typeofthing.GetCustomAttributes(false).OfType<WeatherDataAttribute>().ToArray();
                        if (attrib.Length > 0)
                            retval = attrib.OfType<WeatherDataAttribute>().First().Unit;
                    }
                }
                catch
                {
                }
                return retval;
            }

            public bool getFieldVisibility(string name)
            {
                bool retval = false;
                try
                {
                    var typeofthing = typeof(WeatherStationData).GetProperty(name);
                    if (typeofthing != null)
                    {
                        var attrib = typeofthing.GetCustomAttributes(false).OfType<WeatherDataAttribute>().ToArray();
                        if (attrib.Length > 0)
                            retval = attrib.OfType<WeatherDataAttribute>().First().Visible;
                    }
                }
                catch
                {
                }
                return retval;
            }



            // Format // heading|windSpd|windDir|pressure|intTemp|extHum|extTemp|battery|impules/min
            public void updateData(string UDPString)
            {
                UDPString = UDPString.Replace('.', (0.1).ToString()[1])
                                     .Replace(',', (0.1).ToString()[1]); // suck it invariant culture!

                var fields = UDPString.Split('|');
                Type test = this.GetType();
                foreach (var item in test.GetProperties())
                {
                    var pos = this.getFieldPosition(item.Name);
                    var value = fields[pos];
                    item.SetValue(this, value.ConvertToDouble());
                }

            }

            public int getPropertyCount()
            {
                Type test = this.GetType();
                return test.GetProperties().Length;
            }

            public List<(string, string)> getDisplay()
            {
                List<(string, string)> retval = new List<(string,string)>();

                Type test = this.GetType();
                foreach (var item in test.GetProperties())
                {
                    var n = this.getFieldHumanName(item.Name);
                    var u = this.getFieldUnit(item.Name);
                    bool vis = this.getFieldVisibility(item.Name);  
                    var v = item.GetValue(this);
                    if (vis) retval.Add(($"{n}", $"{v.ToString()} {u}"));
                }
                return retval;
            }

            public List<string> getDataNames()
            {
                List<string> retval = new List<string>();   
                Type test = this.GetType();
                foreach (var item in test.GetProperties())
                {
                    retval.Add(item.Name);
                }
                return retval;
            }
        }

        public WeatherStationData wd = new WeatherStationData();    
        private Image imageOriginal;
        private bool neverConnected;
        private Label disconnectedLabel;
        private PictureBox pBoxArrow;
        private TableLayoutPanel weatherTable;
        private FlowLayoutPanel flowPanel;
        private TabPage tabPage;

        private string wsOffsetKeyName = "WEATHER_WindspeedOffset";
        private string wsMultiplierKeyName = "WEATHER_WindspeedMultiplier";
        private string wdOffsetKeyName = "WEATHER_WinddirOffset";


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
                disconnectedLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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

                if (Settings.Instance.ContainsKey(wdOffsetKeyName))
                {
                    wd.winddirOffset = Settings.Instance.GetDouble(wdOffsetKeyName, 0);
                }
                else
                {
                    Settings.Instance[wdOffsetKeyName] = wd.winddirOffset.ToString();
                }

                if (Settings.Instance.ContainsKey(wsOffsetKeyName))
                {
                    wd.windspeedOffset = Settings.Instance.GetDouble(wsOffsetKeyName, 0);
                }
                else
                {
                    Settings.Instance[wsOffsetKeyName] = wd.windspeedOffset.ToString();
                }

                if (Settings.Instance.ContainsKey(wsMultiplierKeyName))
                {
                    wd.windspeedMultiplier = Settings.Instance.GetDouble(wsMultiplierKeyName, 0);
                }
                else
                {
                    Settings.Instance[wsMultiplierKeyName] = wd.windspeedMultiplier.ToString();
                }


                // Create control
                weatherTable = new TableLayoutPanel();
                weatherTable.Name = "tableWeather";
                weatherTable.ColumnCount = 2;

                List<(string, string)> data = wd.getDisplay();
                foreach (var i in data)
                {
                        AddRow(i.Item1, i.Item2);
                }

                // Format control
                weatherTable.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
                weatherTable.Width = flowPanel.ClientSize.Width;
                for (int i = 0; i < weatherTable.ColumnCount; i++)
                    weatherTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f / weatherTable.ColumnCount));
                weatherTable.Height = (weatherTable.RowCount+1) * ((new Label()).Height + 1) + 5;
                weatherTable.Visible = true;
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
            weatherTable.RowCount = weatherTable.RowCount + 1;
            // Column 0: Name
            Label l1 = new Label();
            l1.Text = name;
            l1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            l1.Padding = new Padding(5);
            l1.AutoSize = true;
            l1.Dock = DockStyle.Fill;
            l1.Visible = true;
            weatherTable.Controls.Add(l1, 0, weatherTable.RowCount-1);

            // Column 1: Value and unit
            Label l2 = new Label();
            l2.Text = $"{Math.Round(0d, 2)} {unit}";
            l2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            l2.Padding = new Padding(5);
            l2.AutoSize = true;
            l2.Dock = DockStyle.Fill;
            l2.Visible = true;
            weatherTable.Controls.Add(l2, 1, weatherTable.RowCount-1);
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
            //UpdateData(message);

            wd.updateData(message);
            UpdateCurrentControlsSet();
            // Update UI
            UpdateUI();
        }

        private void UpdateCurrentControlsSet()
        {
            Host.cs.g_wind_vel = (float)wd.windSpeed;
            Host.cs.g_wind_dir = (float)wd.windDirection;
            Host.cs.g_press = (float)wd.QFE;
            Host.cs.g_humidity = (float)wd.humidity;
            Host.cs.g_temp = (float)wd.extTemp;

        }
        /// <summary>
        /// Update the UI with the current values in the weather data table
        /// </summary>
        private void UpdateUI()
        {
            // tableConfig -> weatherTable
            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                List<(string, string)> data = wd.getDisplay();
                for (int i = 0; i < data.Count; i++) 
                {
                        weatherTable.GetControlFromPosition(1, i).Text = $"{data[i].Item2}";
                }

                // Update wind arrow
                Bitmap rotatedBmp = new Bitmap(imageOriginal.Width, imageOriginal.Height);
                rotatedBmp.SetResolution(imageOriginal.HorizontalResolution, imageOriginal.VerticalResolution);
                Graphics graphics = Graphics.FromImage(rotatedBmp);
                graphics.TranslateTransform(rotatedBmp.Width / 2, rotatedBmp.Height / 2);
                //float newAngle = ((float)wd.windDirection + 180) % 360; // Arrow points where the wind is blowing to
                graphics.RotateTransform((float)wd.windDirection);
                graphics.TranslateTransform(-(rotatedBmp.Width / 2), -(rotatedBmp.Height / 2));
                graphics.DrawImage(imageOriginal, new PointF(0, 0));
                pBoxArrow.Image = rotatedBmp;

                // Change control visibility & refresh
                disconnectedLabel.Hide();
                pBoxArrow.Show();
                weatherTable.Show();
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
                    //weatherTable.Hide();
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
