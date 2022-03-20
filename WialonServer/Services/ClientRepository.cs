﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WialonServer.Services
{
    public class ClientRepository<T>
    {
        public string ClientId { get; set; }
        public NetworkStream NetWorkStream { get; set; }
        public TcpClient ClientTcp { get; set; }
        public List<T> RecievedDataList { get; set; }
    }
}