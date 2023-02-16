using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ptPlugin1
{
    public partial class batterypanel : UserControl
    {
        private float servo1 = 6.6f;
        private float servo2 = 5;
        private float payload = 7.5f;
        private float main = 10;
        private float gcs = 0;



        public batterypanel()
        {
            InitializeComponent();

            gaugeServo1.Value0 = servo1;
            gaugeServo2.Value0 = servo2;
            gaugePayload.Value0 = payload;
            gaugeMain.Value0 = main;
            gaugeGCS.Value0 = gcs;

            gaugeServo1.Cap_Idx = 0;
            gaugeServo1.CapText = servo1.ToString("1F") + " V";


        }

        public void setServo1Voltage(float v)
        {
            gaugeServo1.Cap_Idx = 0;
            gaugeServo1.CapText = v.ToString("1F") + " V";

        }





    }
}
