using System;

using WialonServer.Services;

namespace WialonServer
{
    class Program
    {
        static void Main(string[] args)
        {
            SocketTcpListener otherTcpListener = new SocketTcpListener();
            otherTcpListener.StartListening();
            Console.Read();
        }
    }
}
