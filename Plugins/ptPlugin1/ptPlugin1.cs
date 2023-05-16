using MissionPlanner;
using MissionPlanner.Plugin;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Drawing;
using MissionPlanner.Controls.PreFlight;
using MissionPlanner.Controls;
using System.Linq;
using FastColoredTextBoxNS;
using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using MissionPlanner.Maps;
using System.Text;
using System.Media;
using ClipperLib;
using System.Net.Sockets;
using System.Net;
using static MissionPlanner.Utilities.LTM;




//By Bandi
namespace ptPlugin1
{
    public enum LandState
    {
        None,
        Ready,
        GoToWaiting,
        WaitForSpeed,
        WaitForTangent,
        GoToLand,
        CloseToLand,
        Land
    }

    public enum ChuteState
    {
        AutoOpenDisabled,
        AutoOpenEnabled
    }

    public class situation
    {
        public int sysid1;
        public PointLatLngAlt pos1;
        public float heading1;
        public float airspeed1;
        public int sysid2;
        public PointLatLngAlt pos2;
        public float heading2;
        public float airspeed2;
        public int sysid3;
        public PointLatLngAlt pos3;
        public float heading3;
        public float airspeed3;

    }

    [PreventTheming]
    public partial class ptPlugin1 : Plugin
    {

        public situation sit = new situation();
        
        public static int plane1ID = 0;
        public static int plane2ID = 0;    
        public static int plane3ID = 0;

        public static string plane1Name = "";
        public static string plane2Name = "";
        public static string plane3Name = "";

        public static bool slot1Enabled = false;
        public static bool slot2Enabled = false;
        public static bool slot3Enabled = false;

        annunciator aMain1 = new annunciator(20, new Size(130, 40));
        annunciator aMain2 = new annunciator(20, new Size(130, 40));
        annunciator aMain3 = new annunciator(20, new Size(130, 40));

        public bool SetupDone = false;

        public static FloatingForm annunciatorForm = new FloatingForm();



        public TabPage payloadControlpage = new TabPage();
        public payloadcontrol plControl = new payloadcontrol();
        public List<Payload> payloadSettings = new List<Payload>();
        payloadSetupForm fPs;



        public TabPage engineControlPage = new TabPage();
        public engineControl eCtrl = new engineControl();


        public TabPage colorMessagePage = new TabPage();
        public FastColoredTextBox fctb;
        public DateTime lastDisplayedMessage1 = DateTime.MinValue;
        public DateTime lastDisplayedMessage2 = DateTime.MinValue;
        public DateTime lastDisplayedMessage3 = DateTime.MinValue;



        TextStyle infoStyle = new TextStyle(Brushes.White, null, FontStyle.Regular);
        TextStyle warningStyle = new TextStyle(Brushes.BurlyWood, null, FontStyle.Regular);
        TextStyle errorStyle = new TextStyle(Brushes.Red, null, FontStyle.Regular);


        public TabPage connectionControlPage = new TabPage();
        public ConnectionStats connectionStats;

        public TabPage batteryPage = new TabPage();
        public batterypanel bp = new batterypanel();


        public TabPage ekfPage = new TabPage();
        public ekfStatControl ekfStat = new ekfStatControl();


        public TabPage pitotPage = new TabPage();
        public pitotControl pitot = new pitotControl();

        public TabPage fuelPage = new TabPage();
        public fuelControl fuel = new fuelControl();

        public TabPage landingPage = new TabPage();
        public landingControl lc = new landingControl();


        public TabPage overviewPage = new TabPage();
        public TableLayoutPanel tlOw = new TableLayoutPanel();   
        public Dictionary<string, Label> oWlabels = new Dictionary<string, Label>();  

        public ToolStripMenuItem tsLandingPoint = new ToolStripMenuItem();
        public ToolStripMenuItem tsStartSim = new ToolStripMenuItem();
        public ToolStripMenuItem tsSetupFleet = new ToolStripMenuItem();
        public ToolStripMenuItem tsDoAutoConnect = new ToolStripMenuItem();

        

        string actualPanel = "";

        public int chuteServo = 9;
        public int chuteServoOpenPWM = 1100;


        internal GMapMarker markerLanding;
        internal GMapMarker markerWaiting;
        internal GMapMarker markerTarget;

        internal GMapRoute landingRoute;
        internal static GMapOverlay landingOverlay;

        public LandState landState = LandState.None;



        DateTime lastNonCriticalUpdate = DateTime.MinValue;


        public override string Name
        {
            get { return "ptPlugin1"; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }

        public override string Author
        {
            get { return "Schaffer Andras"; }
        }

        //[DebuggerHidden]
        public override bool Init()
        //Init called when the plugin dll is loaded
        {
            loopratehz = 5;  //Loop runs every second (The value is in Hertz, so 2 means every 500ms, 0.1f means every 10 second...) 

            return true;	 // If it is false then plugin will not load
        }

        public override bool Loaded()
        //Loaded called after the plugin dll successfully loaded
        {

            tsLandingPoint.Text = "Set Landing Point";
            tsLandingPoint.Click += TsLandingPoint_Click;
            Host.FDMenuMap.Items.Add(tsLandingPoint);
            landingOverlay = new GMapOverlay("landing");


            tsStartSim.Text = "Start Simulator";
            tsStartSim.Click += TsStartSim_Click;
            Host.FDMenuMap.Items.Add(tsStartSim);


            tsSetupFleet.Text = "Setup Fleet";
            tsSetupFleet.Click += TsSetupFleet_Click;
            Host.FDMenuMap.Items.Add(tsSetupFleet);

            tsDoAutoConnect.Text = "Trigger AutoConnect";
            tsDoAutoConnect.Click += TsDoAutoConnect_Click; 
            Host.FDMenuMap.Items.Add(tsDoAutoConnect);


            Panel panel1 = Host.MainForm.Controls.Find("Panel1", true).FirstOrDefault() as Panel;


            string[] btnLabels = new string[] {
                "PLANE",
                "FLIGHT"+ Environment.NewLine +"DATA",
                "FLIGHT" + Environment.NewLine +"PLAN",
                "GEO"+ Environment.NewLine +"FENCE",
                "SETUP",
                "TUNING",
                "ENGINE",
                "FUEL",
                "BATT",
                "PAY"+ Environment.NewLine +"LOAD",
                "AIR"+ Environment.NewLine +"SPEED",
                "EKF",
                "WEATHER",
                "LAND",
                "LAUNCH",
                "PRE" + Environment.NewLine + "FLGHT",
                "ACTIONS",
                "DATA",
                "MSG",
                "COMMS" };

            string[] btnNames = new string[] {
                "PLANE",
                "FD",
                "FP",
                "GF",
                "SETUP",
                "TUNING",
                "ENGINE",
                "FUEL",
                "BATT",
                "PAYLD",
                "PITOT",
                "EKF",
                "WEATHER",
                "LAND",
                "START",
                "PREFLGHT",
                "ACTIONS",
                "DATA",
                "MSG",
                "COMMS" };


            aMain1.setPanels(btnNames, btnLabels);
            aMain1.Enabled = true;
            aMain1.Location = new Point(0, 0);
            aMain1.Size = new Size(panel1.Width, 47);
            aMain1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            aMain1.undock += annunciator1_undock;
            aMain1.buttonClicked += annunciator1_buttonClicked;
            aMain1.Visible = true;
            aMain1.SysID = 0;
            aMain1.Name = "";
            aMain1.Active = true;
            aMain1.Refresh();

            aMain2.setPanels(btnNames, btnLabels);
            aMain2.Enabled = true;
            aMain2.Location = new Point(0, 47);
            aMain2.Size = new Size(panel1.Width, 47);
            aMain2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            //aMain2.undock += annunciator2_undock;
            aMain2.buttonClicked += annunciator1_buttonClicked;
            aMain2.Visible = true;
            aMain2.SysID = 0;
            aMain2.Name = "";
            aMain2.Active = false;
            aMain2.Refresh();

            aMain3.setPanels(btnNames, btnLabels);
            aMain3.Enabled = true;
            aMain3.Location = new Point(0, 94);
            aMain3.Size = new Size(panel1.Width, 47);
            aMain3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            //aMain3.undock += annunciator2_undock;
            aMain3.buttonClicked += annunciator1_buttonClicked;
            aMain3.Visible = true;
            aMain3.SysID = 0;
            aMain3.Name = "";
            aMain3.Active = false;
            aMain3.Refresh();


            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {

                MenuStrip MainMenu = Host.MainForm.Controls.Find("MainMenu", true).FirstOrDefault() as MenuStrip;
                if (MainMenu != null) MainMenu.Visible = false;

                panel1.Size = new Size(panel1.Width, 141);

            }));

            //Check undocked status
            if (Settings.Instance["aMainDocked"] != null)
            {
                bool aMainDocked = Settings.Instance.GetBoolean("aMainDocked");
                if (!aMainDocked)
                {
                    annunciator1_undock(this, new EventArgs());
                }
                else
                {
                    panel1.Controls.Add(aMain1);

                    panel1.Controls.Add(aMain2); 
                    panel1.Controls.Add(aMain3);


                }
            }
            else
            {
                panel1.Controls.Add(aMain1);
                panel1.Controls.Add(aMain2);
                panel1.Controls.Add(aMain3);
            }


            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                //If this is not a supervisor then you have to setup only one ID
                if (!isSupervisor())
                {
                    aMain1.Active = true;
                    aMain2.Active = false;
                    aMain3.Active = false;
                    panel1.Size = new Size(panel1.Width, 47);
                }
            }));


            colorMessagePage.Text = "ColorMessages";
            colorMessagePage.Name = "colorMsgTab";
            fctb = new FastColoredTextBoxNS.FastColoredTextBox();
            setupFCTB();
            this.fctb.Size = colorMessagePage.ClientSize;
            colorMessagePage.Controls.Add(fctb);
            Host.MainForm.FlightData.tabControlactions.TabPages.Add(colorMessagePage);


            payloadControlpage.Text = "PayloadControl";
            payloadControlpage.Name = "payLoadCTRTab";

            plControl.Name = "plControl";
            plControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            plControl.Location = new Point(0, 0);
            plControl.Size = new Size(payloadControlpage.Width, payloadControlpage.Height);
            plControl.setupClicked += PlControl_setupClicked;
            plControl.igniteClicked += PlControl_igniteClicked;
            payloadControlpage.Controls.Add(plControl);
            plControl.redrawControls();
            plControl.setSafetyStatus(false);
            Host.MainForm.FlightData.tabControlactions.TabPages.Add(payloadControlpage);

            engineControlPage.Text = "Engine Control";
            engineControlPage.Name = "engCtrlTab";

            eCtrl.Name = "engineControl";
            eCtrl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            eCtrl.Location = new Point(0, 0);
            eCtrl.Size = new Size(engineControlPage.Width, engineControlPage.Height);
            engineControlPage.Controls.Add(eCtrl);
            eCtrl.armClicked += ECtrl_armClicked;

            eCtrl.startClicked += ECtrl_startClicked;
            eCtrl.stopClicked += ECtrl_stopClicked;
            eCtrl.emergencyClicked += ECtrl_emergencyClicked;

            Host.MainForm.FlightData.tabControlactions.TabPages.Add(engineControlPage);

            eCtrl.setEngineStatus("Ready to Start", "No error");





            connectionControlPage.Text = "Comms";
            connectionControlPage.Name = "commsTab";

            connectionControlPage.Controls.Add(MainV2._connectionControl);
            ToolStrip ts = new ToolStrip();
            ts.BackColor = Color.Black;
            ts.Items.Add(MainV2.instance.MenuConnect);
            connectionControlPage.Controls.Add(ts);
            MainV2._connectionControl.Location = new Point(0, 0);
            connectionStats = new ConnectionStats(Host.comPort);
            connectionStats.Location = new Point(0, 50);
            connectionControlPage.Controls.Add(connectionStats);
            Host.MainForm.FlightData.tabControlactions.TabPages.Add(connectionControlPage);


            batteryPage.Text = "Battery";
            batteryPage.Name = "battTab";
            bp.Location = new Point(0, 0);
            bp.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            bp.Size = batteryPage.ClientSize;
            batteryPage.Controls.Add(bp);
            Host.MainForm.FlightData.tabControlactions.TabPages.Add(batteryPage);


            ekfPage.Text = "EKF";
            ekfPage.Name = "ekfTab";
            ekfPage.Controls.Add(ekfStat);
            ekfStat.Size = ekfPage.ClientSize;
            ekfStat.Location = new Point(0, 0);
            ekfStat.Dock = DockStyle.Fill;
            Host.MainForm.FlightData.tabControlactions.TabPages.Add(ekfPage);


            pitotPage.Text = "Airspeed";
            pitotPage.Name = "pitotTab";
            pitotPage.Controls.Add(pitot);
            pitot.Size = pitotPage.ClientSize;
            pitot.Location = new Point(0, 0);
            pitot.Dock = DockStyle.Fill;
            pitot.calibrateClicked += Pitot_calibrateClicked;
            Host.MainForm.FlightData.tabControlactions.TabPages.Add(pitotPage);

            fuelPage.Text = "Fuel";
            fuelPage.Name = "fuelTab";
            fuelPage.Controls.Add(fuel);
            fuel.Size = fuelPage.ClientSize;
            fuel.Location = new Point(0, 0);
            fuel.Dock = DockStyle.Fill;
            Host.MainForm.FlightData.tabControlactions.TabPages.Add(fuelPage);


            landingPage.Text = "Landing";
            landingPage.Name = "landingTab";
            landingPage.Controls.Add(lc);
            lc.Size = landingPage.ClientSize;
            lc.Location = new Point(0, 0);
            lc.Dock = DockStyle.Fill;
            lc.StartLandingClicked += Lc_waitClicked;
            lc.landClicked += Lc_landClicked;
            lc.setspeedClicked += Lc_setspeedClicked;
            lc.setCruiseSpeedClicked += Lc_setCruiseSpeedClicked;
            lc.abortLandingClicked += Lc_abortLandingClicked;
            lc.nudgeSpeedClicked += Lc_nudgeSpeedClicked;
            Host.MainForm.FlightData.tabControlactions.TabPages.Add(landingPage);


            overviewPage.Text = "Overview";
            overviewPage.Name = "overViewTab";
            overviewPage.Controls.Add(tlOw);
            tlOw.Size = overviewPage.ClientSize;
            tlOw.Location = new Point(3,3);
            tlOw.Dock = DockStyle.Fill;
            tlOw.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom;
            tlOw.ColumnCount = 4;
            tlOw.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tlOw.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tlOw.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tlOw.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
            tlOw.RowCount = 12;
            tlOw.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;


            for (int i = 0; i < 12; i++)
            {
                Label l = new Label();
                l.AutoSize = true;
                l.Dock = DockStyle.Fill;
                l.Font = new Font("Arial", 12,FontStyle.Bold);
                tlOw.Controls.Add(l, 0, i);

                Label l1 = new Label();
                l1.Font = new Font("Arial", 12);
                l1.Dock = DockStyle.Fill;
                l1.TextAlign = ContentAlignment.TopCenter;
                l1.AutoSize = true;
                tlOw.Controls.Add(l1, 1, i);

                Label l2 = new Label();
                l2.Font = new Font("Arial", 12);
                l2.Dock = DockStyle.Fill;
                l2.TextAlign = ContentAlignment.TopCenter;
                l2.AutoSize = true;
                tlOw.Controls.Add(l2, 2, i);

                Label l3 = new Label();
                l3.Font = new Font("Arial", 12);
                l3.Dock = DockStyle.Fill;
                l3.TextAlign = ContentAlignment.TopCenter;
                l3.AutoSize = true;
                tlOw.Controls.Add(l3, 2, i);
            }

            //Update Row Names
            ((Label)tlOw.GetControlFromPosition(0, 0)).Text = "SYSID";
            ((Label)tlOw.GetControlFromPosition(0, 1)).Text = "Callsign";
            ((Label)tlOw.GetControlFromPosition(0, 2)).Text = "AirSpeed";
            ((Label)tlOw.GetControlFromPosition(0, 3)).Text = "GroundSpeed";
            ((Label)tlOw.GetControlFromPosition(0, 4)).Text = "Altitude";
            ((Label)tlOw.GetControlFromPosition(0, 5)).Text = "ClimbRate";
            ((Label)tlOw.GetControlFromPosition(0, 6)).Text = "Fuel Consumed";
            ((Label)tlOw.GetControlFromPosition(0, 7)).Text = "Radio Health";
            ((Label)tlOw.GetControlFromPosition(0, 8)).Text = "Distance To Home";
            ((Label)tlOw.GetControlFromPosition(0, 9)).Text = "Engine RPM";
            ((Label)tlOw.GetControlFromPosition(0, 10)).Text = "Engine EGT";
            ((Label)tlOw.GetControlFromPosition(0, 11)).Text = "Engine Thr";

            Host.MainForm.FlightData.tabControlactions.TabPages.Add(overviewPage);

            //Setup mavlink receiving
            //Host.comPort.OnPacketReceived += MavOnOnPacketReceivedHandler;

            chuteServo = Settings.Instance.GetInt32("chuteServo", 9);
            Settings.Instance["chuteServo"] = chuteServo.ToString();

            chuteServoOpenPWM = Settings.Instance.GetInt32("chuteServoOpenPWM", 1100);
            Settings.Instance["chuteServoOpenPWM"] = chuteServoOpenPWM.ToString();


            return true;     //If it is false plugin will not start (loop will not called)
        }

        private void TsDoAutoConnect_Click(object sender, EventArgs e)
        {
            AutoConnect.Start();
        }

        //Setup sysid's and names and number of planes in the air;
        private void TsSetupFleet_Click(object sender, EventArgs e)
        {

            fleetSetup frm = new fleetSetup();
            frm.ShowDialog();

            aMain1.SysID = plane1ID;
            aMain2.SysID = plane2ID;
            aMain3.SysID = plane3ID;    
            aMain1.Name = plane1Name;    
            aMain2.Name = plane2Name;
            aMain3.Name = plane3Name;

            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                //If this is not a supervisor then you have to setup only one ID
                if (!isSupervisor())
                {
                    aMain1.Active = true;
                    aMain2.Active = false;
                    aMain3.Active = false;
                    Panel panel1 = Host.MainForm.Controls.Find("Panel1", true).FirstOrDefault() as Panel;
                    panel1.Size = new Size(panel1.Width, 47);
                }
                else
                {
                    aMain1.Active = true;
                    aMain2.Active = (aMain2.SysID != 0);
                    aMain3.Active = (aMain3.SysID != 0);
                    Panel panel1 = Host.MainForm.Controls.Find("Panel1", true).FirstOrDefault() as Panel;
                    int panelsize = 47 + (aMain2.Active ? 47 : 0) + (aMain3.Active ? 47 : 0); 
                    panel1.Size = new Size(panel1.Width, panelsize);

                }


            }));
        }

        private void TsStartSim_Click(object sender, EventArgs e)
        {
            MainV2.instance.Simulation.StartProtars(3);
        }

        private void Lc_nudgeSpeedClicked(object sender, EventArgs e)
        {
            if (Host.cs.mode.ToUpper() == "GUIDED")
            {
                PointLatLngAlt target = new PointLatLngAlt(Host.cs.TargetLocation);
                lc.state = LandState.GoToLand;
                Locationwp gotohere = new Locationwp();

                gotohere.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
                gotohere.alt = (float)Host.cs.TargetLocation.Alt - (float)Host.cs.HomeAlt; // back to m
                gotohere.lat = Host.cs.TargetLocation.Lat;
                gotohere.lng = Host.cs.TargetLocation.Lng;

                Host.comPort.setMode("FBWA");

                try
                {
                    MainV2.comPort.setGuidedModeWP(gotohere);
                }
                catch (Exception ex)
                {
                    CustomMessageBox.Show("Unable to switch back to Guided" + ex.Message, "ERROR");
                }
            }
        }

        private void Lc_abortLandingClicked(object sender, EventArgs e)
        {
            //Clean up markers
            try
            {
                landingOverlay.Markers.RemoveAt(3);
                landingOverlay.Markers.RemoveAt(3);
            }
            catch { }
        }

        private void Lc_setCruiseSpeedClicked(object sender, EventArgs e)
        {

            if (Host.cs.connected)
            {

                var speed = MainV2.comPort.MAV.param["TRIM_ARSPD_CM"].Value / 100;
                if (speed <= 0) return;
                try
                {
                    MainV2.comPort.doCommandAsync(MainV2.comPort.MAV.sysid, MainV2.comPort.MAV.compid,
                            MAVLink.MAV_CMD.DO_CHANGE_SPEED, 0, (float)speed, 0, 0, 0, 0, 0);
                }
                catch
                {
                    CustomMessageBox.Show("Unable to set speed", "Error");
                }
            }
        }


        public void sendUDPBroadcast(string message)
        {
            try
            {
                UdpClient client = new UdpClient();
                IPEndPoint ip = new IPEndPoint(IPAddress.Broadcast, 19729);
                byte[] bytes = Encoding.ASCII.GetBytes(message);
                client.Send(bytes, bytes.Length, ip);
                client.Close();
            }
            catch { }

        }

        //Returns true if this is a supervisor station, if there is no setting then it returns false by default
        public bool isSupervisor()
        {
            bool val = Settings.Instance.GetBoolean("Protar_Supervisor", false);
            return val;
        }

        public override bool Loop()
        //Loop is called in regular intervalls (set by loopratehz)
        {
            //The most important thing is to do the landing update, this will run at 5hz 

            doLanding();

            //If 500ms ellapsed to the processing
            if (((TimeSpan)(DateTime.Now - lastNonCriticalUpdate)).TotalMilliseconds > 500)
            {



                lastNonCriticalUpdate = DateTime.Now;


                updateNotifications();
                update_gauges();
                if (isSupervisor()) updateOverview();

                if (isSupervisor()) sendUDPBroadcast(sit.ToJSON());

                MainV2.instance.BeginInvoke((MethodInvoker)(() =>
                {
                    lc.updateLabels();
                }));


                #region MessagesBox

                //Message box update

                //Iterate over all ports and put every message to the common messages box

                foreach (var port in MainV2.Comports)
                {
                    //***
                    if (port.sysidcurrent == aMain1.SysID)
                    {
                        if (port.MAV.cs.messages.LastOrDefault().time != lastDisplayedMessage1)
                        {
                            fctb.BeginUpdate();
                            fctb.TextSource.CurrentTB = fctb;

                            port.MAV.cs.messages.ForEach(x =>
                             {
                                 if (x.Item1 > lastDisplayedMessage1)
                                 {

                                     TextStyle displayStyle;
                                     switch ((int)x.Item3)
                                     {
                                         case 0:
                                         case 1:
                                         case 2:
                                             {
                                                 displayStyle = errorStyle;
                                                 aMain1.setStatus("MSG", Stat.ALERT);
                                                 break;
                                             }
                                         case 3:
                                         case 4:
                                             {
                                                 displayStyle = warningStyle;
                                                 aMain1.setStatus("MSG", Stat.WARNING);
                                                 break;
                                             }
                                         default:
                                             {
                                                 displayStyle = infoStyle;
                                                 break;
                                             }
                                     }

                                     fctb.SelectionStart = 0;
                                     fctb.InsertText("(ID " + port.sysidcurrent + ") " + x.Item1 + " : (" + x.Item3 + ") " + x.Item2 + "\r\n", displayStyle);
                                 }
                             });
                            fctb.EndUpdate();
                            lastDisplayedMessage1 = port.MAV.cs.messages.LastOrDefault().time;
                        }
                    }
                    //---
                    //***
                    if (port.sysidcurrent == aMain2.SysID)
                    {
                        if (port.MAV.cs.messages.LastOrDefault().time != lastDisplayedMessage2)
                        {
                            fctb.BeginUpdate();
                            fctb.TextSource.CurrentTB = fctb;

                            port.MAV.cs.messages.ForEach(x =>
                            {
                                if (x.Item1 > lastDisplayedMessage2)
                                {

                                    TextStyle displayStyle;
                                    switch ((int)x.Item3)
                                    {
                                        case 0:
                                        case 1:
                                        case 2:
                                            {
                                                displayStyle = errorStyle;
                                                aMain2.setStatus("MSG", Stat.ALERT);
                                                break;
                                            }
                                        case 3:
                                        case 4:
                                            {
                                                displayStyle = warningStyle;
                                                aMain2.setStatus("MSG", Stat.WARNING);
                                                break;
                                            }
                                        default:
                                            {
                                                displayStyle = infoStyle;
                                                break;
                                            }
                                    }

                                    fctb.SelectionStart = 0;
                                    fctb.InsertText("(ID " + port.sysidcurrent + ") " + x.Item1 + " : (" + x.Item3 + ") " + x.Item2 + "\r\n", displayStyle);
                                }
                            });
                            fctb.EndUpdate();
                            lastDisplayedMessage2 = port.MAV.cs.messages.LastOrDefault().time;
                        }
                    }
                    //---
                    //***
                    if (port.sysidcurrent == aMain3.SysID)
                    {
                        if (port.MAV.cs.messages.LastOrDefault().time != lastDisplayedMessage3)
                        {
                            fctb.BeginUpdate();
                            fctb.TextSource.CurrentTB = fctb;

                            port.MAV.cs.messages.ForEach(x =>
                            {
                                if (x.Item1 > lastDisplayedMessage3)
                                {

                                    TextStyle displayStyle;
                                    switch ((int)x.Item3)
                                    {
                                        case 0:
                                        case 1:
                                        case 2:
                                            {
                                                displayStyle = errorStyle;
                                                aMain3.setStatus("MSG", Stat.ALERT);
                                                break;
                                            }
                                        case 3:
                                        case 4:
                                            {
                                                displayStyle = warningStyle;
                                                aMain3.setStatus("MSG", Stat.WARNING);
                                                break;
                                            }
                                        default:
                                            {
                                                displayStyle = infoStyle;
                                                break;
                                            }
                                    }

                                    fctb.SelectionStart = 0;
                                    fctb.InsertText("(ID " + port.sysidcurrent + ") " + x.Item1 + " : (" + x.Item3 + ") " + x.Item2 + "\r\n", displayStyle);
                                }
                            });
                            fctb.EndUpdate();
                            lastDisplayedMessage3 = port.MAV.cs.messages.LastOrDefault().time;
                        }
                    }
                    //---

                }

                #endregion



                #region GCSPower
                PowerStatus pwr = SystemInformation.PowerStatus;
                bp.setGcsVoltage(pwr.BatteryLifePercent, pwr.PowerLineStatus);
                #endregion
            }

            return true;	//Return value is not used
        }

        private void updateOverview()
        {

            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {

            foreach (var port in MainV2.Comports)
            {

                    if (port.sysidcurrent != 0)
                    {
                        if (port.sysidcurrent == aMain1.SysID)
                        {
                            ((Label)tlOw.GetControlFromPosition(1, 0)).Text = aMain1.SysID.ToString();
                            ((Label)tlOw.GetControlFromPosition(1, 1)).Text = aMain1.Name;
                            ((Label)tlOw.GetControlFromPosition(1, 2)).Text = port.MAV.cs.airspeed.ToString("F0") + " m/s";
                            ((Label)tlOw.GetControlFromPosition(1, 3)).Text = port.MAV.cs.groundspeed.ToString("F0") + " m/s";
                            ((Label)tlOw.GetControlFromPosition(1, 4)).Text = port.MAV.cs.alt.ToString("F0") + " m";
                            ((Label)tlOw.GetControlFromPosition(1, 5)).Text = port.MAV.cs.climbrate.ToString("F1") + " m/s";
                            ((Label)tlOw.GetControlFromPosition(1, 6)).Text = port.MAV.cs.fuel_consumed.ToString("F1") + " L";
                            ((Label)tlOw.GetControlFromPosition(1, 7)).Text = port.MAV.cs.linkqualitygcs.ToString("F0") + " %";
                            ((Label)tlOw.GetControlFromPosition(1, 8)).Text = port.MAV.cs.DistToHome.ToString("F0") + " m";
                            ((Label)tlOw.GetControlFromPosition(1, 9)).Text = port.MAV.cs.eng_rpm.ToString("F0");
                            ((Label)tlOw.GetControlFromPosition(1, 10)).Text = port.MAV.cs.eng_egt.ToString("F0") + " C";
                            ((Label)tlOw.GetControlFromPosition(1, 11)).Text = port.MAV.cs.eng_throttle.ToString("F0") + " %";

                        }

                        if (port.sysidcurrent == aMain2.SysID)
                        {
                            ((Label)tlOw.GetControlFromPosition(2, 0)).Text = aMain2.SysID.ToString();
                            ((Label)tlOw.GetControlFromPosition(2, 1)).Text = aMain2.Name;
                            ((Label)tlOw.GetControlFromPosition(2, 2)).Text = port.MAV.cs.airspeed.ToString("F0") + " m/s";
                            ((Label)tlOw.GetControlFromPosition(2, 3)).Text = port.MAV.cs.groundspeed.ToString("F0") + " m/s";
                            ((Label)tlOw.GetControlFromPosition(2, 4)).Text = port.MAV.cs.alt.ToString("F0") + " m";
                            ((Label)tlOw.GetControlFromPosition(2, 5)).Text = port.MAV.cs.climbrate.ToString("F1") + " m/s";
                            ((Label)tlOw.GetControlFromPosition(2, 6)).Text = port.MAV.cs.fuel_consumed.ToString("F1") + " L";
                            ((Label)tlOw.GetControlFromPosition(2, 7)).Text = port.MAV.cs.linkqualitygcs.ToString("F0") + " %";
                            ((Label)tlOw.GetControlFromPosition(2, 8)).Text = port.MAV.cs.DistToHome.ToString("F0") + " m";
                            ((Label)tlOw.GetControlFromPosition(2, 9)).Text = port.MAV.cs.eng_rpm.ToString("F0");
                            ((Label)tlOw.GetControlFromPosition(2, 10)).Text = port.MAV.cs.eng_egt.ToString("F0") + " C";
                            ((Label)tlOw.GetControlFromPosition(2, 11)).Text = port.MAV.cs.eng_throttle.ToString("F0") + " %";

                        }
                        if (port.sysidcurrent == aMain3.SysID)
                        {
                            ((Label)tlOw.GetControlFromPosition(3, 0)).Text = aMain3.SysID.ToString();
                            ((Label)tlOw.GetControlFromPosition(3, 1)).Text = aMain3.Name;
                            ((Label)tlOw.GetControlFromPosition(3, 2)).Text = port.MAV.cs.airspeed.ToString("F0") + " m/s";
                            ((Label)tlOw.GetControlFromPosition(3, 3)).Text = port.MAV.cs.groundspeed.ToString("F0") + " m/s";
                            ((Label)tlOw.GetControlFromPosition(3, 4)).Text = port.MAV.cs.alt.ToString("F0") + " m";
                            ((Label)tlOw.GetControlFromPosition(3, 5)).Text = port.MAV.cs.climbrate.ToString("F1") + " m/s";
                            ((Label)tlOw.GetControlFromPosition(3, 6)).Text = port.MAV.cs.fuel_consumed.ToString("F1") + " L";
                            ((Label)tlOw.GetControlFromPosition(3, 7)).Text = port.MAV.cs.linkqualitygcs.ToString("F0") + " %";
                            ((Label)tlOw.GetControlFromPosition(3, 8)).Text = port.MAV.cs.DistToHome.ToString("F0") + " m";
                            ((Label)tlOw.GetControlFromPosition(3, 9)).Text = port.MAV.cs.eng_rpm.ToString("F0");
                            ((Label)tlOw.GetControlFromPosition(3, 10)).Text = port.MAV.cs.eng_egt.ToString("F0") + " C";
                            ((Label)tlOw.GetControlFromPosition(3, 11)).Text = port.MAV.cs.eng_throttle.ToString("F0") + " %";
                        }
                    }
            }


            }));



        }

        public override bool Exit()
        //Exit called when plugin is terminated (usually when Mission Planner is exiting)
        {


            //Save position, we need to do it since formclose is not called in plugins

            annunciatorForm?.SaveStartupLocation();
            return true;	//Return value is not used
        }


        private void doLanding()
        {
            //This will handle the landing process

            if (!Host.cs.connected || !Host.cs.armed)
            {
                lc.state = LandState.None;
                return;
            }

            //Check status
            if (lc.state == LandState.None) return;

            if (lc.state == LandState.GoToWaiting)
            {
                //Wait until we started loitering around the waiting point (+50meter need to check for discrepancies between loiter radius and actual radius
                Console.WriteLine(Host.cs.Location.GetDistance(lc.WaitingPoint));
                if (Host.cs.Location.GetDistance(lc.WaitingPoint) <= MainV2.comPort.MAV.param["WP_LOITER_RAD"].Value + 50
                    && (Host.cs.alt <= lc.LandingAlt+20) )
                {
                    lc.state = LandState.WaitForSpeed;
                    try
                    {
                        MainV2.comPort.doCommandAsync(MainV2.comPort.MAV.sysid, MainV2.comPort.MAV.compid,
                                MAVLink.MAV_CMD.DO_CHANGE_SPEED, 0, (float)lc.LandingSpeed, 0, 0, 0, 0, 0);
                    }
                    catch
                    {
                        lc.state = LandState.None;
                        CustomMessageBox.Show("Unable to set speed", "Error");
                    }
                }
            }

            if (lc.state == LandState.WaitForSpeed)
            {
                if (Host.cs.airspeed <= lc.LandingSpeed + 3)
                {
                    lc.state = LandState.WaitForTangent;
                }
            }
               
            if (lc.state == LandState.WaitForTangent)
            {
                if (Math.Abs(lc.WindDirection - Host.cs.yaw) < 3)
                {

                    lc.state = LandState.GoToLand;
                    Locationwp gotohere = new Locationwp();

                    gotohere.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
                    gotohere.alt = (float)lc.LandingPoint.Alt; // back to m
                    gotohere.lat = (lc.LandingPoint.Lat);
                    gotohere.lng = (lc.LandingPoint.Lng);

                    try
                    {
                        MainV2.comPort.setGuidedModeWP(gotohere);
                        lc.state = LandState.GoToLand;
                    }
                    catch (Exception ex)
                    {
                        CustomMessageBox.Show("Unable go to Landing point" + ex.Message, "ERROR");
                        lc.state = LandState.None;
                    }

                }
            }

            //Switch from landingpoint to target point if we are within loiter radius + 200meter
            if (lc.state == LandState.GoToLand)
            {
                if (Host.cs.Location.GetDistance(lc.LandingPoint) <= MainV2.comPort.MAV.param["WP_LOITER_RAD"].Value + 200)
                {
                    Locationwp gotohere = new Locationwp();

                    gotohere.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
                    gotohere.alt = (float)lc.TargetPoint.Alt; // back to m
                    gotohere.lat = (lc.TargetPoint.Lat);
                    gotohere.lng = (lc.TargetPoint.Lng);

                    try
                    {
                        MainV2.comPort.setGuidedModeWP(gotohere);

                    }
                    catch { }
                    lc.state = LandState.CloseToLand;
                }
            }


            //Landing position calculation

            var speed = Host.cs.airspeed;
            PointLatLngAlt currentpos = new PointLatLngAlt(Host.cs.Location);
            PointLatLngAlt openpos1 = currentpos.newpos(Host.cs.yaw, speed * lc.OpeningTime);

            float wd = 0;

            if (Convert.ToBoolean(Host.config["reverse_winddir_drag", "true"]))
            {

                wd = wrap360(lc.WindDirection-180);
            }
            else
            {
                wd = lc.WindDirection;
            }

            PointLatLngAlt openpos2 = openpos1.newpos(wd, (Host.cs.g_wind_vel * (Host.cs.alt / lc.SinkRate) * lc.WindDrag));

            GMarkerGoogle p1 = new GMarkerGoogle(openpos1, GMarkerGoogleType.white_small);
            p1.Tag = "p1";
            GMarkerGoogle p2 = new GMarkerGoogle(openpos2, GMarkerGoogleType.yellow_small);
            p2.Tag = "p2";



            if (openpos2.GetDistance(lc.LandingPoint) <= 20 )
            {

                if (lc.chute == ChuteState.AutoOpenEnabled)
                {
                    Host.cs.messageHigh = "OPEN OPEN OPEN";
                    MainV2.comPort.doCommand((byte)MainV2.comPort.sysidcurrent, (byte)MainV2.comPort.compidcurrent, MAVLink.MAV_CMD.DO_SET_SERVO, chuteServo, chuteServoOpenPWM, 0, 0, 0, 0, 0);
                    SystemSounds.Exclamation.Play();
                }
                else
                {
                    Host.cs.messageHigh = "SIMULTED CHUTE OPEN";
                    SystemSounds.Exclamation.Play();

                }

            }
            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {

                try
                {
                    landingOverlay.Markers.RemoveAt(3);
                    landingOverlay.Markers.RemoveAt(3);
                }
                catch { }

                landingOverlay.Markers.Add(p1);
                landingOverlay.Markers.Add(p2);
                Host.FDGMapControl.Refresh();
            }));
        }


        private void Lc_setspeedClicked(object sender, EventArgs e)
        {
            try
            {
                MainV2.comPort.doCommandAsync(MainV2.comPort.MAV.sysid, MainV2.comPort.MAV.compid,
                        MAVLink.MAV_CMD.DO_CHANGE_SPEED, 0, (float)lc.LandingSpeed, 0, 0, 0, 0, 0);
            }
            catch
            {
                CustomMessageBox.Show("Unable to set speed", "Error");
            }
        }

        private void Lc_landClicked(object sender, EventArgs e)
        {

            //Todo Check valid point
            if (Host.cs.Location.GetDistance(lc.TargetPoint) > 8000)
            {
                CustomMessageBox.Show("Target point is more tha 8Km away!", "ERROR");
                return;
            }


            Locationwp gotohere = new Locationwp();

            gotohere.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
            gotohere.alt = (float)lc.LandingPoint.Alt; // back to m
            gotohere.lat = (lc.LandingPoint.Lat);
            gotohere.lng = (lc.LandingPoint.Lng);

            try
            {
                MainV2.comPort.setGuidedModeWP(gotohere);
                lc.state = LandState.GoToLand;
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Unable go to Landing point" + ex.Message, "ERROR");
            }
        }

        private void Lc_waitClicked(object sender, EventArgs e)
        {
            //Todo Check valid point
            if (Host.cs.Location.GetDistance(lc.WaitingPoint) > 8000)
            {
                CustomMessageBox.Show("Waiting point is more tha 8Km away!", "ERROR");
                return;
            }

            lc.state = LandState.GoToWaiting;

            Locationwp gotohere = new Locationwp();

            gotohere.id = (ushort)MAVLink.MAV_CMD.WAYPOINT;
            gotohere.alt = (float)lc.WaitingPoint.Alt;  // back to m
            gotohere.lat = (lc.WaitingPoint.Lat);
            gotohere.lng = (lc.WaitingPoint.Lng);

            try
            {
                MainV2.comPort.setGuidedModeWP(gotohere);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Unable go to Waiting point" + ex.Message, "ERROR");
                lc.state = LandState.None;
            }



        }

        private void ECtrl_emergencyClicked(object sender, EventArgs e)
        {
            MainV2.comPort.doCommand(MAVLink.MAV_CMD.DO_SET_SERVO, (float)Convert.ToInt16(Host.config["jetcontrolch","10"]), 1000, 0, 0, 0, 0, 0, false);
        }

        private void ECtrl_stopClicked(object sender, EventArgs e)
        {
            MainV2.comPort.doCommand(MAVLink.MAV_CMD.DO_SET_SERVO, (float)Convert.ToInt16(Host.config["jetcontrolch", "10"]), 1500, 0, 0, 0, 0, 0, false);
        }

        private void ECtrl_startClicked(object sender, EventArgs e)
        {
            MainV2.comPort.doCommand(MAVLink.MAV_CMD.DO_SET_SERVO, (float)Convert.ToInt16(Host.config["jetcontrolch", "10"]), 2000, 0, 0, 0, 0, 0, false);
        }

        private void ECtrl_armClicked(object sender, EventArgs e)
        {
            
            if (!Host.cs.connected)
                return;

            // arm the MAV
            try
            {
                var isitarmed = Host.comPort.MAV.cs.armed;
                var action = Host.comPort.MAV.cs.armed ? "Disarm" : "Arm";

                if (isitarmed)
                    if (CustomMessageBox.Show("Are you sure you want to " + action, action,
                            CustomMessageBox.MessageBoxButtons.YesNo) !=
                        CustomMessageBox.DialogResult.Yes)
                        return;
                StringBuilder sb = new StringBuilder();
                var sub = Host.comPort.SubscribeToPacketType(MAVLink.MAVLINK_MSG_ID.STATUSTEXT, message =>
                {
                    sb.AppendLine(Encoding.ASCII.GetString(((MAVLink.mavlink_statustext_t)message.data).text)
                        .TrimEnd('\0'));
                    return true;
                }, (byte)Host.comPort.sysidcurrent, (byte)MainV2.comPort.compidcurrent);
                bool ans = Host.comPort.doARM(!isitarmed);
                Host.comPort.UnSubscribeToPacketType(sub);
                if (ans == false)
                {
                    if (CustomMessageBox.Show(
                            action + " failed.\n" + sb.ToString() + "\nForce " + action +
                            " can bypass safety checks,\nwhich can lead to the vehicle crashing\nand causing serious injuries.\n\nDo you wish to Force " +
                            action + "?", "Error", CustomMessageBox.MessageBoxButtons.YesNo,
                            CustomMessageBox.MessageBoxIcon.Exclamation, "Force " + action, "Cancel") ==
                        CustomMessageBox.DialogResult.Yes)
                    {
                        ans = Host.comPort.doARM(!isitarmed, true);
                        if (ans == false)
                        {
                            CustomMessageBox.Show("ARM request rejected by MAV", "Error");
                        }
                    }
                }
            }
            catch
            {
                CustomMessageBox.Show("No response for the ARM command", "Error");
            }
        }

        private void PlControl_igniteClicked(object sender, EventArgs e)
        {

            byte[] c = Encoding.Default.GetBytes("P");
            MainV2.comPort.sendPacket(new MAVLink.mavlink_named_value_float_t() { name = c, time_boot_ms = 0, value = (float)plControl.igniteMask }, MainV2.comPort.sysidcurrent, MainV2.comPort.compidcurrent);

            Console.WriteLine("Payload ignite:{0}", plControl.igniteMask);

        }

        //set landing point an init landing sequence
        private void TsLandingPoint_Click(object sender, EventArgs e)
        {
            PointLatLngAlt lp = Host.FDMenuMapPosition;


            float landReverse = Settings.Instance.GetFloat("LandDirectionReverse", 0);
            Settings.Instance["LandDirectionReverse"] = landReverse.ToString();
   
            lc.updateLandingData(lp, wrap360(Host.cs.g_wind_dir - landReverse), Host.cs.g_wind_vel, (int)MainV2.comPort.MAV.param["WP_LOITER_RAD"].Value);
   
            landingOverlay.Markers.Clear();
            landingOverlay.Routes.Clear();

            markerWaiting = new GMarkerGoogle(lc.WaitingPoint, GMarkerGoogleType.lightblue_dot);
            markerWaiting.ToolTipText = "Waiting Point";
            markerLanding = new GMarkerGoogle(lc.LandingPoint, GMarkerGoogleType.green_dot);
            markerLanding.ToolTipText = "Landing Point";
            markerTarget = new GMarkerGoogle(lc.TargetPoint, GMarkerGoogleType.pink_dot);
            markerTarget.ToolTipText = "FlyOver";

            landingRoute = new GMapRoute("landingline");
            landingRoute.Points.Add(lc.WaitingPointTangent);
            landingRoute.Points.Add(lc.LandingPoint);
            landingRoute.Points.Add(lc.TargetPoint);

            landingOverlay.Markers.Add(markerWaiting);
            landingOverlay.Markers.Add(markerLanding);
            landingOverlay.Markers.Add(markerTarget);
            landingOverlay.Routes.Add(landingRoute);

            Host.FDGMapControl.Overlays.Add(landingOverlay);
            Host.FDGMapControl.Position = Host.FDGMapControl.Position;


            lc.LandingPoint.Tag = "LP";
            lc.WaitingPoint.Tag = "WP";

            //sendUDPBroadcast(lc.LandingPoint.ToJSON());
            //sendUDPBroadcast(lc.WaitingPoint.ToJSON());

        }

        private void Pitot_calibrateClicked(object sender, EventArgs e)
        {
            if (!MainV2.comPort.MAV.cs.connected)
            {
                CustomMessageBox.Show("You have to connect first!", "Action", MessageBoxButtons.OK);
                return;
            }

            if (MainV2.comPort.MAV.cs.armed)
            {
                CustomMessageBox.Show("You cannot do it while aircraft is armed!", "Action", MessageBoxButtons.OK);
                return;

            }

            if (CustomMessageBox.Show("Are you sure you want to do Preflight Calibration ?", "Action", MessageBoxButtons.YesNo) == (int)DialogResult.Yes)
            {
                try
                {
                    ((Control)sender).Enabled = false;

                    int param1 = 0;
                    int param2 = 0;
                    int param3 = 1;

                    //baro/airspeed calibration, but no gyro !
                    param3 = 1; // baro / airspeed
                    var cmd = (MAVLink.MAV_CMD)Enum.Parse(typeof(MAVLink.MAV_CMD), "Preflight_Calibration".ToUpper());

                    if (MainV2.comPort.doCommand(cmd, param1, param2, param3, 0, 0, 0, 0))
                    {
                    }
                    else
                    {
                        CustomMessageBox.Show("Calibration Failed" + cmd, "ERROR");
                    }
                }
                catch
                {
                    CustomMessageBox.Show("Calibration failed", "ERROR");
                }

                ((Control)sender).Enabled = true;
            }
        }

        private void PlControl_setupClicked(object sender, EventArgs e)
        {
            fPs = new payloadSetupForm();
            fPs.Show();
            fPs.FormClosing += FPs_FormClosing;
            fPs.updateAll(plControl.payloads);
            plControl.setupEnabled(false);
        }

        private void FPs_FormClosing(object sender, FormClosingEventArgs e)
        {
            plControl.setupEnabled(true);
            plControl.updateAll(fPs.payloadStatus);
            plControl.redrawControls();
        }


        //This updates the gauge data based on the currently selected mavlink datastream;
        public void update_gauges()
        {
            eCtrl.setRpmEgt(Host.cs.eng_rpm, Host.cs.eng_egt);
            eCtrl.setThrFuel(Host.cs.eng_throttle, Host.cs.fuel_pump_volt, Host.cs.fuel_level_raw);
            eCtrl.setStatus((byte)Host.cs.eng_status, (byte)Host.cs.eng_error);
            fuel.setData(Host.cs.fuel_level_raw,Host.cs.fuel_flow, Host.cs.fuel_consumed);
            pitot.setData(Host.cs.pitot_temp, Host.cs.ambient_temp, Host.cs.pitot_target_temp, Host.cs.pitot_heat_duty);
            bp.setServo1Voltage(Host.cs.bat_servo1);
            bp.setServo2Voltage(Host.cs.bat_servo2);
            bp.setMainVoltage(Host.cs.bat_main);
            bp.setPayloadVoltage(Host.cs.bat_payload);
            plControl.setSafetyStatus(Host.cs.safety_switch);
        }

        //This updates all annunciator panels, based on connected vehicles. (Assuming cs is updated)
        public void updateNotifications()
        {
            foreach (var port in MainV2.Comports)
            {
                if (port.sysidcurrent != 0)
                {

                    if (port.sysidcurrent == aMain1.SysID)
                    {
                        Stat engineStat = Stat.NOMINAL;
                        Stat fuelStat = Stat.NOMINAL;
                        if (port.MAV.cs.eng_rpm < 30000) engineStat = Stat.ALERT;
                        if (port.MAV.cs.eng_egt > 900) engineStat = Stat.ALERT;

                        if (port.MAV.cs.fuel_level_raw < 150) fuelStat = Stat.WARNING;
                        if (port.MAV.cs.fuel_level_raw < 50) fuelStat = Stat.ALERT;

                        aMain1.setStatus("ENGINE", engineStat);
                        aMain1.setStatus("FUEL", fuelStat);

                        var lq = port.MAV.cs.linkqualitygcs;
                        if (lq >= 95) aMain1.setStatus("COMMS", Stat.NOMINAL);
                        else if (lq < 95 && lq > 70) aMain1.setStatus("COMMS", Stat.WARNING);
                        else aMain1.setStatus("COMMS", Stat.ALERT);
                        switch (getBatteryStatus(port.MAV.cs))
                        {
                            case 0:
                                aMain1.setStatus("BATT", Stat.NOMINAL);
                                break;
                            case 1:
                                aMain1.setStatus("BATT", Stat.WARNING);
                                break;
                            case 2:
                                aMain1.setStatus("BATT", Stat.ALERT);
                                break;
                            default:
                                break;
                        }

                        sit.sysid1 = port.sysidcurrent;
                        sit.pos1 = new PointLatLngAlt(port.MAV.cs.Location.Lat,port.MAV.cs.Location.Lng,port.MAV.cs.alt);
                        sit.heading1 = port.MAV.cs.yaw;
                        sit.airspeed1 = port.MAV.cs.airspeed;


                    }

                    if (port.sysidcurrent == aMain2.SysID)
                    {
                        Stat engineStat = Stat.NOMINAL;
                        Stat fuelStat = Stat.NOMINAL;
                        if (port.MAV.cs.eng_rpm < 30000) engineStat = Stat.ALERT;
                        if (port.MAV.cs.eng_egt > 900) engineStat = Stat.ALERT;

                        if (port.MAV.cs.fuel_level_raw < 150) fuelStat = Stat.WARNING;
                        if (port.MAV.cs.fuel_level_raw < 50) fuelStat = Stat.ALERT;

                        aMain2.setStatus("ENGINE", engineStat);
                        aMain2.setStatus("FUEL", fuelStat);
                        var lq = port.MAV.cs.linkqualitygcs;
                        if (lq >= 95) aMain2.setStatus("COMMS", Stat.NOMINAL);
                        else if (lq < 95 && lq > 70) aMain2.setStatus("COMMS", Stat.WARNING);
                        else aMain2.setStatus("COMMS", Stat.ALERT);
                        switch (getBatteryStatus(port.MAV.cs))
                        {
                            case 0:
                                aMain2.setStatus("BATT", Stat.NOMINAL);
                                break;
                            case 1:
                                aMain2.setStatus("BATT", Stat.WARNING);
                                break;
                            case 2:
                                aMain2.setStatus("BATT", Stat.ALERT);
                                break;
                            default:
                                break;
                        }
                        sit.sysid2 = port.sysidcurrent;
                        sit.pos2 = new PointLatLngAlt(port.MAV.cs.Location.Lat, port.MAV.cs.Location.Lng, port.MAV.cs.alt);
                        sit.heading2 = port.MAV.cs.yaw;
                        sit.airspeed2 = port.MAV.cs.airspeed;

                    }

                    if (port.sysidcurrent == aMain3.SysID)
                    {
                        Stat engineStat = Stat.NOMINAL;
                        Stat fuelStat = Stat.NOMINAL;
                        if (port.MAV.cs.eng_rpm < 30000) engineStat = Stat.ALERT;
                        if (port.MAV.cs.eng_egt > 900) engineStat = Stat.ALERT;

                        if (port.MAV.cs.fuel_level_raw < 150) fuelStat = Stat.WARNING;
                        if (port.MAV.cs.fuel_level_raw < 50) fuelStat = Stat.ALERT;

                        aMain3.setStatus("ENGINE", engineStat);
                        aMain3.setStatus("FUEL", fuelStat);
                        var lq = port.MAV.cs.linkqualitygcs;
                        if (lq >= 95) aMain3.setStatus("COMMS", Stat.NOMINAL);
                        else if (lq < 95 && lq > 70) aMain3.setStatus("COMMS", Stat.WARNING);
                        else aMain3.setStatus("COMMS", Stat.ALERT);
                        switch (getBatteryStatus(port.MAV.cs))
                        {
                            case 0:
                                aMain3.setStatus("BATT", Stat.NOMINAL);
                                break;
                            case 1:
                                aMain3.setStatus("BATT", Stat.WARNING);
                                break;
                            case 2:
                                aMain3.setStatus("BATT", Stat.ALERT);
                                break;
                            default:
                                break;
                        }
                        sit.sysid3 = port.sysidcurrent;
                        sit.pos3 = new PointLatLngAlt(port.MAV.cs.Location.Lat, port.MAV.cs.Location.Lng, port.MAV.cs.alt);
                        sit.heading3 = port.MAV.cs.yaw;
                        sit.airspeed3 = port.MAV.cs.airspeed;
                    }
                }
            }

        }


        public byte getBatteryStatus(CurrentState cs)
        {

            byte retval = 0;

            switch (cs.bat_servo1)
            {
                case float f when f <= 6:
                    retval = 2;
                    break;
                case float f when f > 6 && f <= 6.8:
                    retval = 1;
                    break;
                case float f when f > 8.4:
                    retval = 2;
                    break;
                default:
                    break;
            }

            if (retval == 2) return retval;

            switch (cs.bat_servo2)
            {
                case float f when f <= 6:
                    retval = 2;
                    break;
                case float f when f > 6 && f <= 6.8:
                    retval = 1;
                    break;
                case float f when f > 8.4:
                    retval = 2;
                    break;
                default:
                    break;
            }

            if (retval == 2) return retval;

            switch (cs.bat_payload)
            {
                case float f when f <= 9:
                    retval = 2;
                    break;
                case float f when f > 9 && f <= 10.2:
                    retval = 1;
                    break;
                case float f when f > 12.6:
                    retval = 2;
                    break;
                default:
                    break;
            }
            if (retval == 2) return retval;

            switch (cs.bat_main)
            {
                case float f when f <= 12:
                    retval = 2;
                    break;
                case float f when f > 12 && f <= 13.6:
                    retval = 1;
                    break;
                case float f when f > 16.8:
                    retval = 2;
                    break;
                default:
                    break;
            }
            if (retval == 2) return retval;

            PowerStatus pwr = SystemInformation.PowerStatus;
            switch (pwr.BatteryLifePercent)
            {
                case float f when f <= 0.1:
                    retval = 2;
                    break;
                case float f when f > 0.1 && f <= 0.2:
                    retval = 1;
                    break;
                default:
                    break;
            }
            return retval;
        }

        private void annunciator1_buttonClicked(object sender, EventArgs e)
        {

            //Check if we are in the right vehicle, if not then switch vehicle
            var s = (annunciator)sender;


            if (s.SysID != 0)
            {
                //If SysID os zero, then we does not have to update anything.
                if (s.SysID != Host.comPort.sysidcurrent)
                {
                    foreach (var port in MainV2.Comports)
                    {
                        if (port.sysidcurrent == s.SysID)
                        {
                            MainV2.comPort = port;
                            MainV2.comPort.sysidcurrent = s.SysID;
                            MainV2.comPort.compidcurrent = 1; //Always do the vehicle  
                            MainV2.instance.FlightPlanner.UpdateVehicleMissionOnScreen(s.SysID);  //Notify FlightPlanner that we changed vehicle
                            MainV2.View.Reload();
                        }

                        aMain1.Active = false;
                        aMain2.Active = false;
                        aMain3.Active = false;
                        s.Active = true;
                    }
                }
            }

            //We also assume that all three controls are named and sorted on the same way, so no need to differentiate
            switch (s.clickedButtonName)
            {
                case "FD":
                    MainV2.instance.MyView.ShowScreen("FlightData");
                    break;
                case "FP":
                    if (!MainV2.instance.FlightData.GetDropoutState("FlightPlanner")) MainV2.instance.MyView.ShowScreen("FlightPlanner");
                    MainV2.instance.FlightPlanner.cmb_missiontype.SelectedIndex = 0;
                    break;
                case "GF":
                    if (!MainV2.instance.FlightData.GetDropoutState("FlightPlanner")) MainV2.instance.MyView.ShowScreen("FlightPlanner");
                    MainV2.instance.FlightPlanner.cmb_missiontype.SelectedIndex = 1;

                    break;
                case "SETUP":
                    MainV2.instance.MyView.ShowScreen("HWConfig");
                    actualPanel = "SETUP";
                    break;

                case "TUNING":
                    MainV2.instance.MyView.ShowScreen("SWConfig");
                    actualPanel = "TUNING";
                    break;

                case "PAYLD":
                    {
                        TabPage tobeSelected = Host.MainForm.FlightData.tabControlactions.TabPages["payLoadCTRTab"];
                        if (tobeSelected != null) Host.MainForm.FlightData.tabControlactions.SelectedTab = tobeSelected;
                        break;
                    }
                case "ENGINE":
                    {
                        TabPage tobeSelected = Host.MainForm.FlightData.tabControlactions.TabPages["engCtrlTab"];
                        if (tobeSelected != null) Host.MainForm.FlightData.tabControlactions.SelectedTab = tobeSelected;
                        break;
                    }
                case "COMMS":
                    {
                        TabPage tobeSelected = Host.MainForm.FlightData.tabControlactions.TabPages["commsTab"];
                        if (tobeSelected != null) Host.MainForm.FlightData.tabControlactions.SelectedTab = tobeSelected;
                        break;
                    }
                case "MSG":
                    {
                        TabPage tobeSelected = Host.MainForm.FlightData.tabControlactions.TabPages["colorMsgTab"];
                        if (tobeSelected != null) Host.MainForm.FlightData.tabControlactions.SelectedTab = tobeSelected;
                        aMain1.setStatus("MSG", Stat.NOMINAL);
                        aMain2.setStatus("MSG", Stat.NOMINAL);
                        aMain3.setStatus("MSG", Stat.NOMINAL);
                        break;
                    }
                case "BATT":
                    {
                        TabPage tobeSelected = Host.MainForm.FlightData.tabControlactions.TabPages["battTab"];
                        if (tobeSelected != null) Host.MainForm.FlightData.tabControlactions.SelectedTab = tobeSelected;
                        break;
                    }
                case "DATA":
                    {
                        TabPage tobeSelected = Host.MainForm.FlightData.tabControlactions.TabPages["tabQuick"];
                        if (tobeSelected != null) Host.MainForm.FlightData.tabControlactions.SelectedTab = tobeSelected;
                        break;
                    }
                case "ACTIONS":
                    {
                        TabPage tobeSelected = Host.MainForm.FlightData.tabControlactions.TabPages["tabActions"];
                        if (tobeSelected != null) Host.MainForm.FlightData.tabControlactions.SelectedTab = tobeSelected;
                        break;
                    }
                case "EKF":
                    {
                        TabPage tobeSelected = Host.MainForm.FlightData.tabControlactions.TabPages["ekfTab"];
                        if (tobeSelected != null) Host.MainForm.FlightData.tabControlactions.SelectedTab = tobeSelected;
                        break;
                    }
                case "PITOT":
                    {
                        TabPage tobeSelected = Host.MainForm.FlightData.tabControlactions.TabPages["pitotTab"];
                        if (tobeSelected != null) Host.MainForm.FlightData.tabControlactions.SelectedTab = tobeSelected;
                        break;
                    }
                case "FUEL":
                    {
                        TabPage tobeSelected = Host.MainForm.FlightData.tabControlactions.TabPages["fuelTab"];
                        if (tobeSelected != null) Host.MainForm.FlightData.tabControlactions.SelectedTab = tobeSelected;
                        break;
                    }
                case "PREFLGHT":
                    {
                        TabPage tobeSelected = Host.MainForm.FlightData.tabControlactions.TabPages["tabPagePreFlight"];
                        if (tobeSelected != null) Host.MainForm.FlightData.tabControlactions.SelectedTab = tobeSelected;
                        break;
                    }
                case "WEATHER":
                    {
                        TabPage tobeSelected = Host.MainForm.FlightData.tabControlactions.TabPages["tabWeather"];
                        if (tobeSelected != null) Host.MainForm.FlightData.tabControlactions.SelectedTab = tobeSelected;
                        break;
                    }
                case "LAND":
                    {
                        TabPage tobeSelected = Host.MainForm.FlightData.tabControlactions.TabPages["landingTab"];
                        if (tobeSelected != null) Host.MainForm.FlightData.tabControlactions.SelectedTab = tobeSelected;
                        break;
                    }
                default:
                    break;
            }
        }

        private void annunciator1_undock(object sender, EventArgs e)
        {
            annunciatorForm = new FloatingForm();
            annunciatorForm.Text = "Annunciator";
            annunciatorForm.RestoreStartupLocation();
            MainV2.instance.panel1.Controls.Remove(aMain1);
            aMain1.isSingleLine = false;
            aMain1.Dock = DockStyle.None;
            aMain1.Visible = true;
            aMain1.contextMenuEnabled = false;
            aMain1.Size = annunciatorForm.Size;
            //annunciator1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            annunciatorForm.Controls.Add(aMain1);
            annunciatorForm.FormClosing += annunciator_FormClosing;
            //annunciatorUndocked = true;
            MainV2.instance.panel1.Dock = DockStyle.Top;
            MainV2.instance.panel1.Visible = false;
            aMain1.Invalidate();
            Settings.Instance["aMainDocked"] = "false";
            Settings.Instance.Save();
            annunciatorForm.Show();
        }

        void annunciator_FormClosing(object sender, FormClosingEventArgs e)
        {
            (sender as Form).SaveStartupLocation();
            aMain1.isSingleLine = true;
            aMain1.contextMenuEnabled = true;
            MainV2.instance.panel1.Controls.Add(aMain1);
            MainV2.instance.panel1.Dock = DockStyle.Top;
            MainV2.instance.panel1.Visible = true;
            aMain1.Size = MainV2.instance.panel1.Size;
            //annunciatorUndocked = false;
            Settings.Instance["aMainDocked"] = "true";
            Settings.Instance.Save();
            (sender as Form).Dispose();
        }



        //************** FCTB


        void setupFCTB()
        {
            // 
            // fctb
            // 
        //    this.fctb.AutoCompleteBracketsList = new char[] {
        //'(',
        //')',
        //'{',
        //'}',
        //'[',
        //']',
        //'\"',
        //'\"',
        //'\'',
        //'\''};
            this.fctb.AutoScrollMinSize = new System.Drawing.Size(25, 15);
            this.fctb.BackBrush = null;
            this.fctb.CharHeight = 15;
            this.fctb.CharWidth = 7;
            this.fctb.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.fctb.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.fctb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fctb.Font = new System.Drawing.Font("Consolas", 11F);
            this.fctb.IsReplaceMode = false;
            this.fctb.Location = new System.Drawing.Point(0, 0);
            this.fctb.Name = "fctb";
            this.fctb.Paddings = new System.Windows.Forms.Padding(1);
            this.fctb.ReadOnly = true;
            this.fctb.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.fctb.TabIndex = 5;
            this.fctb.Zoom = 100;
            this.fctb.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            this.fctb.ShowLineNumbers = false;
            this.fctb.BackColor = Color.Black;
            this.fctb.ShowScrollBars = true;
            this.fctb.BeginUpdate();
            this.fctb.TextSource.CurrentTB = fctb;
            this.fctb.AppendText("Messages starting", infoStyle);
            this.fctb.EndUpdate();
            
        }
        float wrap360(float noin)
        {
            if (noin < 0)
                return noin + 360;
            return noin;
        }


    }
}