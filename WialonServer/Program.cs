using Services;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Timers;

using WialonServer.Models;
using WialonServer.Services;
using WialonServer.Services.Interfaces;

namespace WialonServer
{
    class Program
    {
        static void Main(string[] args)
        {
            IWialonParsingService _parsingService = new WialonParsingService();
            IJsonService _jsonService = new JsonService();
            ITcpServerService _serverService = new TcpServerService(_jsonService, _parsingService); 
            Thread threadListen = new Thread(() => _serverService.StartLIstening(8888));
            threadListen.Start();
        }
    }
}
