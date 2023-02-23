using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MissionPlanner.Controls;
using MissionPlanner.Utilities;

namespace ptPlugin1
{
    public partial class landingControl : UserControl
    {

        public int WaitDistance = 2000;
        public int LandingSpeed = 36;
        public float OpeningTime = 1.8f;
        public float SinkRate = 5f;
        public int LandingAlt = 100;
        public float WindDrag = 0.9f;

        public PointLatLngAlt LandingPoint = new PointLatLngAlt();
        public PointLatLngAlt WaitingPoint = new PointLatLngAlt();
        public PointLatLngAlt TargetPoint = new PointLatLngAlt();


        public landingControl()
        {
            InitializeComponent();


            mWaitDistance.NumericUpDown.Maximum = 5000;

            mWaitDistance.NumericUpDown.Value = WaitDistance;
            mLandSpeed.NumericUpDown.Value = LandingSpeed;
            mOPeningTime.NumericUpDown.Value = (int)(OpeningTime * 10) ;
            mSInkRate.NumericUpDown.Value = (int)(SinkRate * 10);
            mLandingAlt.NumericUpDown.Value = LandingAlt;
            mWindDrag.NumericUpDown.Value = (int)(WindDrag * 10);


            //Update labels as well
            lWaitDist.Text = WaitDistance.ToString();
            lLandingSpeed.Text = LandingSpeed.ToString();
            lOpeningTime.Text = OpeningTime.ToString("F1");
            lSinkRate.Text = SinkRate.ToString("F1");
            lLandAlt.Text = LandingAlt.ToString();
            lWindDrag.Text = WindDrag.ToString("F1");



        }

        public void updateLandingData(PointLatLngAlt l, int WindDir, float WindSpeed)
        {

            l.Alt = LandingAlt;
            LandingPoint = l;

            WaitingPoint = l.newpos(WindDir, WaitDistance);

            TargetPoint = l.newpos(wrap360(180 - WindDir), 2000);

            lLandPoint.Text = l.Lat.ToString("F5") + "," + l.Lng.ToString("F5");


        }
        float wrap360(float noin)
        {
            if (noin < 0)
                return noin + 360;
            return noin;
        }

    }
}
