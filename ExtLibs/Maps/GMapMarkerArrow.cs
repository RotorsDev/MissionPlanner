using GMap.NET;
using GMap.NET.WindowsForms;
using System;
using System.Drawing;

namespace MissionPlanner.Maps
{
    public class GMapMarkerArrow: GMapMarker
    {
        public float Bearing = 0;

        public GMapMarkerArrow(PointLatLng p, float Bearing)
           : base(p)
        {
            this.Bearing = Bearing;
            Offset = new Point(0, 0);
        
        }

        

        static readonly Point[] Arrow = new Point[] { new Point(-12, 12), new Point(0, -12), new Point(12, 12)/*, new Point(0, 2)*/ };

        public override void OnRender(IGraphics g)
        {
            if (Math.Abs(LocalPosition.X) > 100000 || Math.Abs(LocalPosition.Y) > 100000)
                return;

            //if(Overlay.Control.Zoom < 16)
            //    return;


            var old = g.Transform;

            g.TranslateTransform(this.LocalPosition.X - this.Offset.X, this.LocalPosition.Y - this.Offset.Y);
            g.RotateTransform(Bearing - Overlay.Control.Bearing);
            g.DrawLines(new Pen(Color.Pink,2),Arrow);
            g.Transform = old;
        }
    }
}