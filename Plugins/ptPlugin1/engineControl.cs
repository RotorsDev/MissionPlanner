﻿using System;
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
            MissionPlanner.MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                lEngineStatus.Text = status + Environment.NewLine + error;
            }));
        }


        public void setThrFuel(float thr, float pump, float level)
        {
            MissionPlanner.MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                lThrFuel.Text = "Throttle:" + thr.ToString("F0") + "%" + Environment.NewLine +
                            "Fuel Pump:" + pump.ToString("F1") + "volt" + Environment.NewLine +
                            "RAW Fuel Level:" + level.ToString("F0");
            }));
        }



        public void setRpmEgt(float rpm, float egt)
        {

            MissionPlanner.MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {

                engineRpmGauge.Value0 = rpm / 1000;
                engineRpmGauge.Value1 = rpm / 1000;

                engineRpmGauge.Cap_Idx = 1;
                engineRpmGauge.CapText = rpm.ToString("F0");


                engineTempGauge.Value0 = egt;
                engineTempGauge.Value1 = egt;

                engineTempGauge.Cap_Idx = 1;
                engineTempGauge.CapText = egt.ToString("F0") + " C°";
            }));
        }

        public void setStatus(byte stat, byte error)
        {
            MissionPlanner.MainV2.instance.BeginInvoke((MethodInvoker)(() =>
            {
                Console.WriteLine("Stat : {0} {1} Error:{2}", stat & 0x07, stat & 0xf8, error);

                switch (stat & 0x07)
                {
                    case 1:
                        pEmergency.BackColor = Color.OrangeRed;
                        pStop.BackColor = Color.Transparent;
                        pStart.BackColor = Color.Transparent;
                        break;
                    case 2:
                        pEmergency.BackColor = Color.Transparent;
                        pStop.BackColor = Color.OrangeRed;
                        pStart.BackColor = Color.Transparent;
                        break;
                    case 4:
                        pEmergency.BackColor = Color.Transparent;
                        pStop.BackColor = Color.Transparent;
                        pStart.BackColor = Color.OrangeRed;
                        break;
                    default:
                        pEmergency.BackColor = Color.Transparent;
                        pStop.BackColor = Color.Transparent;
                        pStart.BackColor = Color.Transparent;
                        break;
                }

                string engineStat;
                

                switch ((stat & 0xF8))
                {
                    case 8:
                        engineStat = "Start clearence";
                        break;
                    case 16:
                        engineStat = "Starting";
                        break;
                    case 32:
                        engineStat = "Started Up";
                        break;
                    case 64:
                        engineStat = "Idle calibration";
                        break;
                    case 96:
                        engineStat = "Full operation";
                        break;
                    case 224:
                        engineStat = "MAX Rpm";
                        break;
                    default:
                        engineStat = "ERROR";
                        break;
                }

                string engineError;

                switch (error)
                {
                    case 0:
                        engineError = "";
                        break;
                    case 1:
                        engineError = "RPM Low";
                        break;
                    case 2:
                        engineError = "No Switch channel";
                        break;
                    case 4:
                        engineError = "No Throttle channel";
                        break;
                    case 8:
                        engineError = "EGT Error";
                        break;
                    case 16:
                        engineError = "RPM High";
                        break;
                    case 32:
                        engineError = "Low voltage";
                        break;
                    case 64:
                        engineError = "AS Low voltage";
                        break;
                    default:
                        engineError = "Unknown error";
                        break;
                }

                lEngineStatus.Text = engineStat + Environment.NewLine + engineError;

            }));
        }

        private void bArm_Click(object sender, EventArgs e)
        {
        }







    }
}