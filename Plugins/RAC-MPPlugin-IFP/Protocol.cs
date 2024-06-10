using Force.Crc32;
using System;
using System.Runtime.InteropServices;

namespace RAC_MPPlugin_IFP
{
    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct IMKTelemetrypacket
    {
        public byte header1;
        public byte header2;
        public float lat;
        public float lng;
        public short amsl;
        public short relAlt;
        public short heading;
        public byte status;
        public uint checksum;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 2)]
    public struct IMKFlyTopacket
    {
        public byte header1;
        public byte header2;
        public float lat;
        public float lng;
        public uint checksum;
    }

    [Flags]
    public enum AircraftStatus { NOK = 0, OK = 1 }

    [Flags]
    public enum FlyToResponse { Idle = 0, ACK = 1, NACK = 2, OperatorWait = 3 }

    public static class IMKProtocol
    {
        /// <summary>
        /// Create byte array from IMKTelemetrypacket struct; 
        /// </summary>
        /// <param name="packet"> A packet to be marshaled to byte array (header and crc will be overwritten).</param>
        /// <returns>a byte array with a calculated 32bit crc at the end</returns>
        /// 
        public static byte[] GenerateRAWTelemetryPacket(IMKTelemetrypacket packet)
        {
            packet.header1 = 0xaa;
            packet.header2 = 0x55;

            //Convert from struct to byte array.
            byte[] rawpacket = null;
            IntPtr ptr = IntPtr.Zero;

            int size = Marshal.SizeOf(packet);
            rawpacket = new byte[size];
            ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(packet, ptr, true);
            Marshal.Copy(ptr, rawpacket, 0, size);
            Marshal.FreeHGlobal(ptr);

            //packetized stuff is in ptr now, calc the crc
            Crc32Algorithm.ComputeAndWriteToEnd(rawpacket);

            return rawpacket;
        }

        /// <summary>
        /// Convert the incoming byte array to IMKTelemtrypacket
        /// </summary>
        /// <param name="rawpacket">byte array of incoming data.</param>
        /// <param name="packet">reference to the decoded packet .</param>
        /// <returns>true if decode successfullm, false if size or crc does not match</returns>
        public static bool ExpandRAWTelemetryPacket(byte[] rawpacket, ref IMKTelemetrypacket packet)
        {
            //first of all check the size 
            if (rawpacket.Length != Marshal.SizeOf(packet)) return false;
            if (!Crc32Algorithm.IsValidWithCrcAtEnd(rawpacket)) return false;

            int size = Marshal.SizeOf(packet);
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(rawpacket, 0, ptr, size);
            packet = (IMKTelemetrypacket)Marshal.PtrToStructure(ptr, typeof(IMKTelemetrypacket));
            Marshal.FreeHGlobal(ptr);

            return true;
        }

        /// <summary>
        /// Create byte array from IMKFlyTopacket struct; 
        /// </summary>
        /// <param name="packet"> A packet to be marshaled to byte array (header and crc will be overwritten).</param>
        /// <returns>a byte array with a calculated 32bit crc at the end</returns>
        /// 
        public static byte[] GenerateRAWFlytoPacket(IMKFlyTopacket packet)
        {
            packet.header1 = 0xaa;
            packet.header2 = 0x55;

            //Convert from struct to byte array.
            byte[] rawpacket = null;
            IntPtr ptr = IntPtr.Zero;

            int size = Marshal.SizeOf(packet);
            rawpacket = new byte[size];
            ptr = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(packet, ptr, true);
            Marshal.Copy(ptr, rawpacket, 0, size);
            Marshal.FreeHGlobal(ptr);

            //packetized stuff is in ptr now, calc the crc
            Crc32Algorithm.ComputeAndWriteToEnd(rawpacket);

            return rawpacket;
        }

        /// <summary>
        /// Convert the incoming byte array to IMKFlyTopacket struct
        /// </summary>
        /// <param name="rawpacket">byte array of incoming data.</param>
        /// <param name="packet">reference to the decoded packet .</param>
        /// <returns>true if decode successfull, false if size or crc does not match</returns>
        public static bool ExpandRAWFlyToPacket(byte[] rawpacket, ref IMKFlyTopacket packet)
        {
            //first of all check the size 
            if (rawpacket.Length != Marshal.SizeOf(packet)) return false;
            if (!Crc32Algorithm.IsValidWithCrcAtEnd(rawpacket)) return false;

            int size = Marshal.SizeOf(packet);
            IntPtr ptr = Marshal.AllocHGlobal(size);

            Marshal.Copy(rawpacket, 0, ptr, size);
            packet = (IMKFlyTopacket)Marshal.PtrToStructure(ptr, typeof(IMKFlyTopacket));
            Marshal.FreeHGlobal(ptr);

            return true;
        }

        /// <summary>
        /// Gets the aircraft status.
        /// </summary>
        /// <param name="flags">The flags byte from the packet</param>
        /// <returns></returns>
        public static AircraftStatus GetAircraftStatus(byte flags)
        {
            if ((flags & 0x01) == 1) return AircraftStatus.OK;
            else return AircraftStatus.NOK;
        }

        /// <summary>
        /// Gets the fly to response.
        /// </summary>
        /// <param name="flags">The flags byte from the packet</param>
        /// <returns></returns>
        public static FlyToResponse GetFlyToResponse(byte flags)
        {
            var stat = (flags & 0x06) >> 1;
            switch (stat)
            {
                case 0: return FlyToResponse.Idle;
                case 1: return FlyToResponse.ACK;
                case 2: return FlyToResponse.NACK;
                case 3: return FlyToResponse.OperatorWait;
                default: return FlyToResponse.Idle;
            }
        }

        /// <summary>
        /// Set the status byte based on input
        /// </summary>
        /// <param name="a">a - aircraft status</param>
        /// <param name="f">f - fly to responese.</param>
        /// <returns></returns>
        public static byte setstatus(AircraftStatus a, FlyToResponse f)
        {
            return (byte)((byte)a | (byte)f << 1);
        }
    }
}
