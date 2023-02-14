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


//By Bandi
namespace ptPlugin1
{
    [PreventTheming]
    public class ptPlugin1 : Plugin
    {

        annunciator aMain = new annunciator(18, new Size(130,40));
        public static FloatingForm annunciatorForm = new FloatingForm();



        public TabPage payloadControlpage = new TabPage();
        public payloadcontrol plControl = new payloadcontrol();


        public TabPage engineControlPage = new TabPage();
        public engineControl eCtrl = new engineControl();



        public List<Payload> payloadSettings = new List<Payload>();
        payloadSetupForm fPs;

        string actualPanel = "";

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
            loopratehz = 1;  //Loop runs every second (The value is in Hertz, so 2 means every 500ms, 0.1f means every 10 second...) 

            return true;	 // If it is false then plugin will not load
        }

        public override bool Loaded()
		//Loaded called after the plugin dll successfully loaded
        {

            Panel panel1 = Host.MainForm.Controls.Find("Panel1", true).FirstOrDefault() as Panel;


            string[] btnLabels = new string[] { 
                "FLIGHT"+ Environment.NewLine +"DATA",
                "FLIGHT" + Environment.NewLine +"PLAN",
                "GEO"+ Environment.NewLine +"FENCE",
                "SETUP",
                "ENGINE",
                "FUEL",
                "PAY"+ Environment.NewLine +"LOAD",
                "AIR"+ Environment.NewLine +"SPEED",
                "DUMMY",
                "MAG",
                "DUMMY",
                "DUMMY",
                "PARA"+ Environment.NewLine +"CHUTE",
                "PRE"+ Environment.NewLine +"FLIGHT",
                "START",
                "MSG",
                "DUMMY",
                "DUMMY" };

            string[] btnNames = new string[] { 
                "FD",
                "FP",
                "GF",
                "SETUP", 
                "ENGINE",
                "FUEL",
                "PAYLD",
                "AIRSPD",
                "DUMMY1",
                "MAG",
                "DUMMY2",
                "DUMMY3",
                "CHUTE",
                "PRFLT",
                "START",
                "MSG",
                "DUMMY4",
                "DUMMY5" };
            aMain.setPanels(btnNames, btnLabels);

            //Setup initial button status
            aMain.setStatus("FD", Stat.NOMINAL);
            aMain.setStatus("FP", Stat.NOMINAL);
            aMain.setStatus("GF", Stat.NOMINAL);
            aMain.setStatus("SETUP", Stat.NOMINAL);
            aMain.setStatus("PAYL", Stat.NOMINAL);
            aMain.setStatus("ENGINE", Stat.NOMINAL);





            aMain.Enabled = true;
            aMain.Location = new Point(0, 0);
            aMain.Size = new Size(panel1.Width, 47);
            aMain.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Bottom | AnchorStyles.Right;
            aMain.undock += annunciator1_undock;
            aMain.buttonClicked += annunciator1_buttonClicked;

            aMain.Visible = true;
            aMain.Refresh();

            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {

                MenuStrip MainMenu = Host.MainForm.Controls.Find("MainMenu", true).FirstOrDefault() as MenuStrip;
                if (MainMenu != null) MainMenu.Visible = false;

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
                    panel1.Controls.Add(aMain);

                }
            }





            payloadControlpage.Text = "PayloadControl";
            payloadControlpage.Name = "payLoadCTRTab";

            plControl.Name = "plControl";
            plControl.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            plControl.Location = new Point(0, 0);
            plControl.Size = new Size(payloadControlpage.Width, payloadControlpage.Height);
            plControl.setupClicked += PlControl_setupClicked;
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
            Host.MainForm.FlightData.tabControlactions.TabPages.Add(engineControlPage);

            eCtrl.setEngineStatus("Ready to Start", "No error");

            return true;     //If it is false plugin will not start (loop will not called)
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

        public override bool Loop()
		//Loop is called in regular intervalls (set by loopratehz)
        {
            return true;	//Return value is not used
        }

        public override bool Exit()
		//Exit called when plugin is terminated (usually when Mission Planner is exiting)
        {


            //Save position, we need to do it since formclose is not called in plugins

            annunciatorForm?.SaveStartupLocation();
            return true;	//Return value is not used
        }

        private void annunciator1_buttonClicked(object sender, EventArgs e)
        {

            switch (aMain.clickedButtonName)
            {
                case "FD":
                    MainV2.instance.MyView.ShowScreen("FlightData");
                    break;
                case "FP":
                    MainV2.instance.MyView.ShowScreen("FlightPlanner");
                    MainV2.instance.FlightPlanner.cmb_missiontype.SelectedIndex = 0;
                    break;
                case "GF":
                    MainV2.instance.MyView.ShowScreen("FlightPlanner");
                    MainV2.instance.FlightPlanner.cmb_missiontype.SelectedIndex = 1;

                    break;

                case "SETUP":
                    MainV2.instance.MyView.ShowScreen("SWConfig");
                    Console.WriteLine(MainV2.instance.FlightData.tabQuick);
                    actualPanel = "SETUP";
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
                default:
                    break;
            }
        }

        private void annunciator1_undock(object sender, EventArgs e)
        {
            annunciatorForm = new FloatingForm();
            annunciatorForm.Text = "Annunciator";
            //annunciatorForm.Size = new Size(300, 450);
            annunciatorForm.RestoreStartupLocation();
            MainV2.instance.panel1.Controls.Remove(aMain);
            aMain.isSingleLine = false;
            aMain.Dock = DockStyle.None;
            aMain.Visible = true;
            aMain.contextMenuEnabled = false;
            aMain.Size = annunciatorForm.Size;
            //annunciator1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            annunciatorForm.Controls.Add(aMain);
            annunciatorForm.FormClosing += annunciator_FormClosing;
            //annunciatorUndocked = true;
            MainV2.instance.panel1.Dock = DockStyle.Top;
            MainV2.instance.panel1.Visible = false;
            aMain.Invalidate();
            Settings.Instance["aMainDocked"] = "false";
            Settings.Instance.Save();
            annunciatorForm.Show();
        }

        void annunciator_FormClosing(object sender, FormClosingEventArgs e)
        {
            (sender as Form).SaveStartupLocation();
            aMain.isSingleLine = true;
            aMain.contextMenuEnabled = true;
            MainV2.instance.panel1.Controls.Add(aMain);
            MainV2.instance.panel1.Dock = DockStyle.Top;
            MainV2.instance.panel1.Visible = true;
            aMain.Size = MainV2.instance.panel1.Size;
            //annunciatorUndocked = false;
            Settings.Instance["aMainDocked"] = "true";
            Settings.Instance.Save();
            (sender as Form).Dispose();
        }



    }
}