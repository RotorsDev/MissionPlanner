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
        public float WindDirection = 0;

        public PointLatLngAlt LandingPoint = new PointLatLngAlt();
        public PointLatLngAlt WaitingPoint = new PointLatLngAlt();
        public PointLatLngAlt TargetPoint = new PointLatLngAlt();
        public PointLatLngAlt WaitingPointTangent = new PointLatLngAlt();

        public LandState state = LandState.None;


        public event EventHandler waitClicked;
        public event EventHandler landClicked;
        public event EventHandler setspeedClicked;

        protected virtual void OnSpeedClicked(EventArgs e)
        {
            EventHandler handler = this.setspeedClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnWaitClicked(EventArgs e)
        {
            EventHandler handler = this.waitClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnLandClicked(EventArgs e)
        {
            EventHandler handler = this.landClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public void updateLabels()
        {
            //Update labels as well
            lWaitDist.Text = WaitDistance.ToString();
            lLandingSpeed.Text = LandingSpeed.ToString();
            lOpeningTime.Text = OpeningTime.ToString("F1");
            lSinkRate.Text = SinkRate.ToString("F1");
            lLandAlt.Text = LandingAlt.ToString();
            lWindDrag.Text = WindDrag.ToString("F1");

        }

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
            updateLabels();


            mWaitDistance.Button.Click += Button_Click;
            mLandingAlt.Button.Click += Button_Click1;
            mLandSpeed.Button.Click += Button_Click2;
            mOPeningTime.Button.Click += Button_Click3;
            mSInkRate.Button.Click += Button_Click4;
            mWindDrag.Button.Click += Button_Click5;

        }

        private void Button_Click5(object sender, EventArgs e)
        {
            WindDrag = ((float)mWindDrag.NumericUpDown.Value) / 10;
            updateLabels();
        }

        private void Button_Click4(object sender, EventArgs e)
        {
            SinkRate = ((float)mSInkRate.NumericUpDown.Value) / 10;
            updateLabels();
        }

        private void Button_Click3(object sender, EventArgs e)
        {
            OpeningTime = ((float)mOPeningTime.NumericUpDown.Value) / 10;
            updateLabels();
        }

        private void Button_Click2(object sender, EventArgs e)
        {
            LandingSpeed = (int)mLandSpeed.NumericUpDown.Value;
            updateLabels();
        }

        private void Button_Click1(object sender, EventArgs e)
        {
            LandingAlt = (int)mLandingAlt.NumericUpDown.Value;
            updateLabels();
        }

        private void Button_Click(object sender, EventArgs e)
        {
            WaitDistance = (int)mWaitDistance.NumericUpDown.Value;
            state = LandState.None;
            updateLabels();
        }

        public void updateLandingData(PointLatLngAlt l, float WindDir, float WindSpeed, int LoiterRadius)
        {
            WindDir = 49;

            l.Alt = LandingAlt;
            LandingPoint = l;

            WaitingPointTangent = l.newpos(wrap360(WindDir-180), WaitDistance);
            WaitingPoint = WaitingPointTangent.newpos(wrap360(WindDir + 90), LoiterRadius + 30);
            TargetPoint = l.newpos(WindDir, 2000);
            lLandPoint.Text = l.Lat.ToString("F5") + "," + l.Lng.ToString("F5");
            WindDirection = WindDir;


        }
        float wrap360(float noin)
        {
            if (noin < 0)
                return noin + 360;
            return noin;
        }

        private void mWaitDistance_Load(object sender, EventArgs e)
        {

        }

        private void myButton1_Click(object sender, EventArgs e)
        {
            this.OnWaitClicked(EventArgs.Empty);
        }

        private void myButton2_Click(object sender, EventArgs e)
        {
            this.OnLandClicked(EventArgs.Empty);
        }

        private void bSetLandingSpeed_Click(object sender, EventArgs e)
        {
            this.OnSpeedClicked(EventArgs.Empty);
        }
    }
}
