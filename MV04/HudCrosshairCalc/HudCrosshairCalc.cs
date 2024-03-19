using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MissionPlanner.Utilities;

namespace MissionPlanner.MV04.HudCrosshairCalc
{
    internal static class HudCrosshairCalc
    {
        /// <summary>
        /// 3D distance between the camera and the target in meters
        /// </summary>
        internal static double TargetDistance(PointLatLngAlt cameraPos, PointLatLngAlt targetPos)
        {
            double distance_flat = cameraPos.GetDistance(targetPos);
            double distance_3d = Math.Sqrt(Math.Pow(distance_flat, 2) + Math.Pow(cameraPos.Alt, 2));
            // We disregard the difference between the distance of two points on a flat surface and a curved surface

            return distance_3d;
        }

        /// <summary>
        /// Camera view width for a given FOV at the target distance in meters
        /// </summary>
        internal static double FOVWidth(PointLatLngAlt cameraPos, PointLatLngAlt targetPos, int viewDegrees)
        {
            double distance = TargetDistance(cameraPos, targetPos);
            //double width = 2 * Math.Sqrt(Math.Pow(distance / Math.Cos(MathHelper.Radians(90 - (viewDegrees / 2))), 2) - Math.Pow(distance, 2));
            return 2 * distance * Math.Tan(MathHelper.Radians((double)viewDegrees / 2));
        }
    }
}
