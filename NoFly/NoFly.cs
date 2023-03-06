using GeoJSON.Net;
using GMap.NET;
using GMap.NET.WindowsForms;
using Ionic.Zip;
using MissionPlanner.Maps;
using MissionPlanner.Utilities;
using SharpKml.Dom;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace MissionPlanner.NoFly
{
    public class NoFly
    {
        private const int proximity = 100000;

        static GMapOverlay kmlpolygonsoverlay = new GMapOverlay();

        private static string directory = Settings.GetRunningDirectory() + "NoFly";

        public static event EventHandler<NoFlyEventArgs> NoFlyEvent;

        public class NoFlyEventArgs : EventArgs
        {
            public NoFlyEventArgs(GMapOverlay overlay)
            {
                NoFlyZones = overlay;
            }

            public GMapOverlay NoFlyZones { get; set; }
        }

        public static void Scan()
        {
            if (!Settings.Instance.GetBoolean("ShowNoFly", true))
                return;

            var files = Directory.GetFiles(directory, "*.kmz");

            foreach (var file in files)
            {
                try
                {
                    // get a temp dir
                    var outputDirectory = Path.GetTempPath() + Path.DirectorySeparatorChar + "mpkml" +
                                          DateTime.Now.Ticks;
                    using (var zip = ZipFile.Read(File.OpenRead(file)))
                    {
                        zip.ExtractAll(outputDirectory, ExtractExistingFileAction.OverwriteSilently);
                    }

                    var kmls = Directory.GetFiles(outputDirectory, "*.kml");
                    foreach (var kml in kmls)
                    {
                        LoadNoFly(kml);
                    }

                    Directory.Delete(outputDirectory, true);
                }
                catch
                {
                }
            }

            if (NoFlyEvent != null)
                NoFlyEvent(null, new NoFlyEventArgs(kmlpolygonsoverlay));
        }

        static PointLatLngAlt lastUpdateLocation = PointLatLngAlt.Zero;
        public static void UpdateNoFlyZone(object sender, PointLatLngAlt plla)
        {
            if (plla.GetDistance(lastUpdateLocation) > 100)
            {
                UpdateNoFlyZoneEvent?.Invoke(sender, plla);
                lastUpdateLocation = plla;
            }
        }

        public static event EventHandler<PointLatLngAlt> UpdateNoFlyZoneEvent;

        public static void LoadNoFly(string file)
        {
            string kml = "";

            using (var sr = new StreamReader(File.OpenRead(file)))
            {
                kml = sr.ReadToEnd();
                sr.Close();
            }

            kml = kml.Replace("<Snippet/>", "");

            var parser = new SharpKml.Base.Parser();

            parser.ElementAdded += parser_ElementAdded;
            parser.ParseString(kml, false);
        }

        static void parser_ElementAdded(object sender, SharpKml.Base.ElementEventArgs e)
        {
            processKML(e.Element);
        }

        private static void processKML(SharpKml.Dom.Element Element)
        {
            try
            {
                //  log.Info(Element.ToString() + " " + Element.Parent);
            }
            catch
            {
            }

            SharpKml.Dom.Document doc = Element as SharpKml.Dom.Document;
            SharpKml.Dom.Placemark pm = Element as SharpKml.Dom.Placemark;
            SharpKml.Dom.Folder folder = Element as SharpKml.Dom.Folder;
            SharpKml.Dom.Polygon polygon = Element as SharpKml.Dom.Polygon;
            SharpKml.Dom.LineString ls = Element as SharpKml.Dom.LineString;
            MultipleGeometry geom = Element as MultipleGeometry;

            if (doc != null)
            {
                foreach (var feat in doc.Features)
                {
                    //Console.WriteLine("feat " + feat.GetType());
                    //processKML((Element)feat);
                }
            }
            else if (folder != null)
            {
                foreach (SharpKml.Dom.Feature feat in folder.Features)
                {
                    //Console.WriteLine("feat "+feat.GetType());
                    //processKML(feat);
                }
            }
            else if (pm != null)
            {
            }
            else if (polygon != null)
            {
                GMapPolygon kmlpolygon = new GMapPolygon(new List<PointLatLng>(), polygon.Id);

                kmlpolygon.Stroke.Color = Color.Purple;
                kmlpolygon.Stroke.Width = 2;

                kmlpolygon.Fill = new SolidBrush(Color.FromArgb(30, Color.Blue));

                foreach (var loc in polygon.OuterBoundary.LinearRing.Coordinates)
                {
                    kmlpolygon.Points.Add(new PointLatLng(loc.Latitude, loc.Longitude));
                }

                kmlpolygonsoverlay.Polygons.Add(kmlpolygon);
            }
            else if (ls != null)
            {
                GMapRoute kmlroute = new GMapRoute(new List<PointLatLng>(), "kmlroute");

                kmlroute.Stroke.Color = Color.Purple;

                foreach (var loc in ls.Coordinates)
                {
                    kmlroute.Points.Add(new PointLatLng(loc.Latitude, loc.Longitude));
                }

                kmlpolygonsoverlay.Routes.Add(kmlroute);
            }
            else if (geom != null)
            {
                foreach (var geometry in geom.Geometry)
                {
                    processKML(geometry);
                }
            }
        }
    }
}