using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using WialonServer.Models.Interfaces;

namespace WialonServer.Models
{
    public class ClientModel:IClientModel
    {
        public string ClientId { get; set; }
        public NetworkStream NetWorkStream { get; set; }
        public TcpClient ClientTcp { get; set; }
        public List<byte> RecievedDataList { get; set; }
    }
}
