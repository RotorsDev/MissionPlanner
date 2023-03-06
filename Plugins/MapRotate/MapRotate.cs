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

namespace MapRotate
{


    public class MapRotate : Plugin
    {

        internal GMapMarkerArrow markerFDcatapult;
        internal GMapMarkerArrow markerFPcatapult;

        internal static GMapOverlay catapultFDOverlay;
        internal static GMapOverlay catapultFPOverlay;

        public int maprotation = 0;
        public PointLatLngAlt catapultLocation;
       
        public override string Name
        {
            get { return "MapRotate/Catapult"; }
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


            return true;
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