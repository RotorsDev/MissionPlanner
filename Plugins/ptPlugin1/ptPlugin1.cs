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
    public class ptPlugin1 : Plugin
    {

        annunciator aMain = new annunciator(18, new Size(130,40));
        public static FloatingForm annunciatorForm = new FloatingForm();



        public TabPage payloadControlpage = new TabPage();



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


            string[] btnLabels = new string[] { "FLIGHT"+ Environment.NewLine +"DATA", "FLIGHT" + Environment.NewLine +"PLAN", "GEO"+ Environment.NewLine +"FENCE", "SETUP", "COM", "VIBE", "FUEL", "GEO"+ Environment.NewLine +"FENCE",
                "AIR"+ Environment.NewLine +"SPEED", "MAG", "PAY"+ Environment.NewLine +"LOAD", "MISSION", "PARA"+ Environment.NewLine +"CHUTE",
                "PRE"+ Environment.NewLine +"FLIGHT", "START", "MSG","MAIN" + Environment.NewLine + "SCRN","DUMMY" };

            string[] btnNames = new string[] { "FD", "FP", "GF", "SETUP", "COMM", "VIBE", "FUEL", "FENCE", "AIRSPD", "MAG", "PAYLD", "ROUTE", "CHUTE", "PRFLT", "START", "MSG", "MAIN","DUMMY" };
            aMain.setPanels(btnNames, btnLabels);

            //Setup initial button status
            aMain.setStatus("FD", Stat.NOMINAL);
            aMain.setStatus("FP", Stat.NOMINAL);
            aMain.setStatus("GF", Stat.NOMINAL);
            aMain.setStatus("SETUP", Stat.NOMINAL);





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
            Host.MainForm.FlightData.tabControlactions.TabPages.Add(payloadControlpage);
            //MainV2.instance.tabpagesLoaded = true;

            return true;     //If it is false plugin will not start (loop will not called)
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
                    if (actualPanel != "FD")
                    {
                        MainV2.instance.MyView.ShowScreen("FlightData");
                        actualPanel = "FD";
                    }
                    break;
                case "FP":
                    if (actualPanel != "FP")
                    {

                        if (actualPanel == "FD")
                        {
                            //MainV2.instance.FlightData.flightPlannerToolStripMenuItem_Click(null, EventArgs.Empty);
                            //// MainV2.instance.FlightPlanner.BUT_read_Click(null, EventArgs.Empty); TODO: When to read actual mission plan
                            MainV2.instance.MyView.ShowScreen("FlightPlanner");
                            MainV2.instance.FlightPlanner.cmb_missiontype.SelectedIndex = 0;


                        }
                        else if (actualPanel == "GF")
                        {
                            MainV2.instance.FlightPlanner.cmb_missiontype.SelectedIndex = 0;
                        }
                        actualPanel = "FP";
                    }

                    break;
                case "GF":
                    if (actualPanel != "GF")
                    {

                        if (actualPanel == "FD")
                        {
                            MainV2.instance.MyView.ShowScreen("FlightPlanner");
                            MainV2.instance.FlightPlanner.cmb_missiontype.SelectedIndex = 1;
                            // MainV2.instance.FlightPlanner.BUT_read_Click(null, EventArgs.Empty); TODO: When to read actual mission plan

                        }
                        else if (actualPanel == "FP")
                        {
                            MainV2.instance.FlightPlanner.cmb_missiontype.SelectedIndex = 1;
                        }
                        actualPanel = "GF";
                    }

                    break;

                case "SETUP":
                    MainV2.instance.MyView.ShowScreen("SWConfig");
                    Console.WriteLine(MainV2.instance.FlightData.tabQuick);
                    actualPanel = "SETUP";
                    break;

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