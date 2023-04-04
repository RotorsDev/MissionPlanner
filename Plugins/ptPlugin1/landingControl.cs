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
    [PreventTheming]
    public partial class landingControl : UserControl
    {

        public int WaitDistance = 1000;
        public int LandingSpeed = 42;
        public float OpeningTime = 1.8f;
        public float SinkRate = 5f;
        public int LandingAlt = 100;
        public float WindDrag = 1.2f;
        public float WindDirection = 0;
        public ChuteState chute = ChuteState.AutoOpenDisabled;

        public PointLatLngAlt LandingPoint = new PointLatLngAlt();
        public PointLatLngAlt WaitingPoint = new PointLatLngAlt();
        public PointLatLngAlt TargetPoint = new PointLatLngAlt();
        public PointLatLngAlt WaitingPointTangent = new PointLatLngAlt();

        public LandState state = LandState.None;


        public event EventHandler StartLandingClicked;
        public event EventHandler landClicked;
        public event EventHandler setspeedClicked;
        public event EventHandler setCruiseSpeedClicked;
        public event EventHandler abortLandingClicked;
        public event EventHandler nudgeSpeedClicked;

        protected virtual void OnNudgeSpeedClicked(EventArgs e)
        {
            EventHandler handler = this.nudgeSpeedClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        protected virtual void OnAbortLandingClicked(EventArgs e)
        {
            EventHandler handler = this.abortLandingClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        protected virtual void OnSpeedClicked(EventArgs e)
        {
            EventHandler handler = this.setspeedClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnsetCruiseSpeedClicked(EventArgs e)
        {
            EventHandler handler = this.setCruiseSpeedClicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnStartLandingClicked(EventArgs e)
        {
            EventHandler handler = this.StartLandingClicked;
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

            //instead of labels, we update the color of the label
            if (mWaitDistance.NumericUpDown.Value != WaitDistance)
                mWaitDistance.NumericUpDown.BackColor = Color.Red;
            else
                mWaitDistance.NumericUpDown.BackColor = Color.Green;

            if (mLandSpeed.NumericUpDown.Value != LandingSpeed)
                mLandSpeed.NumericUpDown.BackColor = Color.Red;
            else
                mLandSpeed.NumericUpDown.BackColor = Color.Green;

            if (mOPeningTime.NumericUpDown.Value != (int)(OpeningTime*10))
                mOPeningTime.NumericUpDown.BackColor = Color.Red;
            else
                mOPeningTime.NumericUpDown.BackColor = Color.Green;

            if (mSInkRate.NumericUpDown.Value != (int)(SinkRate * 10))
                mSInkRate.NumericUpDown.BackColor = Color.Red;
            else
                mSInkRate.NumericUpDown.BackColor = Color.Green;

            if (mLandingAlt.NumericUpDown.Value != LandingAlt)
                mLandingAlt.NumericUpDown.BackColor = Color.Red;
            else
                mLandingAlt.NumericUpDown.BackColor = Color.Green;

            if (mWindDrag.NumericUpDown.Value != (int)(WindDrag * 10))
                mWindDrag.NumericUpDown.BackColor = Color.Red;
            else
                mWindDrag.NumericUpDown.BackColor = Color.Green;


            if (chute == ChuteState.AutoOpenDisabled)
            {
                buttonEnableChuteOpen.Text = "ENABLE Auto Chute open";
                lLeftStatus.Text = "Auto Chute Open DISABLED";
                lLeftStatus.BackColor = Color.Red;
            }
            else
            {
                buttonEnableChuteOpen.Text = "DISABLE Auto Chute open";
                lLeftStatus.Text = "Auto Chute Open ENABLED";
                lLeftStatus.BackColor = Color.Green;
            }

            lRightStatus.Text = state.ToString();

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

            mWaitDistance.Button.Click += bWaitDistanceSet_clicked;
            mLandingAlt.Button.Click += bLandingAltSet_clicked;
            mLandSpeed.Button.Click += bLandingSpeedSet_clicked;
            mOPeningTime.Button.Click += bOpeningTimeSet_clicked;
            mSInkRate.Button.Click += bSinkRateSet_clicked;
            mWindDrag.Button.Click += bWindDragSet_clicked;

            mWaitDistance.NumericUpDown.ValueChanged += NumericUpDown_ValueChanged;
            mLandingAlt.NumericUpDown.ValueChanged += NumericUpDown_ValueChanged;
            mLandSpeed.NumericUpDown.ValueChanged += NumericUpDown_ValueChanged;
            mOPeningTime.NumericUpDown.ValueChanged += NumericUpDown_ValueChanged;
            mSInkRate.NumericUpDown.ValueChanged += NumericUpDown_ValueChanged;
            mWindDrag.NumericUpDown.ValueChanged += NumericUpDown_ValueChanged;




        }

        private void NumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            var s = sender as NumericUpDown;
            s.BackColor = Color.Red;
        }

        private void bWindDragSet_clicked(object sender, EventArgs e)
        {
            WindDrag = ((float)mWindDrag.NumericUpDown.Value) / 10;
            updateLabels();
        }

        private void bSinkRateSet_clicked(object sender, EventArgs e)
        {
            SinkRate = ((float)mSInkRate.NumericUpDown.Value) / 10;
            updateLabels();
        }

        private void bOpeningTimeSet_clicked(object sender, EventArgs e)
        {
            OpeningTime = ((float)mOPeningTime.NumericUpDown.Value) / 10;
            updateLabels();
        }

        private void bLandingSpeedSet_clicked(object sender, EventArgs e)
        {
            LandingSpeed = (int)mLandSpeed.NumericUpDown.Value;
            updateLabels();
        }

        private void bLandingAltSet_clicked(object sender, EventArgs e)
        {
            LandingAlt = (int)mLandingAlt.NumericUpDown.Value;
            updateLabels();
        }

        private void bWaitDistanceSet_clicked(object sender, EventArgs e)
        {
            WaitDistance = (int)mWaitDistance.NumericUpDown.Value;
            state = LandState.None;
            updateLabels();
        }

        public void updateLandingData(PointLatLngAlt l, float WindDir, float WindSpeed, int LoiterRadius)
        {
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

        private void bStartLanding_Click(object sender, EventArgs e)
        {
            this.OnStartLandingClicked(EventArgs.Empty);
        }

        private void bEnableDisableChuteOpen_clicked(object sender, EventArgs e)
        {
            
            if (chute == ChuteState.AutoOpenDisabled)
            {
                chute = ChuteState.AutoOpenEnabled;
            }
            else
            {
                chute = ChuteState.AutoOpenDisabled;
            }

            updateLabels();
        }

        private void bSetLandingSpeed_Click(object sender, EventArgs e)
        {
            this.OnSpeedClicked(EventArgs.Empty);
        }

        private void bAbortLanding_Click(object sender, EventArgs e)
        {
            state = LandState.None;
            updateLabels();
            this.OnAbortLandingClicked(EventArgs.Empty);
        }

        private void bSetCruiseSpeed_Click(object sender, EventArgs e)
        {
            this.OnsetCruiseSpeedClicked(EventArgs.Empty);
        }

        private void bNudge_Click(object sender, EventArgs e)
        {
            this.OnNudgeSpeedClicked(EventArgs.Empty);  
        }
    }
}
