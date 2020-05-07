using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Basics.Syslog.Net
{
    public class UdpListener
    {
        private UdpClient _udpClient;
        private int _port;

        public event EventHandler<MessageReceivedArgs> MessageReceived;

        public UdpListener(int port)
        {
            _port = port;
        }

        public void Start()
        {
            if (_udpClient is null) _udpClient = new UdpClient(_port);

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, _port);

            while (true)
            {
                var receivedBytes = _udpClient.Receive(ref endPoint);
                OnDataReceived(new MessageReceivedArgs()
                {
                    IPEndPoint = endPoint,
                    Data = receivedBytes,
                    DateTime = DateTime.Now
                });
            }
        }

        protected virtual void OnDataReceived(MessageReceivedArgs e)
        {
            MessageReceived?.Invoke(this, e);
        }
    }
}
