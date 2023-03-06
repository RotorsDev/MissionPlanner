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

namespace GMap.NET.WindowsForms
{
    //public class GMapMarkerCatapult : GMapMarker
    //{
    //    public PointLatLng Location { get; set; }
    //    public int Heading { get; set; }


    //    public GMapMarkerCatapult(PointLatLng p, int heading) : base(p)
    //    {
    //        this.Location = p;
    //        this.Heading = heading;
    //    }

    //    public override void OnRender(System.IGraphics g)
    //    {
    //        base.OnRender(g);
    //    }
    //}

}

namespace MapRotate
{


    public class MapRotate : Plugin
    {
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



            return true;
        }

        private void SetCatapultLocationMenuItem_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
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