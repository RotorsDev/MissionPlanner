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
    public partial class engineControl : UserControl
    {
        public engineControl()
        {
            InitializeComponent();
        }


        public void setEngineStatus(string status, string error)
        {
            lEngineStatus.Text = status + Environment.NewLine + error;

        }


        public void setThrFuel(int thr, float pump, int level)
        {
            lThrFuel.Text = "Throttle:" + thr.ToString() + "%" + Environment.NewLine +
                            "Fuel Pump:" + pump.ToString("F1") + "volt" + Environment.NewLine +
                            "RAW Fuel Level:" + level.ToString();
        }


        private void bArm_Click(object sender, EventArgs e)
        {

        }





    }
}
