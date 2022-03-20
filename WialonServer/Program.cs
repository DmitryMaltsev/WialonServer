using System;
using System.Threading;

using WialonServer.Services;

namespace WialonServer
{
    class Program
    {
        static TcpServerService TCPServerService;


        static void Main(string[] args)
        {
            TCPServerService = new TcpServerService();
            Thread threadListen = new Thread(() => TCPServerService.StartLIstening(8888));
            threadListen.Start();
        }
    }
}
