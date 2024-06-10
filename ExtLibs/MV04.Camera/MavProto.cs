using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Runtime.InteropServices;
using System.Linq.Expressions;
using System.Threading;
using System.Windows.Forms;

namespace MV04.Camera
{
    public class MavProto
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void MavPacketReceivedCallBack(UInt32 report_type, IntPtr buf, UInt32 buf_len);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void MavCbCmd(Int32 command_id, Int32 result, IntPtr unused_arg, IntPtr handle, IntPtr whole_buf, UInt32 whole_buflen);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate Int32 MavFunc(IntPtr mav_comm, double a, double b, double c, double d, double e, double f, double g, double h);
        
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void MavDumpBytesFunc(IntPtr buffer_ptr, int buffer_size, int callback_argument);

        #region MavDLL
        private const string MavDLL = "MavLinkDLL.dll";

        // The DLL interface is using C calling conventions for all functions
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr InitMavManagerNetwork(Int32 protocol, int ip_dig1, int ip_dig2, int ip_dig3, int ip_dig4, Int32 port, MavPacketReceivedCallBack cb);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr InitMavManagerSerial(Int32 protocol, [MarshalAs(UnmanagedType.LPStr)] string device, Int32 baud_rate, MavPacketReceivedCallBack cb);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavManagerGetLastError();
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern bool MavManagerSetGlobalTlsCollectSendBufferMode(bool no_send);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MavManagerCollectTlsSendBuffer(MavDumpBytesFunc dump_callback, int callback_argument = 0);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern void MavManagerSetGlobalTlsNoReceiveMode(bool no_receive);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetROI_None(IntPtr mav_comm, MavCbCmd callback);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdDoMountControl(IntPtr mav_comm, MavCbCmd callback, int mode);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetGimbal(IntPtr mav_comm, MavCbCmd callback, float roll, float pitch, int zoom_tribtn, float gnd_crs_alt);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSendHeartbeatMsg(IntPtr mav_comm, MavCbCmd callback, Int32 drone_armed);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdConfigureIP_Port(IntPtr mav_comm, MavCbCmd callback, bool is_update, Int32 conf_param, Int32 port);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSendGlobalPositionInitMsg(IntPtr mav_comm, MavCbCmd callback, double lat, double lon, double alt, double rel_alt, double vx, double vy, double vz);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetCurrentDetectorThreshold(IntPtr mav_comm, MavCbCmd callback, Int32 threshold);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetCameraStabilization(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetReportFrequency(IntPtr mav_comm, MavCbCmd callback, Int32 report_type, Int32 frequency);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetSnapshotInterval(IntPtr mav_comm, MavCbCmd callback, Int32 Interval, Int32 Count, Int32 snapInterval);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetDayWhiteBalance(IntPtr mav_comm, MavCbCmd callback, Int32 mode, float temp);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdRefineLocation(IntPtr mav_comm, MavCbCmd callback, Int32 i, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetVideoStreamTransmissionState(IntPtr mav_comm, MavCbCmd callback, bool _b,  Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdUpdateSystemTime(IntPtr mav_comm, MavCbCmd callback, UInt32 epoch_time);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetRecordingState(IntPtr mav_comm, MavCbCmd callback, bool rec_state,  Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetFollowMode(IntPtr mav_comm, MavCbCmd callback, bool enabled);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetROI_Location(IntPtr mav_comm, MavCbCmd callback, float f, float b, float _flt);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdUpdateINS_CalibrationSet(IntPtr mav_comm, MavCbCmd callback, float f, float b, float _flt);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetGeoAvg(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetVividEnabled(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableObjectDetection(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdExecuteRebootTrip(IntPtr mav_comm, MavCbCmd callback);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableFlyAbove(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetFOV(IntPtr mav_comm, MavCbCmd callback, float fov);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetIlluminatorMode(IntPtr mav_comm, MavCbCmd callback, Int32 mode);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdConfigureIP_Address(IntPtr mav_comm, MavCbCmd callback, bool is_update, Int32 IP_entry, Int32 a, Int32 b, Int32 c, Int32 d);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableVMD_Reports(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetHoldCoordinateMode(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdConfigureStreamBitrate(IntPtr mav_comm, MavCbCmd callback, bool is_update, Int32 bitrate, Int32 channel);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnable_VMD(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetRollDerotation(IntPtr mav_comm, MavCbCmd callback, Int32 derotationMode);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdClearRetractLock(IntPtr mav_comm, MavCbCmd callback);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdDoNUC(IntPtr mav_comm, MavCbCmd callback);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetAES128Key(IntPtr mav_comm, MavCbCmd callback, bool is_update, UInt32 bits_0_31, UInt32 bits_32_63, UInt32 bits_64_95, UInt32 bits_96_127);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetDisplayedSensor(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 TerminateMavManager(IntPtr mav_comm, int is_blocking);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSendAttitudeMsg(IntPtr mav_comm, MavCbCmd callback, float roll, float pitch, float yaw);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetActiveTracker(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetPointToCoordinateMode(IntPtr mav_comm, MavCbCmd callback, float a, float b, float c);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetCameraZoom(IntPtr mav_comm, MavCbCmd callback, Int32 zoomVal);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetBandwidthLimit(IntPtr mav_comm, MavCbCmd callback, bool is_update, Int32 b, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetJoystickMode(IntPtr mav_comm, MavCbCmd callback, Int32 gndCrsAlt);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetSystemMode(IntPtr mav_comm, MavCbCmd callback, Int32 mode);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetRateMultiplier(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdTakeSnapShot(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetSingleYawMode(IntPtr mav_comm, MavCbCmd callback, Int32 sy_mode);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetPrimaryTracker(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetSharpness(IntPtr mav_comm, MavCbCmd callback, Int32 i, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetCameraPositionMode(IntPtr mav_comm, MavCbCmd callback, Int32 mode, float pitch, float roll);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetIR_NoiseReductionLevel(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetRVT_Position(IntPtr mav_comm, MavCbCmd callback, float lat, float lon);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetTracker_ROI(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdDetectorSelect(IntPtr mav_comm, MavCbCmd callback, Int32 detectorIndex);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableRollDerotation(IntPtr mav_comm, MavCbCmd callback, bool is_update, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableAR_Marker(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetTrackingIcon(IntPtr mav_comm, MavCbCmd callback, Int32 trackerIcon);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetPIP_Mode(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdResumeObjectDetectionScan(IntPtr mav_comm, MavCbCmd callback);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetTrackingMode(IntPtr mav_comm, MavCbCmd callback, Int32 a, Int32 b, Int32 c, Int32 d);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetFireDetectorThreshold(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdUpdateVideoIP(IntPtr mav_comm, MavCbCmd callback, Int32 i, Int32 b, Int32 c, Int32 d);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdDoBIT_Test(IntPtr mav_comm, MavCbCmd callback);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdEnablePLR_Control(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableCarCounting(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetSBS_Mode(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetVMD_PolygonColor(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableGCS_SecondaryControl(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetStreamMode(IntPtr mav_comm, MavCbCmd callback, Int32 ch0_mode, Int32 ch1_mode);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdReconfigureVideoStream(IntPtr mav_comm, MavCbCmd callback, Int32 bitRate, Int32 reconfBitrateChannel);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetIRPolarity(IntPtr mav_comm, MavCbCmd callback, Int32 polarity);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdDoIperfTest(IntPtr mav_comm, MavCbCmd callback);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableBandwidthLimit(IntPtr mav_comm, MavCbCmd callback, bool is_update, bool enable, Int32 channel);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableAutoRecord(IntPtr mav_comm, MavCbCmd callback, bool is_update, bool enabled, Int32 channel);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableGCS_SecondaryReports(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSendGpsRawInitMsg(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetNetworkMTU(IntPtr mav_comm, MavCbCmd callback, bool is_update, Int32 mtu);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetGroundCrossingAlt(IntPtr mav_comm, MavCbCmd callback, float _flt);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableCrosshairOnIdle(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdFreezeVideoConfigure(IntPtr mav_comm, MavCbCmd callback, Int32 freezwVal);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetIR_GainLevel(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableAES_Encryption(IntPtr mav_comm, MavCbCmd callback, bool is_update, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdConfigureVideoDestinationPort(IntPtr mav_comm, MavCbCmd callback, Int32 i, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSendSysStatusMsg(IntPtr mav_comm, MavCbCmd callback, UInt16 a);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdClearSD_Card(IntPtr mav_comm, MavCbCmd callback);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSendSysTimeMsg(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetStreamBitrate(IntPtr mav_comm, MavCbCmd callback, Int32 bitrate, Int32 channel);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetRecordingBitrate(IntPtr mav_comm, MavCbCmd callback, bool is_update, Int32 bitrate, Int32 channel);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetIR_Color(IntPtr mav_comm, MavCbCmd callback, Int32 color);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetPilotView(IntPtr mav_comm, MavCbCmd callback, float pitch_angle);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdUpdateRemoteIP(IntPtr mav_comm, MavCbCmd callback, Int32 i, Int32 b, Int32 c, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetGeoMapReferencePoint(IntPtr mav_comm, MavCbCmd callback, double latitude, double longitude, float elevation);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetAutoRecordINS_Timeout(IntPtr mav_comm, MavCbCmd callback, bool config_cmd, Int32 ins_timeout, Int32 vid_channel);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdResetCarCountingCount(IntPtr mav_comm, MavCbCmd callback);

        // to update:
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSaveConfiguration(IntPtr mav_comm, MavCbCmd callback);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdRebootSystem(IntPtr mav_comm, MavCbCmd callback);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableExecuteMavlinkFlightPlan(IntPtr mav_comm, MavCbCmd callback, bool is_update, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableForceRollToHorizon(IntPtr mav_comm, MavCbCmd callback, bool is_update, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetOSD_Mode(IntPtr mav_comm, MavCbCmd callback, bool is_udate, Int32 mode);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableOSDShowGndCrsInfo(IntPtr mav_comm, MavCbCmd callback, bool is_update, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableOSDShowCraftGPSInfo(IntPtr mav_comm, MavCbCmd callback, bool is_update, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableOSDShowCraftFlightMode(IntPtr mav_comm, MavCbCmd callback, bool is_update, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableOSDShowCraftAirSpeed(IntPtr mav_comm, MavCbCmd callback, bool is_update, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetOSD_MiniMapType(IntPtr mav_comm, MavCbCmd callback, bool is_update, int type);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetVGA_ResizingThreshold(IntPtr mav_comm, MavCbCmd callback, bool is_update, int threshold, int channel);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdGetObjectDetectorType(IntPtr mav_comm, MavCbCmd callback);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdGetObjectDetectionEnabled(IntPtr mav_comm, MavCbCmd callback);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetObjectDetectionAutoTrackEnabled(IntPtr mav_comm, MavCbCmd callback, bool is_update, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetObjectDetectionAutoZoomOnTarget(IntPtr mav_comm, MavCbCmd callback, bool is_update, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetObjectDetectionLoiterAPOnDetection(IntPtr mav_comm, MavCbCmd callback, bool is_update, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetALPREnabled(IntPtr mav_comm, MavCbCmd callback, bool is_update, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetALPR_CountryProfile(IntPtr mav_comm, MavCbCmd callback, bool is_update, Int32 profile);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdGetAI_LicenseStatus(IntPtr mav_comm, MavCbCmd callback, bool must_be_false);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetVGA_OutputEnabled(IntPtr mav_comm, MavCbCmd callback, bool is_update, bool enable, int channel);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetStreamGOP_Size(IntPtr mav_comm, MavCbCmd callback, bool is_update, Int32 size, Int32 channel);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetStreamQuantizationValue(IntPtr mav_comm, MavCbCmd callback, bool is_update, Int32 value, Int32 channel);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdAddPOI(IntPtr mav_comm, MavCbCmd callback, int index, int radius, [MarshalAs(UnmanagedType.LPStr)] string object_name, float lat, float lon, float alt);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdClearPOI(IntPtr mav_comm, MavCbCmd callback);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdDeletePOI(IntPtr mav_comm, MavCbCmd callback, int index);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetFocus(IntPtr mav_comm, MavCbCmd callback, int focus_state);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetReportSystemID(IntPtr mav_comm, MavCbCmd callback, bool is_updating, int ID);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetReportCompID(IntPtr mav_comm, MavCbCmd callback, bool is_updating, int ID);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetAckSystemID(IntPtr mav_comm, MavCbCmd callback, bool is_updating, int ID);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetAckCompID(IntPtr mav_comm, MavCbCmd callback, bool is_updating, int ID);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableMavlinkSequenceNumber(IntPtr mav_comm, MavCbCmd callback, bool is_updating, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdDoRescue(IntPtr mav_comm, MavCbCmd callback);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetCameraMotorControl(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]

        public static extern Int32 MavCmdSetOSDFreeText(IntPtr mav_comm, MavCbCmd callback, string strFreeText);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]

        public static extern Int32 MavCmdSetEnable_RangeFinder(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetRangeFinderReportFreq(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]

        // MavCmdIlluminatorControl 
        public static extern Int32 MavCmdSetEnableIlluminator(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableGraphicsIlluminator(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetSafetyIlluminator(IntPtr mav_comm, MavCbCmd callback);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetEnableTrackerOffsetIlluminator(IntPtr mav_comm, MavCbCmd callback, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]

        // MavCmdAuxCameraControl
        public static extern Int32 MavCmdEnableDisableVideoStream(IntPtr mav_comm, MavCbCmd callback, Int32 i2, bool enable);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]

        // added the tracker control functions
        public static extern Int32 MavCmdShowHideTrackerIcon(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetCameraComp(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSetTrackerControlProfile(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]

        // Frame Cache Control
        public static extern Int32 MavCmdStartSessionPause(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdStopSessionLive(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdPlay(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSeek(IntPtr mav_comm, MavCbCmd callback, Int32 i, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSeekSingleFwd(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern Int32 MavCmdSeekSingleBwd(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]

        // added IR 1080P Preprocess Mode
        public static extern Int32 MavCmdDefineIR1080pPreprocessMode(IntPtr mav_comm, MavCbCmd callback, Int32 i2);
        [DllImport(MavDLL, CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr GetMavManagerVersion(ref Int32 major, ref Int32 minor, ref Int32 build);
        #endregion
        
        public static void dummy_cb(Int32 command_id, Int32 result) { }

        // Constants
        private const ushort X25_INIT_CRC = 0xFFFF;
        private const byte MAVLINK_START_OF_FRAME = 0xFE;
        private const byte MAVLINK_2_START_OF_FRAME = 0xFD;
        private const int MAVLINK_HEADER_LENGTH = 6;
        private const int MAVLINK_2_HEADER_LENGTH = 10;
        private const byte MAVLINK_DRONE_ARMED_MASK = 0x80;

        // Mavlink Command Constants
        public const byte MAVLINK_ATTITUDE_MSG_CRC = 0x27;
        public const byte MAVLINK_GBL_POS_INT_CRC = 0x68;
        public const byte MAVLINK_GPS_RAW_INT_CRC = 0x18;
        public const byte MAVLINK_SYS_STAT_CRC = 0x7C;
        public const byte MAVLINK_HEART_BEAT_CRC = 0x32;
        public const byte MAVLINK_SYS_TIME_CRC = 0x89;
        public const byte MAVLINK_CMD_LONG_MSGID = 0x4C;
        public const byte MAVLINK_CMD_LONG_CRC = 0x98;
        public const byte MAVLINK_CMD_LONG_MSG_LEN = 0x21;
        public const byte MAVLINK_CMD_ACK_MSGID = 0x4D;
        public const byte MAVLINK_CMD_ACK_CRC = 0x8F;
        public const byte MAVLINK_CMD_ACK_MSG_LEN = 0x03;
        public const byte MAVLINK_EXT_V2_MSGID = 0xF8;
        public const byte MAVLINK_EXT_V2_MSG_CRC = 0x08;
        public const byte MAVLINK_EXT_V2_SYS_REPORT_MSG_LEN = 0x3D;
        public const byte MAVLINK_EXT_V2_LOS_REPORT_MSG_LEN = 0x36;
        public const byte MAVLINK_EXT_V2_GND_REPORT_MSG_LEN = 0x12;
        public const byte MAVLINK_EXT_V2_SDCARD_REPORT_MSG_LEN = 0x0B;
        public const byte MAVLINK_EXT_V2_VIDEO_REPORT_MSG_LEN = 0x0A;
        public const byte MAVLINK_EXT_V2_SNAPSHOT_REPORT_MSG_LEN = 0x33;
        public const byte MAVLINK_EXT_V2_LOS_RATE_REPORT_MSG_LEN = 0x26;
        public const byte MAVLINK_EXT_V2_IMU_REPORT_MSG_LEN = 0x4C;
        public const byte MAVLINK_EXT_V2_FIRE_REPORT_MSG_LEN = 0x28;
        public const byte MAVLINK_EXT_V2_TRACKING_REPORT_DATA_LEN = 0x3C;
        public const byte MAVLINK_EXT_V2_PARAMETER_REPORT_MSG_LEN = 0x09;
        public const byte MAVLINK_EXT_V2_OGLR_REPORT_MSG_LEN = 0x2A;
        public const byte MAVLINK_EXT_V2_CAR_COUNT_REPORT_MSG_LEN = 0x12;

        public enum TriStateBtn
        {
            Unpressed,
            Pressed_1,
            Pressed_2
        }

        /// <summary>
        /// enum of NextVision's error codes for DLL functions
        /// </summary>
        public enum mav_error : Int32
        {
            ok = 0,
            InvalidArgs = 1,
            Send_Error = 2,
            Error_Timeout = 3,
            Error_Busy = 4,
            Error_Port_In_Use = 5,
            Error_Port_Bind_Failed = 6,
            Error_Serial = 7,
            Error_Memory = 8,
            Eror_Deadlock = 9
        }

        /// <summary>
        /// Mavlink MAV_RESULT values
        /// </summary>
        public enum MAV_RESULT
        {
            MAV_RESULT_ACCEPTED = 0,
            MAV_RESULT_TEMPORARILY_REJECTED = 1,
            MAV_RESULT_DENIED = 2,
            MAV_RESULT_UNSUPPORTED = 3,
            MAV_RESULT_FAILED = 4,
            MAV_RESULT_IN_PROGRESS = 5,
            MAV_RESULT_CANCELLED = 6
        }

        /// <summary>
        /// enum of NextVision's DO_DIGICAM_CONTROL commands
        /// </summary>
        public enum MavCommands
        {
            Set_System_Mode,
            Take_Snapshot,
            Set_Rec_State,
            Set_Sensor,
            Set_FOV,
            Set_Sharpness,
            Do_BIT,
            Set_IR_Polarity,
            Set_SingleYaw_Mode,
            Set_Follow_Mode,
            Do_NUC,
            Set_Report_Frequency,
            Clear_Retract_Lock,
            Set_System_Time,
            Set_IR_Color,
            Set_Joystick_Mode,
            Set_Gnd_Crossing_Alt,
            Set_Roll_Derotation,
            Illuminator_Control,
            Reboot_System,
            Reconfigure_Video_BitRate,
            Set_Tracking_Icon,
            Set_Zoom,
            Freeze_Video,
            Pilot_View,
            RVT_Position,
            Snapshot_Interval,
            Update_Remote_IP,
            Update_Video_IP,
            Configuration_Cmd,
            Erase_SD,
            Set_Rate_Multiplier,
            Set_IR_Gain_Level,
            Set_Vid_Stream_Tx_State,
            Set_Day_White_Balance,
            Set_Illuminator_Mode,
            Set_Hold_Coordinate_Mode,
            Update_Show_Cross_On_Idle,
            Resume_AI_Scan,
            Set_Fly_Above,
            Set_Camera_Stabilization,
            Update_Stream_Bitrate,
            Do_Iperf_Test,
            Set_Geo_AVG,
            Set_Vivid,
            Set_Focus,
            Do_Rescue,
            Set_Camera_Motor_Control,
            Detection_Control,
            AR_Marker_Control,
            Geo_Map_Control,
            Stream_Control,
            VMD_Control,
            Multiple_GCS_Control,
            Update_INS_Calibration,
            Multiple_Trackers_Control,
            POI_Control,
            Mount_Control,
            PLR_Control,
            Set_OSD_Free_Text,
            Range_Finder_Control,
            Aux_Camera_Control,
            Frame_Cache_Control
        }

        /// <summary>
        /// enum of NextVision's camera modes
        /// </summary>
        public enum NvSystemModes
        {
            Stow,
            Pilot,
            HoldCoordinate,
            Observation,
            LocalPosition,
            GlobalPosition,
            GRR,
            Tracking,
            EPR,
            Nadir,
            Nadir_Scan,
            TwoDScan,
            PTC,
            UnstabilizedPosition
        }

        /// <summary>
        /// enum of NextVision's Configuration commands
        /// </summary>
        public enum NvConfCmds
        {
            UpdateParameter = 0,
            SaveParameters,
            RebootSystem,
            GetParameter
        }

        /// <summary>
        /// enum of NextVision's Detection commands
        /// </summary>
        public enum NvDetectCmds
        {
            Detector_Enabled,
            Detector_Select,
            Detector_Threshold,
            Resume_Detection,
            Fire_Detector_Threshold,
            Set_Car_Counting,
            Reset_Car_Counting,
            IR_1080p_Preprocess_Mode
        }

        /// <summary>
        /// enum of NextVision's Stream Control commands
        /// </summary>
        public enum NvStreamCmds
        {
            Stream_Mode,
            Set_PIP_Mode,
            Set_SBS_Mode
        }

        /// <summary>
        /// enum of NextVision's VMD Control commands
        /// </summary>
        public enum NvVMDCmds
        {
            VMD_Enabled,
            VMD_PolygonColor,
            VMD_Reports
        }
        /// <summary>
        /// enum of NextVision's Range Finder Control commands
        /// </summary>
        public enum NvRangeFinderCmds
        {
            Range_Enabled,
            Range_Freq
        }

        /// <summary>
        /// enum of NextVision's Aux Camera Control commands
        /// </summary>
        public enum NvAuxCameraControlCmds
        {
            Enable_Disable_Video_Stream
        }

        /// <summary>
        /// enum of NextVision's Multi Station GCS Control commands
        /// </summary>
        public enum NvGCSCmds
        {
            Enable_Multiple_GCS,
            Secondary_Multiple_GCS
        }

        /// <summary>
        /// enum of NextVision's Tracker Control commands
        /// </summary>
        public enum NvTrackerCmds
        {
            Primary_Tracker,
            Active_Tracker,
            Tracker_ROI,
            Tracker_Icon,
            Camera_Comp,
            Tracker_Control_Profile
        }

        /// <summary>
        /// enum of NextVision's Illuminator Control commands
        /// </summary>
        public enum NvIlluminatorCmds
        {
            Illuminator_EnabledDisabled,
            Illuminator_Graphics,
            Illuminator_Safety,
            Illuminator_EnableTrackerOffset
        }

        /// <summary>
        /// enum of NextVision's Frame Cache Control
        /// </summary>
        public enum NvFrameCacheCmds
        {
            Frame_Start,
            Frame_Stop,
            Frame_Play,
            Frame_Seek,
            Frame_SeeekFwd,
            Frame_SeekBwd
        }

        /// <summary>
        /// enum of NextVision's Configuration parameters
        /// </summary>
        public enum NvConfParams
        {
            VideoDestIp = 0,
            VideoDestPort,
            EnableBWLimit,
            BWLimit,
            StreamBitrate,
            RecordBitrate,
            EthernetIPAddress,
            EthernetMask,
            EthernetGatewayIP,
            EthernetMTU,
            EnableRollDerotation,
            EnableAutoRec,
            AutoRecINSTimeout,
            EnableAESEncryption,
            AES128Key,
            RemoteIP,
            RemotePort,
            LocalPort,
            ExecuteMavlinkFlightPlan,
            ForceRollToHorizon,
            OSDMode,
            OSDShowGndCrsInfo,
            OSDShowCraftGPSInfo,
            OSDShowCraftFlightMode,
            OSDShowCraftAirSpeed,
            OSDMinimapType,
            VGAResizingThreshold,
            VGAOutputMode,
            ReportSystemID,
            ReportComponentID,
            AckSystemID,
            AckComponentID,
            EnableMavlinkSequenceNumber,
            StreamGOPSize,
            StreamQuantizationValue,
            ObjectDetectionEnabled,
            ObjectDetectionDetectorType,
            ObjectDetectionAutoTrackEnabled,
            ObjectDetectionAutoZoomOnTarget,
            ObjectDetectionLoiterAPOnDetection,
            ALPREnabled,
            ALPRCountryProfile,
            AILicenseProfile
        }

        /// <summary>
        /// enum of NextVision's Camera test error codes
        /// </summary>
        public enum NvCamTestErrorCodes
        {
            CameraTestPassed = 0,
            TimeoutMoveToStow_Roll,
            TimeoutMoveToStow_Pitch,
            TimeoutMoveRight,
            RateErrMoveRight,
            TimeoutMoveLeft,
            RateErrMoveLeft,
            TimeoutMoveDown,
            RateErrMoveDown,
            TimeoutMoveUp,
            RateErrMoveUp,
            ImageFlipMismatch
        }

        public enum NvGeoMapCommands
        {
            RefineLocation,
            SetReferencePoint
        }

        public enum NvReportsIndex
        {
            System,
            LOS,
            Gnd,
            SD,
            Video,
            Snapshot,
            LOS_Rate,
            IMU,
            Fire,
            Tracking,
            LPR,
            AR_Marker,
            Param,
            Car_Count,
            OGLR,
            VMD,
            OD,
            PLR,
            RangeFinder,
            Aux_Camera,
            Ack,
            Unknown,
            MaxEnumValue
        }

        /// <summary>
        /// enum of NextVision's POI Control commands
        /// </summary>
        public enum NvPOICmds
        {
            AddPOI,
            DeletePOI,
            ClearPOI
        }

        /// <summary>
        /// Protocol Definitions
        /// </summary>
        public enum MavReportType : Int32
        {
            SystemReport = 0,
            LosReport,
            GndCrsReport,
            SnapshotReport = 5,
            SDCardReport,
            VideoReport,
            LosRateReport,
            ObjectDetectionReport,
            IMUReport,
            FireDetectionReport,
            TrackingReport,
            LPRReport,
            ARMarkerReport,
            ParameterReport,
            CarCountReport,
            OGLRReport,
            VMDReport,
            PLR_Report,
            Reserved,
            RangeFinderReport,
            AuxCameraControlReport
        };

        public enum ParameterReportParameterIds : Int32
        {
            VideoDestinationIP,
            VideoDesinationPort,
            BandwidthLimitEnable,
            BandwidthLimit,
            StreamBitrate,
            RecordBitrate,
            EthernetIPAddress,
            EthernetSubnetMask,
            EthernetGatewayIP,
            EthernetMTU,
            EnableRollDerotation,
            EnableAutoRec,
            AutoRecINSTimeout,
            EnableAESEncryption,
            AES128Key,
            RemoteIP,
            RemotePort,
            LocalPort,
            ExecuteMavlinkFlightPlan,
            ForceRollToHorizon,
            OSDMode,
            OSDShowGndCrsInfo,
            OSDShowCraftGPSInfo,
            OSDShowCraftFlightMode,
            OSDShowCraftAirSpeed,
            OSDMinimapType,
            VGAResizingThreshold,
            VGAOutputMode,
            ReportSystemID,
            ReportComponentID,
            AckSystemID,
            AckComponentID,
            //EnableMavlinkSequenceNumber,
            StreamGOPSize,
            StreamQuantizationValue,
            ObjectDetectionEnabled,
            ObjectDetectionDetectorType,
            ObjectDetectionAutoTrackEnabled,
            ObjectDetectionAutoZoomOnTarget,
            ObjectDetectionLoiterAPOnDetection,
            ALPREnabled,
            ALPRCountryProfile,
            AILicenseStatus,
        };

        public enum ParameterReportStatusArgs : Int32
        {
            StatusFail,
            StatusSuccess,
        };

        /// <summary>
        /// Protocol Definitions
        /// </summary>
        public enum MavMountMode
        {
            Retract = 0,
            Neutral,
        };

        // Receiver Delegate
        private MavPacketReceivedCallBack MavPacketReceived = null;
        public MavCbCmd ackCb = null;

        // Mavlink Communication Object
        public IntPtr mav_comm = IntPtr.Zero;

        // Mavlink Rx Resources
        private byte[] mavlink_rx_packet = new byte[1024];

        // mavlink protocol resources 
        private byte mavlink_protocol_ver;
        public int mavlink_header_length { private set; get; }
        public byte mavlink_start_of_frame { private set; get; }

        // Declares if reports and ACK are being received
        // Only relevant for network mode so the port would not be blocked
        public static bool NoReportsReceivingMode = false;

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 72)]
        public struct SysReport
        {
            public float roll;
            public float pitch;
            public float fov;
            public byte trackerStatus;
            public byte recordingStatus;
            public byte activeSensor;
            public byte irSensorPolarity;
            public byte systemMode;
            public byte IlluminatorStatus;
            public short trackerROI_x;
            public short trackerROI_y;
            public float single_yaw_cmd;
            public byte snapshot_busy;
            public float cpu_temp;
            public float camera_ver;
            public int   trip2_ver;
            public ushort bit_status;
            public byte status_flags;
            public byte camera_type;
            public float roll_rate;
            public float pitch_rate;
            public float cam_temp;
            public float roll_derot_angle;
            public byte network_type;
            public uint timeSinceBoot;
            public ulong utcTimeStamp;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 52)]
        public struct LosReport
        {
            public float los312_x;
            public float los312_y;
            public float los312_z;
            public float upperLeftLat;
            public float upperLeftLon;
            public float upperRightLat;
            public float upperRightLon;
            public float lowerRightLat;
            public float lowerRightLon;
            public float lowerLeftLat;
            public float lowerLeftLon;
            public float losElevation;
            public float losAzimuth;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 16)]
        public struct GndCrsReport
        {
            public float gndCrsLat;
            public float gndCrsLon;
            public float gndCrsAlt;
            public float gndCrsSlantRange;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 9)]
        public struct SDCardReport
        {
            public byte detected;
            public float totalSpace;
            public float availableSpace;            
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 8)]
        public struct VideoReport
        {
            public ushort streamBitrate0;
            public ushort recordBitrate0;
            public ushort streamBitrate1;
            public ushort recordBitrate1;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 48)]
        public struct SnapshotReport
        {
            public byte[] fileName;
            public byte videoChannel;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 36)]
        public struct LosRateReport
        {
            public float losRateX;
            public float losRateY;
            public float roll;
            public float x;
            public float pitch;
            public float y;
            public uint timeSinceBoot;
            public ulong utcTimeStamp;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 74)]
        public struct IMUReport
        {
            public float roll;
            public float pitch;
            public float x;
            public float y;
            public float losRateX;
            public float losRateY;
            public float accelX;
            public float accelY;
            public float accelZ;
            public float gyroX;
            public float gyroY;
            public float gyroZ;
            public float compassX;
            public float compassY;
            public float compassZ;
            public ushort timeStamp;
            public uint timeSinceBoot;
            public ulong utcTimeStamp;
        }

        // Object Detection report structs
        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 32)]
        public struct DetectionObject
        {
            public ushort classId;
            public ushort uniqueIdentifier;
            public float x;
            public float y;
            public float width;
            public float height;
            public float lat;
            public float lon;
            public float alt;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 236)]
        public struct ObjectDetectionReport
        {
            public uint timeSinceBoot;
            public ulong utcTimeStamp;
            public DetectionObject[] detections;
        }

        /* Object Detection report structs */
        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 18)]
        public struct AuxCameraDetectionObject
        {
            public ushort classId;
            public float azimuth;
            public float elevation;
            public float relativeAzimuth;
            public float relativeElevation;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 247)]
        public struct ObjectAuxCameraDetectionReport
        {
            public uint timeSinceBoot;
            public ulong utcTimeStamp;
            public byte aux_channel_id;
            public AuxCameraDetectionObject[] auxCameraDetections;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 38)]
        public struct FireDetectionReport
        {
            public float lat;
            public float lon;
            public float alt;
            public float fov;
            public ushort totalSatPixel;
            public byte[] regionSatPixel;
            public uint timeSinceBoot;
            public ulong utcTimeStamp;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 60)]
        public struct TrackingReport
        {
            public float losX;
            public float losY;
            public float losZ;                      
            public float losRateX;
            public float losRateY;
            public float trackerRoiAzimuth;
            public float trackerRoiElevation;
            public float trackerRoiLat;
            public float trackerRoiLon;
            public float trackerRoiAlt;
            public float trackerRoiX;
            public float trackerRoiY;
            public uint timeSinceBoot;
            public ulong utcTimeStamp;     
        }

        // ALPR report structs
        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 36)]
        public struct PlateObject
        {
            public byte[] plateText;
            public float x;
            public float y;
            public float lat;
            public float lon;
            public float alt;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 228)]
        public struct LPRReport
        {
            public uint timeSinceBoot;
            public ulong utcTimeStamp;
            public PlateObject[] plates;
        }
        // ALPR report structs 

        // AR Marker report structs 
        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 34)]
        public struct ARMarkerObject
        {
            public ushort id;
            public float[] xy1;
            public float[] xy2;
            public float[] xy3;
            public float[] xy4;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 216)]
        public struct ARMarkerReport
        {
            public uint timeSinceBoot;
            public ulong utcTimeStamp;
            public ARMarkerObject[] markers;
        }

        // AR Marker report structs
        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 7)]
        public struct ParameterReport
        {
            public byte status;
            public ushort parameterId;
            public float value;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 16)]
        public struct CarCountReport
        {
            public uint timeSinceBoot;
            public ulong utcTimeStamp;
            public uint carCount;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 16)]
        public struct RangeFinderReport
        {
            public uint timeSinceBoot;
            public ulong utcTimeStamp;
            public float RangeFinder;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 40)]
        public struct OGLRReport
        {
            public uint timeSinceBoot;
            public ulong utcTimeStamp;
            public uint ptsTimeStamp;
            public double refinedLat;
            public double refinedLon;
            public float refinedASL;
            public float refinedAGL;
        }

        // VMD report structs
        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 28)]
        public struct VMDObject
        {
            public float x;
            public float y;
            public float width;
            public float height;
            public float latitude;
            public float longitude;
            public float altitude;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 236)]
        public struct VMDReport
        {
            public uint timeSinceBoot;
            public ulong utcTimeStamp;
            public VMDObject[] vmdObjects;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1, Size = 48)]
        public struct PLR_Report
        {
            public UInt32 timeSinceBoot;
            public UInt64 utcTimeStamp;
            public double latitude;
            public double longitude;
            public double platform_latitude;
            public double platform_longitude;
            public UInt32 frame_idx;
        }

        public static string MavErrorMessage()
        {
            switch ((mav_error)MavManagerGetLastError())
            {
            case mav_error.Error_Timeout: return "Timeout Reached";
            case mav_error.Send_Error: return "Communication Failed";
            case mav_error.InvalidArgs: return "Invalid Arguments";
            case mav_error.Eror_Deadlock: return "Deadlock";
            case mav_error.Error_Busy: return "Busy";
            case mav_error.Error_Memory: return "Out Of Memory";
            case mav_error.Error_Port_Bind_Failed: return "Port Connection Failed";
            case mav_error.Error_Port_In_Use: return "Port Connection Failed";
            case mav_error.Error_Serial: return "COM Error";
            default: return MavManagerGetLastError().ToString();
            }
        }

        public static bool IsValid(MavProto inst)
        {
            return inst != null && inst.mav_comm != IntPtr.Zero;
        }

        /// <summary>
        /// MavProto Constructor for COM port interface
        /// </summary>
        /// <param name="proto_ver"></param>
        /// <param name="port_name">COM port name</param>
        /// <param name="baud">COM port baud rate</param>
        /// <param name="mav_packet_received">callback for reports</param>
        /// <param name="ackCb">callback for COMMAND_ACK messages</param>
        public MavProto(byte proto_ver, string port_name, int baud, MavPacketReceivedCallBack mav_packet_received, MavCbCmd ackCb)
        {
            // update the data received callback
            MavPacketReceived = mav_packet_received;
            SetProtocolVer(proto_ver);
            this.ackCb = ackCb;

            // create the MavComm object using the serial port name and baud rate
            try
            {
                // Ensure no receive mode is off (no use for COM port interface), although this is the default value already
                MavManagerSetGlobalTlsNoReceiveMode(false);

                mav_comm = InitMavManagerSerial((int)proto_ver, port_name, baud, mav_packet_received);
            }
            catch
            {
                mav_comm = IntPtr.Zero;
            }
        }

        public string FormatSerialOpenError()
        {
            if (!IsValid(this))
            {
                return "MavProto Creation Failed Over Serial Failed (last MAV Error: " + MavErrorMessage() + ")";
            }
    
            return "";
        }

        /// <summary>
        /// MavProto Construction for Ethernet(IP) interface
        /// </summary>
        /// <param name="proto_ver"></param>
        /// <param name="ip"></param>
        /// <param name="port">udp port number</param>
        /// <param name="mav_packet_received">callback for reports</param>
        /// <param name="ackCb">callback for COMMAND_ACK messages</param>
        public MavProto(byte proto_ver, IPAddress ip, int port, MavPacketReceivedCallBack mav_packet_received, MavCbCmd ackCb)
        {
            // update the data received callback
            MavPacketReceived = mav_packet_received;     
            SetProtocolVer(proto_ver);
            this.ackCb = ackCb;

            // create the MavComm object using the ip address and port number
            try
            {
                // Disable reports and port binding if specified
                MavManagerSetGlobalTlsNoReceiveMode(NoReportsReceivingMode);

                byte[] digs = ip.GetAddressBytes();
                mav_comm = InitMavManagerNetwork((int)proto_ver, digs[0], digs[1], digs[2], digs[3], port, mav_packet_received);
            }
            catch
            {
                mav_comm = IntPtr.Zero;
            }
        }

        public string FormatNetworkOpenError()
        {
            if (!IsValid(this))
            {
                return "MavProto Creation Failed Over Network Failed (last MAV Error: " + MavErrorMessage() + ")";
            }
    
            return "";
        }

        /// <summary>
        /// Sets mavlink protocol version
        /// </summary>
        /// <param name="proto_ver">protocol version</param>
        public void SetProtocolVer(byte proto_ver)
        {
            mavlink_protocol_ver = proto_ver;
            SetMavProtoValuesByVersion();
        }

        /// <summary>
        /// Get the mavlink protocol version
        /// </summary>
        /// <returns></returns>
        public byte getProtocolVer()
        {
            return mavlink_protocol_ver;
        }

        /// <summary>
        /// Sets values according to mavlinks protocol version
        /// </summary>
        private void SetMavProtoValuesByVersion()
        {
            if (mavlink_protocol_ver == 0x01 || mavlink_protocol_ver == 0x00 || mavlink_protocol_ver == 0x00)
            {
                mavlink_header_length = MAVLINK_HEADER_LENGTH;
                mavlink_start_of_frame = MAVLINK_START_OF_FRAME;
            }
            else if(mavlink_protocol_ver == 0x02)
            {
                mavlink_header_length = MAVLINK_2_HEADER_LENGTH;
                mavlink_start_of_frame = MAVLINK_2_START_OF_FRAME;
            }            
        }

        /// <summary>
        /// Release all sockets and interfaces that are used by the object
        /// </summary>
        public void MavProtoClose()
        {          
            /* close the MavComm object */
            //try
            {
                var handle = Interlocked.Exchange(ref mav_comm, IntPtr.Zero);

                if (handle != IntPtr.Zero)
                {
                    // Wait for threads using blocking mode (assert on error)
                    int error = TerminateMavManager(handle, 1);

                    if (error != 0)
                    {
                        string output_error = "Termination Of Mavlink Connection Failed (last MAV Error: " + MavErrorMessage() + ")";

                        try { Console.WriteLine(output_error); } catch {}
                        MessageBox.Show(output_error, "Error");
                    }
                }
            }
            // catch { }
        }

        /****************************************************************************************************************************
        *                                 Observation System Report Messages Convertors
        ****************************************************************************************************************************/
        static public void MavlinkParseSysReport(byte[] packet, ref SysReport report)
        {
            object report_obj = (SysReport)Activator.CreateInstance(typeof(SysReport));
            ByteArrayToStructure(packet, ref report_obj, 0, 72);
            report = (SysReport)report_obj;
        }

        static public void MavlinkParseLosReport(byte[] packet, ref LosReport report)
        {
            object report_obj = (LosReport)Activator.CreateInstance(typeof(LosReport));
            ByteArrayToStructure(packet, ref report_obj, 0, 52);
            report = (LosReport)report_obj;
        }

        static public void MavlinkParseGndCrsReport(byte[] packet, ref GndCrsReport report)
        {
            object report_obj = (GndCrsReport)Activator.CreateInstance(typeof(GndCrsReport));
            ByteArrayToStructure(packet, ref report_obj, 0, 16);
            report = (GndCrsReport)report_obj;
        }

        static public void MavlinkParseSDCardReport(byte[] packet, ref SDCardReport report)
        {
            object report_obj = (SDCardReport)Activator.CreateInstance(typeof(SDCardReport));
            ByteArrayToStructure(packet, ref report_obj, 0, 9);
            report = (SDCardReport)report_obj;
        }

        static public void MavlinkParseVideoReport(byte[] packet, ref VideoReport report)
        {
            object report_obj = (VideoReport)Activator.CreateInstance(typeof(VideoReport));
            ByteArrayToStructure(packet, ref report_obj, 0, 8);
            report = (VideoReport)report_obj;
        }

        static public void MavlinkParseSnapshotReport(byte[] packet, ref SnapshotReport report)
        {
            object report_obj = (SnapshotReport)Activator.CreateInstance(typeof(SnapshotReport));
            ByteArrayToStructure(packet, ref report_obj, 0, 49);
            report = (SnapshotReport)report_obj;
        }

        static public void MavlinkParseLosRateReport(byte[] packet, ref LosRateReport report)
        {
            object report_obj = (LosRateReport)Activator.CreateInstance(typeof(LosRateReport));
            ByteArrayToStructure(packet, ref report_obj, 0, MAVLINK_EXT_V2_LOS_RATE_REPORT_MSG_LEN - 2);
            report = (LosRateReport)report_obj;
        }

        static public void MavlinkParseIMUReport(byte[] packet, ref IMUReport report)
        {
            object report_obj = (IMUReport)Activator.CreateInstance(typeof(IMUReport));
            ByteArrayToStructure(packet, ref report_obj, 0, MAVLINK_EXT_V2_IMU_REPORT_MSG_LEN - 2);
            report = (IMUReport)report_obj;
        }
        
        static public void MavlinkParseObjectDetectionReport(byte[] packet, ref ObjectDetectionReport report)
        {
            ObjectDetectionReport obj_det_report = new ObjectDetectionReport();
            obj_det_report.timeSinceBoot = BitConverter.ToUInt32(packet, 0);
            obj_det_report.utcTimeStamp = BitConverter.ToUInt64(packet, 4);
            obj_det_report.detections = new DetectionObject[(packet.Length - 12) / 32];
            for (int i = 0; i < obj_det_report.detections.Length; i++)
            {
                object detection_obj = (DetectionObject)Activator.CreateInstance(typeof(DetectionObject));
                ByteArrayToStructure(packet, ref detection_obj, 12 + 32 * i, 32);
                obj_det_report.detections[i] = (DetectionObject)detection_obj;
            }
            report = obj_det_report;
        }

        static public void MavlinkParseAuxCameraControlReport(byte[] packet, ref ObjectAuxCameraDetectionReport report)
        {
            ObjectAuxCameraDetectionReport aux_camera_report = new ObjectAuxCameraDetectionReport();
            aux_camera_report.timeSinceBoot = BitConverter.ToUInt32(packet, 0);
            aux_camera_report.utcTimeStamp = BitConverter.ToUInt64(packet, 4);
            aux_camera_report.aux_channel_id = packet[12];
            aux_camera_report.auxCameraDetections = new AuxCameraDetectionObject[(packet.Length - 13)/18];
            int length = Math.Min(2, aux_camera_report.auxCameraDetections.Length);
            for( int i = 0; i < length; i ++ )
            {
                object aux_camera = (AuxCameraDetectionObject)Activator.CreateInstance(typeof(AuxCameraDetectionObject));
                ByteArrayToStructure(packet, ref aux_camera, 13 + 18 * i, 18);
                aux_camera_report.auxCameraDetections[i] = (AuxCameraDetectionObject)aux_camera;
            }
            report = aux_camera_report;
        }

        static public void MavlinkParseFireDetectionReport(byte[] packet, ref FireDetectionReport report)
        {
            FireDetectionReport rep = new FireDetectionReport();
            int rep_idx = 0;
            rep.lat = BitConverter.ToSingle(packet, rep_idx);
            rep_idx += 4;
            rep.lon = BitConverter.ToSingle(packet, rep_idx);
            rep_idx += 4;
            rep.alt = BitConverter.ToSingle(packet, rep_idx);
            rep_idx += 4;
            rep.fov = BitConverter.ToSingle(packet, rep_idx);
            rep_idx += 4;
            rep.totalSatPixel = BitConverter.ToUInt16(packet, rep_idx);
            rep_idx += 2;
            rep.regionSatPixel = new byte[8];
            Array.Copy(packet, rep_idx, rep.regionSatPixel, 0, 8);
            rep_idx += 8;
            rep.timeSinceBoot = BitConverter.ToUInt32(packet, rep_idx);
            rep_idx += 4;
            rep.utcTimeStamp = BitConverter.ToUInt64(packet, rep_idx);
            report = rep;
        }

        static public void MavlinkParseTrackingReport(byte[] packet, ref TrackingReport report)
        {
            object report_obj = (TrackingReport)Activator.CreateInstance(typeof(TrackingReport));
            ByteArrayToStructure(packet, ref report_obj, 0, MAVLINK_EXT_V2_TRACKING_REPORT_DATA_LEN);
            report = (TrackingReport)report_obj;
        }

        static public void MavlinkParseLPRReport(byte[] packet, ref LPRReport report)
        {
            LPRReport lpr_report = new LPRReport();
            lpr_report.timeSinceBoot = BitConverter.ToUInt32(packet, 0);
            lpr_report.utcTimeStamp = BitConverter.ToUInt64(packet, 4);
            lpr_report.plates = new PlateObject[(packet.Length - 12) / 36];
            for (int i = 0; i < lpr_report.plates.Length; i++)
            {
                int index = 12 + 36 * i;
                lpr_report.plates[i] = new PlateObject();
                lpr_report.plates[i].plateText = new byte[16];
                Array.Copy(packet, index, lpr_report.plates[i].plateText, 0, 16);
                index += 16;
                lpr_report.plates[i].x = BitConverter.ToSingle(packet, index);
                
                index += 4;
                lpr_report.plates[i].y = BitConverter.ToSingle(packet, index);
                index += 4;
                lpr_report.plates[i].lat = BitConverter.ToSingle(packet, index);
                index += 4;
                lpr_report.plates[i].lon = BitConverter.ToSingle(packet, index);
                index += 4;
                lpr_report.plates[i].alt = BitConverter.ToSingle(packet, index);                
            }
            report = lpr_report;
        }

        static public void MavlinkParseARMarkerReport(byte[] packet, ref ARMarkerReport report)
        {
            ARMarkerReport ar_marker_report = new ARMarkerReport();
            ar_marker_report.timeSinceBoot = BitConverter.ToUInt32(packet, 0);
            ar_marker_report.utcTimeStamp = BitConverter.ToUInt64(packet, 4);
            ar_marker_report.markers = new ARMarkerObject[(packet.Length - 12) / 34];

            for (int i = 0; i < ar_marker_report.markers.Length; i++)
            {
                int index = 12 + 34 * i;
                ar_marker_report.markers[i] = new ARMarkerObject();
                ar_marker_report.markers[i].xy1 = new float[2];
                ar_marker_report.markers[i].xy2 = new float[2];
                ar_marker_report.markers[i].xy3 = new float[2];
                ar_marker_report.markers[i].xy4 = new float[2];                

                // marker ID
                ar_marker_report.markers[i].id = BitConverter.ToUInt16(packet, index);
                index += 2;                

                // x1 y1                              
                ar_marker_report.markers[i].xy1[0] = BitConverter.ToSingle(packet, index);
                index += 4;
                ar_marker_report.markers[i].xy1[1] = BitConverter.ToSingle(packet, index);
                index += 4;                                    

                // x2 y2
                ar_marker_report.markers[i].xy2[0] = BitConverter.ToSingle(packet, index);
                index += 4;
                ar_marker_report.markers[i].xy2[1] = BitConverter.ToSingle(packet, index);
                index += 4;

                // x3 y3                
                ar_marker_report.markers[i].xy3[0] = BitConverter.ToSingle(packet, index);
                index += 4;
                ar_marker_report.markers[i].xy3[1] = BitConverter.ToSingle(packet, index);
                index += 4;

                // x4 y4                
                ar_marker_report.markers[i].xy4[0] = BitConverter.ToSingle(packet, index);
                index += 4;
                ar_marker_report.markers[i].xy4[1] = BitConverter.ToSingle(packet, index);
                index += 4;
            }
            report = ar_marker_report;
        }

        static public void MavlinkParseParameterReport(byte[] packet, ref ParameterReport report)
        {
            object report_obj = (ParameterReport)Activator.CreateInstance(typeof(ParameterReport));
            ByteArrayToStructure(packet, ref report_obj, 0, MAVLINK_EXT_V2_PARAMETER_REPORT_MSG_LEN - 2);
            report = (ParameterReport)report_obj;
        }

        static public void MavlinkParseCarCountReport(byte[] packet, ref CarCountReport report)
        {
            object report_obj = (CarCountReport)Activator.CreateInstance(typeof(CarCountReport));
            ByteArrayToStructure(packet, ref report_obj, 0, MAVLINK_EXT_V2_CAR_COUNT_REPORT_MSG_LEN - 2);
            report = (CarCountReport)report_obj;
        }

        static public void MavlinkParseRangeFinderReport(byte[] packet, ref RangeFinderReport report)
        {
            object report_obj = (RangeFinderReport)Activator.CreateInstance(typeof(RangeFinderReport));
            ByteArrayToStructure(packet, ref report_obj, 0, MAVLINK_EXT_V2_CAR_COUNT_REPORT_MSG_LEN - 2);
            report = (RangeFinderReport)report_obj;
        }
        static public void MavlinkParseOGLRReport(byte[] packet, ref OGLRReport report)
        {
            object report_obj = (OGLRReport)Activator.CreateInstance(typeof(OGLRReport));
            ByteArrayToStructure(packet, ref report_obj, 0, MAVLINK_EXT_V2_OGLR_REPORT_MSG_LEN - 2);
            report = (OGLRReport)report_obj;
        }

        static public void MavlinkParseVMDReport(byte[] packet, ref VMDReport report)
        {
            VMDReport vmd_report = new VMDReport();
            vmd_report.timeSinceBoot = BitConverter.ToUInt32(packet, 0);
            vmd_report.utcTimeStamp = BitConverter.ToUInt64(packet, 4);
            vmd_report.vmdObjects = new VMDObject[(packet.Length - 12) / 28];

            for (int i = 0; i < vmd_report.vmdObjects.Length; i++)
            {
                int index = 12 + i * 28;
                vmd_report.vmdObjects[i] = new VMDObject();
                vmd_report.vmdObjects[i].x = BitConverter.ToSingle(packet, index);
                index += 4;
                vmd_report.vmdObjects[i].y = BitConverter.ToSingle(packet, index);
                index += 4;
                vmd_report.vmdObjects[i].width = BitConverter.ToSingle(packet, index);
                index += 4;
                vmd_report.vmdObjects[i].height = BitConverter.ToSingle(packet, index);
                index += 4;
                vmd_report.vmdObjects[i].latitude = BitConverter.ToSingle(packet, index);
                index += 4;
                vmd_report.vmdObjects[i].longitude = BitConverter.ToSingle(packet, index);
                index += 4;
                vmd_report.vmdObjects[i].altitude = BitConverter.ToSingle(packet, index);
                index += 4;
            }
            report = vmd_report;
        }

        static public bool MavlinkParsePLRReport(byte[] packet, ref PLR_Report report)
        {
            if (packet.Length < Marshal.SizeOf(report))
            {
                return false;
            }

            PLR_Report nav_report = new PLR_Report();
            nav_report.timeSinceBoot      = BitConverter.ToUInt32(packet, 0);
            nav_report.utcTimeStamp       = BitConverter.ToUInt64(packet, 4);
            nav_report.latitude           = BitConverter.ToDouble(packet, 8 * 0 + 12);
            nav_report.longitude          = BitConverter.ToDouble(packet, 8 * 1 + 12);
            nav_report.platform_latitude  = BitConverter.ToDouble(packet, 8 * 2 + 12);
            nav_report.platform_longitude = BitConverter.ToDouble(packet, 8 * 3 + 12);
            nav_report.frame_idx = BitConverter.ToUInt32(packet, 32);
            report = nav_report;
            return true;
        }

        /// <summary>
        /// Dumps command bytes of MavCmd* functions, without actually sending commands
        /// </summary>
        /// <param name="command_callback">callback which wraps the call to a MavCmd* function</param>
        public static byte[] DumpMavCmdCommandData(Action command_callback)
        {
            try
            {
                // Disable actual command sending, save bytes instead 
                MavManagerSetGlobalTlsCollectSendBufferMode(true);
            }
            catch
            {
                MessageBox.Show("Failed To Load The Function MavManagerSetGlobalTlsCollectSendBufferMode", "Error");
                return null;
            }

            command_callback();

            byte[] data = new byte[0];

            MavManagerCollectTlsSendBuffer((IntPtr ptr, int buf_size, int arg_unused) =>
            {
                if (buf_size == 0)
                {
                    return;
                }

                data = new byte[buf_size];
                Marshal.Copy(ptr, data, 0, buf_size);
            }, callback_argument: 0);

            // Ensure byte dump mode is disabled
            MavManagerSetGlobalTlsCollectSendBufferMode(false);
            return data;
        }

        /****************************************************************************************************************************
        *                                                 INS Messages Transmission Functions
        ****************************************************************************************************************************/
        /// <summary>
        /// Transmits the Mavlink Attitude Packet
        /// </summary>
        /// <param name="tx_packet"></param>
        /// <param name="roll">platform roll</param>
        /// <param name="pitch">platform pitch</param>
        /// <param name="yaw">platform yaw</param>
        public void MavlinkSendAttitudeMsg(ref byte[] tx_packet, float roll, float pitch, float yaw)
        {
            /* Mavlink Attitude Packet -    
             * The following values are used by the Obervation System: Roll, Pitch, Yaw */
            byte[] mavlink_attitude_packet;
            if (mavlink_protocol_ver == 0x01 || mavlink_protocol_ver == 0x00)
            {
                mavlink_attitude_packet = new byte[]
                {
                    MAVLINK_START_OF_FRAME, 0x1C, 0x00, 0x01, 0x01, 0x1E,                   /* Mavlink Header */
                    0x00, 0x00, 0x00, 0x00,                                                 /* Time Since Boot */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, /* Roll, Pitch, Yaw */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, /* Roll Speed, Pitch Speed, Yaw Speed */          
                    0x00, 0x00                                                              /* Mavlink Message CRC */
                };
            }
            else
            {
                mavlink_attitude_packet = new byte[]
               {
                    MAVLINK_2_START_OF_FRAME, 0x1C, 0x00, 0x00, 0x00, 0x01, 0x01, 0x1E, 0x00, 0x00, /* Mavlink Header */
                    0x00, 0x00, 0x00, 0x00,                                                         /* Time Since Boot */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,         /* Roll, Pitch, Yaw */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,         /* Roll Speed, Pitch Speed, Yaw Speed */          
                    0x00, 0x00                                                                      /* Mavlink Message CRC */
               };
            }

            ushort mavlink_crc;

            //Fixup to radians
            roll = DEG2RAD(roll);
            pitch = DEG2RAD(pitch);
            yaw = DEG2RAD(yaw);

            byte[] roll_byteArray = BitConverter.GetBytes(roll);
            byte[] pitch_byteArray = BitConverter.GetBytes(pitch);
            byte[] yaw_byteArray = BitConverter.GetBytes(yaw);

            /* copy the values to the outgoing message */
            roll_byteArray.CopyTo(mavlink_attitude_packet, mavlink_header_length + 4);
            pitch_byteArray.CopyTo(mavlink_attitude_packet, mavlink_header_length + 8);
            yaw_byteArray.CopyTo(mavlink_attitude_packet, mavlink_header_length + 12);

            /* calculate the mavlink message CRC */
            mavlink_crc = MavlinkCrcCalculate(mavlink_attitude_packet, mavlink_attitude_packet.Length - 2); /* All message bytes execpt the CRC bytes */
            mavlink_crc = MavlinkCrcAccumulate(MAVLINK_ATTITUDE_MSG_CRC, mavlink_crc);

            /* put the calculated CRC in the mavlink message */
            mavlink_attitude_packet[mavlink_attitude_packet.Length - 2] = (byte)mavlink_crc;
            mavlink_attitude_packet[mavlink_attitude_packet.Length - 1] = (byte)(mavlink_crc >> 8);

            /* copy the ready packet to the tx_packet */
            tx_packet = mavlink_attitude_packet.ToArray();

            MavCmdSendAttitudeMsg(mav_comm, ackCb, roll, pitch, yaw);
        }

        /// <summary>
        /// Transmits the Mavlink Global Position Int Packet
        /// </summary>
        /// <param name="tx_packet"></param>
        /// <param name="lat">platform latitude</param>
        /// <param name="lon">platfrom longitude</param>
        /// <param name="alt">platfrom altitude (above mean sea level)</param>
        /// <param name="rel_alt">platfrom altitude (above ground level)</param>
        /// <param name="vx">X ground speed</param>
        /// <param name="vy">Y ground speed</param>
        /// <param name="vz">Z ground speed</param>
        public void MavlinkSendGlobalPosIntMsg(ref byte[] tx_packet, in float lat, in float lon, in float alt, in float rel_alt, in float vx, in float vy, in float vz)
        {
            /* Mavlink Global Position Packet - 
           The following values are used by the Obervation System: Latitude, Longitude, Altitude, Relative Altitude, Vx, Vy, Vz */
            byte[] mavlink_global_pos_int_packet;
            if (mavlink_protocol_ver == 0x01 || mavlink_protocol_ver == 0x00)
            {
                mavlink_global_pos_int_packet = new byte[]
                {
                    MAVLINK_START_OF_FRAME, 0x1C, 0x00, 0x01, 0x01, 0x21,   /* Mavlink Header */
                    0x00, 0x00, 0x00, 0x00,                                 /* Time Since Boot */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,         /* Latitude, Longitude */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,         /* Altitude, Relative Altitude */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,         /* Vx, Vy, Vz, Heading */   
                    0x00, 0x00                                              /* Mavlink Message CRC */
                };
            }
            else
            {
                mavlink_global_pos_int_packet = new byte[]
                {
                    MAVLINK_2_START_OF_FRAME, 0x1C, 0x00, 0x00, 0x00, 0x01, 0x01, 0x21, 0x00, 0x00, /* Mavlink Header */
                    0x00, 0x00, 0x00, 0x00,                                                         /* Time Since Boot */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,                                 /* Latitude, Longitude */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,                                 /* Altitude, Relative Altitude */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,                                 /* Vx, Vy, Vz, Heading */   
                    0x00, 0x00                                                                      /* Mavlink Message CRC */
                };
            }

            ushort mavlink_crc;

            int lat_int = (int)(lat * 10000000.0);
            int lon_int = (int)(lon * 10000000.0);
            short vx_short = (short)(vx * 100.0);
            short vy_short = (short)(vy * 100.0);
            short vz_short = (short)(vz * 100.0);
            int alt_int = (int)(alt * 1000.0);
            int rel_alt_int = (int)(rel_alt * 1000.0);

            byte[] lat_byteArray = BitConverter.GetBytes(lat_int);
            byte[] lon_byteArray = BitConverter.GetBytes(lon_int);
            byte[] alt_byteArray = BitConverter.GetBytes(alt_int);
            byte[] rel_alt_byteArray = BitConverter.GetBytes(rel_alt_int);
            byte[] vx_byteArray = BitConverter.GetBytes(vx_short);
            byte[] vy_byteArray = BitConverter.GetBytes(vy_short);
            byte[] vz_byteArray = BitConverter.GetBytes(vz_short);

            /* copy the values the outgoing message */
            lat_byteArray.CopyTo(mavlink_global_pos_int_packet, mavlink_header_length + 4);
            lon_byteArray.CopyTo(mavlink_global_pos_int_packet, mavlink_header_length + 8);
            alt_byteArray.CopyTo(mavlink_global_pos_int_packet, mavlink_header_length + 12);
            rel_alt_byteArray.CopyTo(mavlink_global_pos_int_packet, mavlink_header_length + 16);
            vx_byteArray.CopyTo(mavlink_global_pos_int_packet, mavlink_header_length + 20);
            vy_byteArray.CopyTo(mavlink_global_pos_int_packet, mavlink_header_length + 22);
            vz_byteArray.CopyTo(mavlink_global_pos_int_packet, mavlink_header_length + 24);

            /* calculate the mavlink message CRC */
            mavlink_crc = MavlinkCrcCalculate(mavlink_global_pos_int_packet, mavlink_global_pos_int_packet.Length - 2); /* All message bytes execpt the CRC bytes */
            mavlink_crc = MavlinkCrcAccumulate(MAVLINK_GBL_POS_INT_CRC, mavlink_crc);

            /* put the calculated CRC in the mavlink message */
            mavlink_global_pos_int_packet[mavlink_global_pos_int_packet.Length - 2] = (byte)mavlink_crc;
            mavlink_global_pos_int_packet[mavlink_global_pos_int_packet.Length - 1] = (byte)(mavlink_crc >> 8);

            /* copy the ready packet to the tx_packet */
            tx_packet = mavlink_global_pos_int_packet.ToArray();

            MavCmdSendGlobalPositionInitMsg(mav_comm, ackCb, lat, lon, alt, rel_alt, vx, vy, vz);
        }

        /// <summary>
        /// Transmits the Mavlink GPS Raw Int Packet
        /// </summary>
        /// <param name="tx_packet"></param>
        /// <param name="sat_count">number of visable satelites</param>
        public void MavlinkSendGPSRawIntMsg(ref byte[] tx_packet, int sat_count)
        {
            /* Mavlink GPS Raw int Packet - 
           The following values are used by the Obervation System: Satellites Visable */
            byte[] mavlink_gps_raw_int_packet;
            if (mavlink_protocol_ver == 0x01 || mavlink_protocol_ver == 0x00)
            {
                mavlink_gps_raw_int_packet = new byte[]
                {
                    MAVLINK_START_OF_FRAME, 0x1E, 0x00, 0x01, 0x01, 0x18,                   /* Mavlink Header */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,                         /* TimeStamp */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, /* Latitude, Longitude, Altitude */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,                         /* Eph, Epv, Vel, Cog */
                    0x00,                                                                   /* Fix Type */   
                    0x00,                                                                   /* Satellites Visable */
                    0x00, 0x00                                                              /* Mavlink Message CRC */
                };
            }
            else
            {
                mavlink_gps_raw_int_packet = new byte[]
                {
                    MAVLINK_2_START_OF_FRAME, 0x1E, 0x00, 0x00, 0x00, 0x01, 0x01, 0x18, 0x00, 0x00, /* Mavlink Header */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,                                 /* TimeStamp */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,         /* Latitude, Longitude, Altitude */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,                                 /* Eph, Epv, Vel, Cog */
                    0x00,                                                                           /* Fix Type */   
                    0x00,                                                                           /* Satellites Visable */
                    0x00, 0x00                                                                      /* Mavlink Message CRC */
                };
            }

            ushort mavlink_crc;

            /* update the satelite count in the outgoing message */
            mavlink_gps_raw_int_packet[mavlink_header_length + 29] = (byte)sat_count;

            /* calculate the mavlink message CRC */
            mavlink_crc = MavlinkCrcCalculate(mavlink_gps_raw_int_packet, mavlink_gps_raw_int_packet.Length - 2); /* All message bytes execpt the CRC bytes */
            mavlink_crc = MavlinkCrcAccumulate(MAVLINK_GPS_RAW_INT_CRC, mavlink_crc);

            /* put the calculated CRC in the mavlink message */
            mavlink_gps_raw_int_packet[mavlink_gps_raw_int_packet.Length - 2] = (byte)mavlink_crc;
            mavlink_gps_raw_int_packet[mavlink_gps_raw_int_packet.Length - 1] = (byte)(mavlink_crc >> 8);

            /* copy the ready packet to the tx_packet */
            tx_packet = mavlink_gps_raw_int_packet.ToArray();
            MavCmdSendGpsRawInitMsg(mav_comm, ackCb, sat_count);
        }

        /// <summary>
        /// Transmits the Mavlink System Status Packet
        /// </summary>
        /// <param name="tx_packet"></param>
        /// <param name="batt_voltage">battery voltage</param>
        public void MavlinkSendSysStatusMsg(ref byte[] tx_packet, float batt_voltage)
        {
            /* Mavlink System Status Packet - 
            The following values are used by the Obervation System: Battery Voltage */
            byte[] mavlink_sys_status_packet;
            if (mavlink_protocol_ver == 0x01 || mavlink_protocol_ver == 0x00)
            {
                mavlink_sys_status_packet = new byte[]
                {
                    MAVLINK_START_OF_FRAME, 0x1F, 0x00, 0x01, 0x01, 0x01,                   /* Mavlink Header */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, /* OCS Present, OCS Enabled, OCS Health */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,                         /* Load, Battery Voltage, Battery Current, Drop Rate */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,             /* Error Counters */
                    0x00,                                                                   /* Battery Remaining */   
                    0x00, 0x00                                                              /* Mavlink Message CRC */
                };
            }
            else
            {
                mavlink_sys_status_packet = new byte[]
                {
                    MAVLINK_2_START_OF_FRAME, 0x1F, 0x00, 0x00, 0x00, 0x01, 0x01, 0x01, 0x00, 0x00, /* Mavlink Header */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,         /* OCS Present, OCS Enabled, OCS Health */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,                                 /* Load, Battery Voltage, Battery Current, Drop Rate */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,                     /* Error Counters */
                    0x00,                                                                           /* Battery Remaining */   
                    0x00, 0x00                                                                      /* Mavlink Message CRC */
                };
            }

            ushort mavlink_crc;

            short batt_voltage_short = (short)(batt_voltage * 1000.0);
            byte[] batt_voltage_byteArray = BitConverter.GetBytes(batt_voltage_short);

            /* update the battery voltage in the outgoing message */
            batt_voltage_byteArray.CopyTo(mavlink_sys_status_packet, mavlink_header_length + 14);

            /* calculate the mavlink message CRC */
            mavlink_crc = MavlinkCrcCalculate(mavlink_sys_status_packet, mavlink_sys_status_packet.Length - 2); /* All message bytes execpt the CRC bytes */
            mavlink_crc = MavlinkCrcAccumulate(MAVLINK_SYS_STAT_CRC, mavlink_crc);

            /* put the calculated CRC in the mavlink message */
            mavlink_sys_status_packet[mavlink_sys_status_packet.Length - 2] = (byte)mavlink_crc;
            mavlink_sys_status_packet[mavlink_sys_status_packet.Length - 1] = (byte)(mavlink_crc >> 8);

            /* copy the ready packet to the tx_packet */
            tx_packet = mavlink_sys_status_packet.ToArray();

            MavCmdSendSysStatusMsg(mav_comm, ackCb, (UInt16)batt_voltage);
        }

        /// <summary>
        /// Transmits the Mavlink Heartbeat Packet
        /// </summary>
        /// <param name="tx_packet"></param>
        /// <param name="drone_armed">is the platfrom armed (motors are running, ready to takeoff)</param>
        public void MavlinkSendHeartbeatMsg(ref byte[] tx_packet, bool drone_armed)
        {
           /* Mavlink Heart Beat Packet - 
           The following values are used by the Obervation System: Base Mode */
            byte[] mavlink_heart_beat_packet;
            if (mavlink_protocol_ver == 0x01 || mavlink_protocol_ver == 0x00)
            {
                mavlink_heart_beat_packet = new byte[]
                {
                    MAVLINK_START_OF_FRAME, 0x09, 0x00, 0x01, 0x01, 0x00,   /* Mavlink Header */
                    0x00, 0x00, 0x00, 0x00,                                 /* Custom Mode */
                    0x00, 0x03,                                             /* Mav Type, AutoPilot Type */ 
                    0x00, 0x00, 0x00,                                       /* Base Mode, System Status Flag, Mavlink Version */
                    0x00, 0x00                                              /* Mavlink Message CRC */
                };
            }
            else
            {
                mavlink_heart_beat_packet = new byte[]
                {
                    MAVLINK_2_START_OF_FRAME, 0x09, 0x00, 0x00, 0x00, 0x01, 0x01, 0x00, 0x00, 0x00, /* Mavlink Header */
                    0x00, 0x00, 0x00, 0x00,                                                         /* Custom Mode */
                    0x00, 0x03,                                                                     /* Mav Type, AutoPilot Type */ 
                    0x00, 0x00, 0x00,                                                               /* Base Mode, System Status Flag, Mavlink Version */
                    0x00, 0x00                                                                      /* Mavlink Message CRC */
                };
            }

            ushort mavlink_crc;

            /* update the drone armed state */
            if (drone_armed)
                mavlink_heart_beat_packet[mavlink_header_length + 6] |= MAVLINK_DRONE_ARMED_MASK;
            else
                mavlink_heart_beat_packet[mavlink_header_length + 6] &= (MAVLINK_DRONE_ARMED_MASK ^ 0xFF);

            /* calculate the mavlink message CRC */
            mavlink_crc = MavlinkCrcCalculate(mavlink_heart_beat_packet, mavlink_heart_beat_packet.Length - 2); /* All message bytes execpt the CRC bytes */
            mavlink_crc = MavlinkCrcAccumulate(MAVLINK_HEART_BEAT_CRC, mavlink_crc);

            /* put the calculated CRC in the mavlink message */
            mavlink_heart_beat_packet[mavlink_heart_beat_packet.Length - 2] = (byte)mavlink_crc;
            mavlink_heart_beat_packet[mavlink_heart_beat_packet.Length - 1] = (byte)(mavlink_crc >> 8);

            /* copy the ready packet to the tx_packet */
            tx_packet = mavlink_heart_beat_packet.ToArray();

            MavCmdSendHeartbeatMsg(mav_comm, ackCb, drone_armed ? 1 : 0);
        }

        /// <summary>
        /// Transmits the Mavlink System Time Packet
        /// </summary>
        /// <param name="tx_packet"></param>
        /// <param name="epoch_timestamp">timestamp in epoch</param>
        public void MavlinkSendSysTimeMsg(ref byte[] tx_packet, int epoch_timestamp)
        {
            /* Mavlink System Time Packet - 
            The following values are used by the Obervation System: TimeStamp */
            byte[] mavlink_system_time_packet;
            if (mavlink_protocol_ver == 0x01 || mavlink_protocol_ver == 0x00)
            {
                mavlink_system_time_packet = new byte[]
                {
                    MAVLINK_START_OF_FRAME, 0x0C, 0x00, 0x01, 0x01, 0x02,   /* Mavlink Header */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,         /* TimeStamp */
                    0x00, 0x00, 0x00, 0x00,                                 /* Time Since Boot */
                    0x00, 0x00                                              /* Mavlink Message CRC */
                };
            }
            else
            {
                mavlink_system_time_packet = new byte[]
                {
                    MAVLINK_2_START_OF_FRAME, 0x0C, 0x00, 0x00, 0x00, 0x01, 0x01, 0x02, 0x00, 0x00, /* Mavlink Header */
                    0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00,                                 /* TimeStamp */
                    0x00, 0x00, 0x00, 0x00,                                                         /* Time Since Boot */
                    0x00, 0x00                                                                      /* Mavlink Message CRC */
                };
            }

            ushort mavlink_crc;
            UInt64 epoch_timestamp_long = ((UInt64)epoch_timestamp * 1000000);
            byte[] epoch_byteArray = BitConverter.GetBytes(epoch_timestamp_long);

            /* copy the timestamp to the outgoing message */
            epoch_byteArray.CopyTo(mavlink_system_time_packet, mavlink_header_length);

            /* calculate the mavlink message CRC */
            mavlink_crc = MavlinkCrcCalculate(mavlink_system_time_packet, mavlink_system_time_packet.Length - 2); /* All message bytes execpt the CRC bytes */
            mavlink_crc = MavlinkCrcAccumulate(MAVLINK_SYS_TIME_CRC, mavlink_crc);

            /* put the calculated CRC in the mavlink message */
            mavlink_system_time_packet[mavlink_system_time_packet.Length - 2] = (byte)mavlink_crc;
            mavlink_system_time_packet[mavlink_system_time_packet.Length - 1] = (byte)(mavlink_crc >> 8);

            /* copy the ready packet to the tx_packet */
            tx_packet = mavlink_system_time_packet.ToArray();

            MavCmdSendSysTimeMsg(mav_comm, ackCb, epoch_timestamp);
        }

        /// <summary>
        /// Transmits the Mavlink Command Long Message - Sub Command: DO_MOUNT_CONTROL
        /// </summary>
        /// <param name="tx_packet">will be filled with a the packet data</param>
        /// <param name="mount_mode">new mount mode</param>
        public void MavlinkSendMountControlMsg(ref byte[] tx_packet, MavMountMode mount_mode)
        {
            /* Mavlink Mount Control Packet */
            byte[] mavlink_cmd_long_packet;
            if (mavlink_protocol_ver == 0x01)
            {
                mavlink_cmd_long_packet = new byte[]
                {
                    MAVLINK_START_OF_FRAME, 0x21, 0x00, 0xFF, 0x00, 0x4C,   /* Mavlink Header */
                    0x00, 0x00, 0x00, 0x00,                                 /* Param1 */
                    0x00, 0x00, 0x00, 0x00,                                 /* Param2 */
                    0x00, 0x00, 0x00, 0x00,                                 /* Param3 */
                    0x00, 0x00, 0x00, 0x00,                                 /* Param4 */
                    0x00, 0x00, 0x00, 0x00,                                 /* Param5 */
                    0x00, 0x00, 0x00, 0x00,                                 /* Param6 */
                    0x00, 0x00, 0x00, 0x00,                                 /* Param7 - MAV_MOUNT_MODE */
                    0xCD, 0x00,                                             /* Command - DO_MOUNT_CONTROL  */
                    0x01,                                                   /* Target System */
                    0x00,                                                   /* Target Component */
                    0x00,                                                   /* Confirmation */
                    0x00, 0x00                                              /* Mavlink Message CRC */
                };
            }
            else
            {
                mavlink_cmd_long_packet = new byte[]
                {
                    MAVLINK_2_START_OF_FRAME,  0x21, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x4C, 0x00, 0x00,    /* Mavlink Header */
                    0x00, 0x00, 0x00, 0x00,                                                             /* Param1 */
                    0x00, 0x00, 0x00, 0x00,                                                             /* Param2 */
                    0x00, 0x00, 0x00, 0x00,                                                             /* Param3 */
                    0x00, 0x00, 0x00, 0x00,                                                             /* Param4 */
                    0x00, 0x00, 0x00, 0x00,                                                             /* Param5 */
                    0x00, 0x00, 0x00, 0x00,                                                             /* Param6 */
                    0x00, 0x00, 0x00, 0x00,                                                             /* Param7 - MAV_MOUNT_MODE */
                    0xCD, 0x00,                                                                         /* Command - DO_MOUNT_CONTROL  */
                    0x01,                                                                               /* Target System */
                    0x00,                                                                               /* Target Component */
                    0x00,                                                                               /* Confirmation */
                    0x00, 0x00                                                                          /* Mavlink Message CRC */
                };
            }

            ushort mavlink_crc;
            byte[] mount_mode_byteArray = BitConverter.GetBytes((float)mount_mode);

            /* copy the values to the outgoing message */
            mount_mode_byteArray.CopyTo(mavlink_cmd_long_packet, mavlink_header_length + 24);

            /* calculate the mavlink message CRC */
            mavlink_crc = MavlinkCrcCalculate(mavlink_cmd_long_packet, mavlink_cmd_long_packet.Length - 2); /* All message bytes execpt the CRC bytes */
            mavlink_crc = MavlinkCrcAccumulate(MAVLINK_CMD_LONG_CRC, mavlink_crc);

            /* put the calculated CRC in the mavlink message */
            mavlink_cmd_long_packet[mavlink_cmd_long_packet.Length - 2] = (byte)mavlink_crc;
            mavlink_cmd_long_packet[mavlink_cmd_long_packet.Length - 1] = (byte)(mavlink_crc >> 8);

            /* copy the ready packet to the tx_packet */
            tx_packet = mavlink_cmd_long_packet.ToArray();

            /* Send the message */
            MavCmdDoMountControl(mav_comm, ackCb, (int)mount_mode);
        }

        /****************************************************************************************************************************
        *                                                 MISC FUNCTIONS
        ****************************************************************************************************************************/

        /// <summary>
        /// calculates CRC for Mavlink Protocol
        /// </summary>
        /// <param name="pBuffer">buffer pointer</param>
        /// <param name="length">buffer length</param>
        /// <returns>CRC</returns>
        private ushort MavlinkCrcCalculate(byte[] pBuffer, int length)
        {
            ushort crcTmp;
            int i;

            if (length < 1)
            {
                return 0xffff;
            }

            crcTmp = X25_INIT_CRC;

            for (i = 1; i < length; i++) // skips header
            {
                crcTmp = MavlinkCrcAccumulate(pBuffer[i], crcTmp);
            }

            return (crcTmp);
        }

        /// <summary>
        /// Adds additional byte to CRC calculate
        /// </summary>
        /// <param name="b">byte</param>
        /// <param name="crc">previous CRC</param>
        /// <returns>new CRC</returns>
        private ushort MavlinkCrcAccumulate(byte b, ushort crc)
        {
            byte ch = (byte)(b ^ (byte)(crc & 0x00ff));
            ch = (byte)(ch ^ (ch << 4));

            return (ushort)((crc >> 8) ^ ((ushort)ch << 8) ^ ((ushort)ch << 3) ^ ((ushort)ch >> 4));
        }

        /// <summary>
        /// Used to convert byte array report messages to structs
        /// </summary>
        /// <param name="bytearray">input byte array</param>
        /// <param name="obj">object to fill</param>
        /// <param name="startoffset">starting offset</param>
        /// <param name="payloadlength">conversion size</param>
        public static void ByteArrayToStructure(byte[] bytearray, ref object obj, int startoffset, int payloadlength = 0)
        {
            int len = Marshal.SizeOf(obj);

            IntPtr iptr = Marshal.AllocHGlobal(len);

            // clear memory
            for (int i = 0; i < len; i += 8)
            {
                Marshal.WriteInt64(iptr, i, 0x00);
            }

            for (int i = len - (len % 8); i < len; i++)
            {
                Marshal.WriteByte(iptr, i, 0x00);
            }

            // copy byte array to ptr (prevent out of bound reads and writes)
            if (startoffset < bytearray.Length)
            {
                Marshal.Copy(bytearray, startoffset, iptr, Math.Min(Math.Min(len, payloadlength), bytearray.Length - startoffset));
            }

            obj = Marshal.PtrToStructure(iptr, obj.GetType());

            Marshal.FreeHGlobal(iptr);
        }

        /// <summary>
        /// Convert Degree To Radian 
        /// </summary>
        /// <param name="angle">angle in degree</param>
        /// <returns>angle in rad</returns>
        public float DEG2RAD(float angle)
        {
            return (float)(Math.PI * angle / 180.0);
        }

        /// <summary>
        /// Convert Radian To Degree
        /// </summary>
        /// <param name="angle">angle in radian</param>
        /// <returns>angle in deg</returns>
        public float RAD2DEG(float angle)
        {
            return (float)(angle * (180.0 / Math.PI));
        }
    }

    /// <summary>
    /// Converter class for any type
    /// </summary>
    class converter
    {
        // Double can hold any UInt32 without losing precision!
        // So it is the perfect type to wrap this mess
        private double a = 0;
        private string opt_string;

        public static implicit operator converter(float arg)
        {
            converter b = new converter();
            b.a = (double)arg;
            return b;
        }
        public static implicit operator converter(Int32 arg)
        {
            converter b = new converter();
            b.a = (double)arg;
            return b;
        }

        public static implicit operator converter(byte arg)
        {
            converter b = new converter();
            b.a = (double)arg;
            return b;
        }
        public static implicit operator converter(UInt16 arg)
        {
            converter b = new converter();
            b.a = (double)arg;
            return b;
        }
        public static implicit operator converter(UInt32 arg)
        {
            converter b = new converter();
            b.a = (double)arg;
            return b;
        }
        public static implicit operator converter(double arg)
        {
            converter b = new converter();
            b.a = arg;
            return b;
        }
        public static implicit operator converter(bool arg)
        {
            converter b = new converter();
            b.a = (double)(arg ? 1 : 0);
            return b;
        }
        public static implicit operator converter(string arg)
        {
            converter b = new converter();
            b.opt_string = arg;
            return b;
        }
        public static converter operator +(converter a, bool arg)
        {
            a = arg;
            return a;
        }
        public static converter operator +(converter a, Int32 arg)
        {
            a = arg;
            return a;
        }
        public static converter operator +(converter a, UInt32 arg)
        {
            a = arg;
            return a;
        }
        public static converter operator +(converter a, float arg)
        {
            a = arg;
            return a;
        }
        public static converter operator +(converter a, double arg)
        {
            a = arg;
            return a;
        }
        public static implicit operator double(converter a)
        {
            return a == null ? 0 : a.a;
        }
        public static implicit operator float(converter a)
        {
            return a == null ? 0 : (float)a.a;
        }
        public static implicit operator Int32(converter a)
        {
            return a == null ? 0 : (Int32)a.a;
        }
        public static implicit operator UInt16(converter a)
        {
            return a == null ? (UInt16)0 : (UInt16)a.a;
        }
        public static implicit operator UInt32(converter a)
        {
            return a == null ? 0 : (UInt32)a.a;
        }
        public static implicit operator bool(converter a)
        {
            return a != null && a.a == 1.0 ? true : false;
        }
        public static implicit operator string(converter a)
        {
            return a != null ? a.ToString() : "";
        }
        public override string ToString()
        {
            return string.IsNullOrEmpty(opt_string) ? Math.Round(a, 2, MidpointRounding.ToEven).ToString() : opt_string;
        }
    };
}
