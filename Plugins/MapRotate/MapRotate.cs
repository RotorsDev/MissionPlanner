using MissionPlanner;
using MissionPlanner.Plugin;
using MissionPlanner.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using MissionPlanner.Controls.PreFlight;
using MissionPlanner.Controls;
using System.Linq;
using GMap.NET.WindowsForms.Markers;
using MissionPlanner.Maps;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Drawing;
using System.Globalization;
using System.Net.Sockets;
using System.Net;
using System.Text;

namespace MapRotate
{
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

    public class MapRotate : Plugin
    {

        public situation sit = new situation();
        UdpClient udpClient;
        IPEndPoint udpEndPoint;
        internal GMapMarkerArrow markerFDcatapult;
        internal GMapMarkerArrow markerFPcatapult;

        internal GMapMarkerPlaneSitu markerPlane1;
        internal GMapMarkerPlaneSitu markerPlane2;
        internal GMapMarkerPlaneSitu markerPlane3;


        internal static GMapOverlay catapultFDOverlay;
        internal static GMapOverlay catapultFPOverlay;
        internal static GMapOverlay situationOverlay;

        public int maprotation = 0;
        public PointLatLngAlt catapultLocation;
       
        public override string Name
        {
            get { return "MapRotate/Catapult/Situational"; }
        }

        public override string Version
        {
            get { return "1.0"; }
        }

        public override string Author
        {
            get { return "Andras Schaffer"; }
        }

        //[DebuggerHidden]
        public override bool Init()
        {
            catapultFDOverlay = new GMapOverlay();
            catapultFPOverlay = new GMapOverlay();
            situationOverlay = new GMapOverlay();   

            loopratehz = 1;
            return true;
        }


       public override bool Loaded()
        {

            ToolStripMenuItem setRotateMenuItem = new ToolStripMenuItem() { Text = "Set Map Rotation" };
            setRotateMenuItem.Click += setRotate_Click;
            Host.FPMenuMap.Items.Add(setRotateMenuItem);
            Host.FDMenuMap.Items.Add(setRotateMenuItem);

            ToolStripMenuItem setCatapultLocationMenuItem = new ToolStripMenuItem() { Text = "Set Catapult Location" };
            setCatapultLocationMenuItem.Click += SetCatapultLocationMenuItem_Click;
            Host.FPMenuMap.Items.Add(setCatapultLocationMenuItem);
            Host.FDMenuMap.Items.Add(setCatapultLocationMenuItem);

            if (!isSupervisor())
            {
                // Setup UDP broadcast listener
                udpEndPoint = new IPEndPoint(IPAddress.Any, 19729);
                udpClient = new UdpClient(19729);
                udpClient.BeginReceive(new AsyncCallback(ProcessMessage), null);

                Host.FDGMapControl.Overlays.Add(situationOverlay);
            }
            return true;
        }

        //Returns true if this is a supervisor station, if there is no setting then it returns false by default
        public bool isSupervisor()
        {
            bool val = Settings.Instance.GetBoolean("Protar_Supervisor", false);
            return val;
        }

        private void ProcessMessage(IAsyncResult result)
        {
            // Get message
            string message = Encoding.UTF8.GetString(udpClient.EndReceive(result, ref udpEndPoint));
            // Restart listener
            udpClient.BeginReceive(new AsyncCallback(ProcessMessage), null);
            // Log message
            
            //Check if this is a valid message
            if (message.Contains("sysid1"))
            {
                Console.WriteLine("UDP broadcast on port (19729){0}",message);
                situation sit = new situation();
                sit = message.FromJSON<situation>();

                situationOverlay.Markers.Clear();

                if (sit.pos1 != null && sit.sysid1 != Host.comPort.sysidcurrent)
                {
                    GMapMarkerPlaneSitu markerPlane1 = new GMapMarkerPlaneSitu(sit.sysid1, sit.pos1, sit.heading1, sit.airspeed1);
                    situationOverlay.Markers.Add(markerPlane1);
                }
                if (sit.pos2 != null && sit.sysid2 != Host.comPort.sysidcurrent)
                {
                    GMapMarkerPlaneSitu markerPlane1 = new GMapMarkerPlaneSitu(sit.sysid2, sit.pos2, sit.heading2, sit.airspeed2);
                    situationOverlay.Markers.Add(markerPlane1);
                }
                if (sit.pos3 != null && sit.sysid3 != Host.comPort.sysidcurrent)
                {
                    GMapMarkerPlaneSitu markerPlane1 = new GMapMarkerPlaneSitu(sit.sysid3, sit.pos3, sit.heading3, sit.airspeed3);
                    situationOverlay.Markers.Add(markerPlane1);
                }


            }


        }

        private void SetCatapultLocationMenuItem_Click(object sender, EventArgs e)
        {

            var location = "";
            location = Host.FDMenuMapPosition.Lat.ToString() + ";" + Host.FDMenuMapPosition.Lng.ToString() + ";0";
            InputBox.Show("Enter Catpult Coords", "Please enter the coords 'lat;long;bearing'", ref location);
            var split = location.Split(';');

            if (split.Length == 3)
            {
                var lat = float.Parse(split[0], CultureInfo.InvariantCulture);
                var lng = float.Parse(split[1], CultureInfo.InvariantCulture);
                var bearing = float.Parse(split[2], CultureInfo.InvariantCulture);

                markerFDcatapult = new GMapMarkerArrow(new PointLatLng(lat, lng), bearing);
                markerFPcatapult = new GMapMarkerArrow(new PointLatLng(lat, lng), bearing);


                catapultFDOverlay.Markers.Clear();
                catapultFPOverlay.Markers.Clear();

                catapultLocation = Host.FDMenuMapPosition;

                catapultFDOverlay.Markers.Add(markerFDcatapult);
                catapultFPOverlay.Markers.Add(markerFPcatapult);

                Host.FDGMapControl.Overlays.Add(catapultFDOverlay);
                Host.FPGMapControl.Overlays.Add(catapultFPOverlay);

                Host.FDGMapControl.Refresh();
            }
            else
            {
                CustomMessageBox.Show("Invalid position!");
            }

        }

        
        private void setRotate_Click(object sender, EventArgs e)
        {
            string txt = "";
            if (DialogResult.Cancel == InputBox.Show("Map rotation", "Rotate maps to bearing ", ref txt))
                return;
            int result = 0;
            if (Int32.TryParse(txt,out result))
            {
                maprotation = result;
            }

            MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                Host.FDGMapControl.Bearing = maprotation;
                Host.FPGMapControl.Bearing = maprotation;
            }));
        }

        public override bool Loop()
        {
            return true;
        }

        public override bool Exit()
        {
            return true;
        }

    }
}