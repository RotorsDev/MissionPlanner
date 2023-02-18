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
    public partial class fuelControl : UserControl
    {
        public fuelControl()
        {
            InitializeComponent();
        }


        public void setData(float fuelLevel, float flow)
        {
            MissionPlanner.MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {

                //range check
                if (flow < 0) flow = 0;
                else if (flow > 5) flow = 5;

                gaugeFuel.Value0 = fuelLevel;
                gaugeFuel.Cap_Idx = 0;
                gaugeFuel.CapText = fuelLevel.ToString("F0");
                barFlow.Value = (int)flow;

            }));
        }

    }
}
