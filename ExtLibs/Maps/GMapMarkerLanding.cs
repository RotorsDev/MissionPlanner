using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.WindowsForms;
using System.Drawing;
using MissionPlanner.Utilities;

namespace MissionPlanner.Maps
{
    [Serializable]
    public class GMapMarkerLanding : GMapMarker
    {

        public float Bearing {get; set;}
        public int Length { get; set; }
        static Bitmap localcache2 = Resources.marker_02;

        public GMapMarkerLanding(PointLatLng pos) : base(pos)
        {
        
        }


        public override void OnRender(IGraphics g)
        {
            var temp = g.Transform;
            g.TranslateTransform(LocalPosition.X, LocalPosition.Y);

            g.RotateTransform(-Overlay.Control.Bearing);

            g.DrawImageUnscaled(localcache2, 0,0);

            double width =
                       (Overlay.Control.MapProvider.Projection.GetDistance(Overlay.Control.FromLocalToLatLng(0, 0),
                            Overlay.Control.FromLocalToLatLng(Overlay.Control.Width, 0)) * 1000.0);
            double m2pixelwidth = Overlay.Control.Width / width;
            var scaledradius = Length * (float)m2pixelwidth;

            g.DrawLine(new Pen(Color.Green, 4), 0f, 0f,
                (float)Math.Cos((Bearing - 90) * MathHelper.deg2rad) * scaledradius,
                (float)Math.Sin((Bearing - 90) * MathHelper.deg2rad) * scaledradius);

            g.RotateTransform(Bearing);
            g.Transform = temp;
        }

    }
}
