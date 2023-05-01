using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ptPlugin1
{
    public partial class fuelControl : UserControl
    {
        public int loadedFuel = 29;

        public fuelControl()
        {
            InitializeComponent();
        }


        public void setData(float fuelLevel, float flow, float consumed)
        {
            MissionPlanner.MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {

                //range check
                if (flow < 0) flow = 0;
                else if (flow > 5) flow = 5;

                gaugeFuel.Value0 = fuelLevel;
                gaugeFuel.Cap_Idx = 0;
                gaugeFuel.CapText = fuelLevel.ToString("F0");

                lLoaded.Text = "Loaded : " + loadedFuel.ToString() + " l";
                lFlow.Text = "Flow : " + flow.ToString("F3") + " l/sec";
                lConsumed.Text = "Consumed : " + consumed.ToString("F2") + " liters";

            }));
        }

    }
}
