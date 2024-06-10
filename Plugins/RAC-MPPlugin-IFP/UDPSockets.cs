using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace RAC_MPPlugin_IFP
{
    public class PacketReceivedEventArgs
    {
        public PacketReceivedEventArgs(byte[] s) { P = s; }
        public byte[] P;
    }

    public class UDPSocket
    {
        private Socket _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
        private const int bufSize = 8 * 1024;
        private State state = new State();
        private EndPoint epFrom = new IPEndPoint(IPAddress.Any, 0);
        private AsyncCallback recv = null;

        public delegate void PacketReceivedEventHandler(object sender, PacketReceivedEventArgs e);
        public event PacketReceivedEventHandler PacketReceivedEvent;

        public class State
        {
            public byte[] buffer = new byte[bufSize];
        }

        public void Server(string address, int port)
        {
            _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.ReuseAddress, true);
            _socket.Bind(new IPEndPoint(IPAddress.Parse(address), port));
            Receive();
        }

        public void Client(string address, int port)
        {
            _socket.Connect(IPAddress.Parse(address), port);
            //Receive();
        }

        public void Send(byte[] data)
        {
            // byte[] data = Encoding.ASCII.GetBytes(text);
            _socket.BeginSend(data, 0, data.Length, SocketFlags.None, (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _socket.EndSend(ar);
                //Console.WriteLine("SEND: {0}, {1}", bytes, data);
            }, state);
        }

        private void Receive()
        {
            _socket.BeginReceiveFrom(state.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv = (ar) =>
            {
                State so = (State)ar.AsyncState;
                int bytes = _socket.EndReceiveFrom(ar, ref epFrom);

                _socket.BeginReceiveFrom(so.buffer, 0, bufSize, SocketFlags.None, ref epFrom, recv, so);

                byte[] p = new byte[bytes];
                Array.Copy(so.buffer, p, bytes);

                PacketReceivedEvent?.Invoke(this, new PacketReceivedEventArgs(p));

                Console.WriteLine("RECV: {0}: {1}, {2}", epFrom.ToString(), bytes, Encoding.ASCII.GetString(so.buffer, 0, bytes));

            }, state);
        }
    }
}
