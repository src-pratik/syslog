using System;
using System.Net;

namespace Basics.Syslog.Net
{
    public class MessageReceivedArgs : EventArgs
    {
        public IPEndPoint IPEndPoint { get; set; }
        public byte[] Data { get; set; }
        public DateTime DateTime { get; set; }

    }

}
