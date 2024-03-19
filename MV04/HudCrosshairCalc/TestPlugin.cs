using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MissionPlanner.Utilities;

namespace MissionPlanner.MV04.HudCrosshairCalc
{
    internal class TestPlugin : MissionPlanner.Plugin.Plugin
    {
        #region Plugin info
        public override string Name => "HudCrosshairCalc Test Plugin";

        public override string Version => "0.1";

        public override string Author => "Dániel Szilágyi";
        #endregion

        public override bool Init()
        {
            loopratehz = 1;
            return true;
        }

        public override bool Loaded()
        {
            new TestForm().Show();

            return true;
        }

        public override bool Exit()
        {
            return true;
        }
    }
}
