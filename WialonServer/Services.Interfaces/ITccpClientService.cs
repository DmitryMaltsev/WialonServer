using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using WialonServer.Models.Interfaces;

namespace WialonServer.Services.Interfaces
{
    public interface ITcpClientservice
    {
        IClientModel ClientModel { get; set; }
        bool IsDataRecieved { get; set; }

        public event EventHandler<List<byte>> DataRecievedEvent;
        public event EventHandler<string> ClientClosingEvent;
        void Disconnect();
        void Process();
        bool RecieveData();
        void SendData(byte[] sendingData);

    }
}
