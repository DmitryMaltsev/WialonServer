using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WialonServer.Services
{
    public class TcpClientService
    {
        string _clientId { get; set; }
        TcpClient _tcpClient { get; set; }
        NetworkStream _clientStream { get; set; }
        public TcpClientService(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            _clientId = Guid.NewGuid().ToString();
            _clientStream = _tcpClient.GetStream();
        }


    }
}
