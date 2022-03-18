using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WialonServer.Services
{
    class TcpServerService
    {
        public void StartLIstening(int port)
        {
            TcpListener tcpListener = new TcpListener(port);
            TcpClient client = tcpListener.AcceptTcpClient();
        }
    }
}
