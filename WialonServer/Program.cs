using System;
using System.Threading;

using WialonServer.Models;
using WialonServer.Services;

namespace WialonServer
{
    class Program
    {
        static TcpServerService TCPServerService;


        static void Main(string[] args)
        {
            //TCPServerService = new TcpServerService();
            //Thread threadListen = new Thread(() => TCPServerService.StartLIstening(8888));
            //threadListen.Start();
            WialonParsingService parsingService = new WialonParsingService();
            WialonDataModel wialonDataModel = parsingService.ParseData(parsingService.data);
        }
    }
}
