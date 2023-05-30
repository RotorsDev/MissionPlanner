using System;
using System.Collections.Generic;
using System.Drawing;
using GMap.NET;
using GMap.NET.WindowsForms;
using MissionPlanner.Utilities;
using GMap.NET.WindowsForms.Markers;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace MissionPlanner.Maps
{
    [Serializable]
    public class GMapMarkerPlaneSitu : GMapMarkerBase
    {
        private readonly Bitmap icon = global::MissionPlanner.Maps.Resources.planeicon;

        static SolidBrush shadow = new SolidBrush(Color.FromArgb(50, Color.Black));

        static Point[] plane = new Point[] {
            new Point(28,0),
            new Point(32,13),
            new Point(53,27),
            new Point(55,32),
            new Point(31,28),
            new Point(30,35),
            new Point(30,43),
            new Point(37,48),
            new Point(37,50),
            new Point(29,50),
            new Point(29,53),
            // inverse
            new Point(inv(29,28),53),
            new Point(inv(29,28),50),
            new Point(inv(37,28),50),
            new Point(inv(37,28),48),
            new Point(inv(30,28),43),
            new Point(inv(30,28),35),
            new Point(inv(31,28),28),
            new Point(inv(55,28),32),
            new Point(inv(53,28),27),
            new Point(inv(32,28),13),
            new Point(inv(28,28),0),
            };


        SizeF txtsize = SizeF.Empty;
        static Dictionary<string, Bitmap> fontBitmaps = new Dictionary<string, Bitmap>();
        static Font font;

        private static int inv(int input, int mid)
        {
            var delta = input - mid;

            return mid - delta;
        }

        float cog = -1;
        float heading = 0;
        float nav_bearing = -1;
        float radius = -1;
        float target = -1;
        public int which = 0;

        public GMapMarkerPlaneSitu(int which, PointLatLng p, float heading, float speed)
            : base(p)
        {
            this.heading = heading;
            this.which = which;
            Size = icon.Size;

            if (font == null)
            {
                font = SystemFonts.DefaultFont;
            }
            string wpno = which.ToString();

            if (!fontBitmaps.ContainsKey(wpno))
            {
                Bitmap temp = new Bitmap(100, 40, PixelFormat.Format32bppArgb);
                using (Graphics g = Graphics.FromImage(temp))
                {
                    g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    txtsize = g.MeasureString(wpno, font);

                    g.DrawString(wpno, font, Brushes.Black, new PointF(0, 0));
                }
                fontBitmaps[wpno] = temp;
            }


        }

        public float Heading { get => heading; set => heading = value; }
        public override void OnRender(IGraphics g)
        {
            var temp = g.Transform;
            g.TranslateTransform(LocalPosition.X, LocalPosition.Y);

            g.RotateTransform(-Overlay.Control.Bearing);

            // anti NaN

            try
            {
                g.RotateTransform(heading);
            }
            catch
            {
            }

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            // the shadow
            g.TranslateTransform(-26, -26);

            //g.FillPolygon(shadow, plane);

            // the plane
            g.TranslateTransform(-2, -2);

            Color c = Color.FromArgb(180, Color.LightBlue);
            g.FillPolygon(new SolidBrush(c), plane);

            g.DrawImageUnscaled(fontBitmaps[which.ToString()], 23, 15);
            g.Transform = temp;


        }
    }
}