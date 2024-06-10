using GMap.NET;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using log4net;
using MissionPlanner;
using MissionPlanner.Controls;
using MissionPlanner.Plugin;
using MissionPlanner.Utilities;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace RAC_MPPlugin_IFP
{
    public class RAC_MPPlugin_IFP : Plugin
    {
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        SplitContainer FDRightSide;

        internal static GMapOverlay FlytoLayer;
        internal GMapMarker FlyToMarker;
        internal GMapMarker CurrentLocationMarker;
        internal PointLatLng FlyToLocation;
        internal GMapRoute FlyToRoute;

        internal UDPSocket telemetrySocket;
        internal UDPSocket flytoSocket;

        // Packet
        public IMKTelemetrypacket telemetryPacket;
        internal FlyToResponse flytostate = FlyToResponse.Idle;
        internal AircraftStatus aircraftstatus = AircraftStatus.OK;
        internal int acktimer = 0;
        MyButton btnApprove;
        MyButton btnDeny;

        // Config
        string telemetryOutAddress;
        string telemetryOutPort;
        string flytoInAddress;
        string flytoInPort;

        #region Plugin info
        public override string Name
        {
            get { return "RACPluginIFP"; }
        }

        public override string Version
        {
            get { return "0.1"; }
        }

        public override string Author
        {
            get { return "Andras Schaffer / RotorsAndCams"; }
        }
        #endregion

        public override bool Init()
        {
            loopratehz = 2;

            FlyToLocation = new PointLatLng(0, 0);

            FlytoLayer = new GMapOverlay("flyto");
            Host.FDGMapControl.Overlays.Add(FlytoLayer);
            FlyToMarker = new GMarkerGoogle(FlyToLocation, GMarkerGoogleType.red_big_stop);

            FlytoLayer.Markers.Add(FlyToMarker);
            FlytoLayer.Markers[0].IsVisible = false;

            // Since the controls on FlighData are located in a different thread, we must use BeginInvoke to access them.
            Host.MainForm.BeginInvoke((MethodInvoker)(() =>
            {
                // SplitContainer1 is hosting panel1 and panel2 where panel2 contains the map and all other controls on the map (WindDir, gps labels, zoom, joystick, etc.)
                FDRightSide = Host.MainForm.FlightData.Controls.Find("splitContainer1", true).FirstOrDefault() as SplitContainer;

                // Hide direction color description labels
                Label l = Host.MainForm.FlightData.Controls.Find("label3", true).FirstOrDefault() as Label;
                l.Visible = false;
                l = Host.MainForm.FlightData.Controls.Find("label4", true).FirstOrDefault() as Label;
                l.Visible = false;
                l = Host.MainForm.FlightData.Controls.Find("label5", true).FirstOrDefault() as Label;
                l.Visible = false;
                l = Host.MainForm.FlightData.Controls.Find("label6", true).FirstOrDefault() as Label;
                l.Visible = false;

                MyLabel hdop = Host.MainForm.FlightData.Controls.Find("lbl_hdop", true).FirstOrDefault() as MyLabel;
                hdop.Location = new Point(-1, 61);
                hdop.Anchor = AnchorStyles.Left | AnchorStyles.Top;

                hdop = Host.MainForm.FlightData.Controls.Find("lbl_sats", true).FirstOrDefault() as MyLabel;
                hdop.Location = new Point(-1, 75);
                hdop.Anchor = AnchorStyles.Left | AnchorStyles.Top;

                // Buttons
                btnApprove = new MyButton();
                btnApprove.Location = new Point(0, FDRightSide.Panel2.Height - 185);
                btnApprove.Name = "btnApprove";
                btnApprove.Text = "Approve";
                btnApprove.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                btnApprove.Visible = false;
                FDRightSide.Panel2.Controls.Add(btnApprove);
                FDRightSide.Panel2.Controls.SetChildIndex(btnApprove, 2);
                btnApprove.Click += new EventHandler(this.btnApprove_Click);

                btnDeny = new MyButton();
                btnDeny.Location = new Point(0, FDRightSide.Panel2.Height - 75);
                btnDeny.Name = "btnDeny";
                btnDeny.Text = "Deny";
                btnDeny.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
                btnDeny.BGGradTop = Color.Red;
                btnDeny.BGGradBot = Color.Red;
                btnDeny.ForeColor = Color.White;
                btnDeny.Visible = false;
                FDRightSide.Panel2.Controls.Add(btnDeny);
                FDRightSide.Panel2.Controls.SetChildIndex(btnDeny, 2);
                btnDeny.Click += new EventHandler(this.btnDeny_Click);
            }));

            // Set up parameters
            telemetryOutAddress = Host.config["IFP_IP", "192.168.0.27"];
            Host.config["IFP_IP"] = telemetryOutAddress;

            telemetryOutPort = Host.config["IFP_TELEMETRY_PORT", "11000"];
            Host.config["IFP_TELEMETRY_PORT"] = telemetryOutPort;

            flytoInAddress = Host.config["GCS_IP", "192.168.0.5"];
            Host.config["GCS_IP"] = flytoInAddress;

            flytoInPort = Host.config["GCS_FLYTO_PORT", "12000"];
            Host.config["GCS_FLYTO_PORT"] = flytoInPort;


            return true;
        }

        public override bool Loaded()
        {
            telemetrySocket = new UDPSocket();
            telemetrySocket.Client(telemetryOutAddress, Convert.ToInt32(telemetryOutPort));

            flytoSocket = new UDPSocket();
            flytoSocket.Server(flytoInAddress, Convert.ToInt32(flytoInPort));
            flytoSocket.PacketReceivedEvent += FlyToPacketReceived_handler;

            return true;
        }

        public override bool Loop()
        {
            // Put together a packet and send it to the remote

            if (Host.cs.connected && Host.cs.armed) aircraftstatus = AircraftStatus.OK;
            else                                    aircraftstatus = AircraftStatus.NOK;

            telemetryPacket.status = IMKProtocol.setstatus(aircraftstatus, flytostate);
            telemetryPacket.lat = (float)Host.cs.lat;
            telemetryPacket.lng = (float)Host.cs.lng;
            telemetryPacket.relAlt = (short)Host.cs.alt;
            telemetryPacket.amsl = (short)Host.cs.altasl;
            telemetryPacket.heading = (short)Host.cs.yaw;

            byte[] raw = IMKProtocol.GenerateRAWTelemetryPacket(telemetryPacket);
            telemetrySocket.Send(raw);

            if (flytostate == FlyToResponse.OperatorWait)
            {
                Host.MainForm.BeginInvoke((MethodInvoker)(() =>
                {
                    btnApprove.Visible = true;
                    btnDeny.Visible = true;
                    btnDeny.BGGradBot = Color.OrangeRed;
                    btnDeny.BGGradTop = Color.Orange;
                }));
            }
            else
            {
                Host.MainForm.BeginInvoke((MethodInvoker)(() =>
                {
                    btnApprove.Visible = false;
                    btnDeny.Visible = false;
                }));
            }

            if ((flytostate == FlyToResponse.ACK) || (flytostate == FlyToResponse.NACK)) acktimer++;

            if (acktimer > 5)
            {
                acktimer = 0;
                if (flytostate == FlyToResponse.NACK) FlytoLayer.Markers[0].IsVisible = false;
                flytostate = FlyToResponse.Idle;

            }

            return true;
        }

        public override bool Exit()
        {
            return true;
        }

        void FlyToPacketReceived_handler(object sender, PacketReceivedEventArgs args)
        {
            IMKFlyTopacket flytopacket = new IMKFlyTopacket();

            bool status = IMKProtocol.ExpandRAWFlyToPacket(args.P, ref flytopacket);

            // If Aircraft is not ok, then ignore and send NACK
            if (aircraftstatus == AircraftStatus.NOK)
            {
                flytostate = FlyToResponse.NACK;
                return;
            }

            if (status)
            {
                // Got fly to packet
                Host.MainForm.BeginInvoke((MethodInvoker)(() =>
                {
                    FlytoLayer.Markers[0].Position = new PointLatLng(flytopacket.lat, flytopacket.lng);
                    FlytoLayer.Markers[0].IsVisible = true;
                    Host.FDGMapControl.ZoomAndCenterMarkers("flyto");
                    Host.FDGMapControl.Zoom = 16.0f;
                    FlyToLocation = new PointLatLng(flytopacket.lat, flytopacket.lng);
                }));

                flytostate = FlyToResponse.OperatorWait;
            }
        }

        void btnApprove_Click(Object sender, EventArgs e)
        {
            flytostate = FlyToResponse.ACK;

            Locationwp gotohere = new Locationwp
            {
                id = (ushort)MAVLink.MAV_CMD.WAYPOINT,
                alt = Host.cs.alt,
                lat = FlyToLocation.Lat,
                lng = FlyToLocation.Lng
            };

            try
            {
                Host.comPort.setGuidedModeWP((byte)Host.comPort.sysidcurrent, (byte)Host.comPort.compidcurrent, gotohere);
            }
            catch (Exception ex)
            {
                CustomMessageBox.Show("Failed to Guided mode:" + ex.Message, "Error");
            }
        }

        void btnDeny_Click(Object sender, EventArgs e)
        {
            flytostate = FlyToResponse.NACK;
        }
    }
}
