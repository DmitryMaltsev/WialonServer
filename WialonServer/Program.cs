using System;

using WialonServer.Services;

namespace WialonServer
{
    class Program
    {
        static void Main(string[] args)
        {
            OtherTcpListener otherTcpListener = new OtherTcpListener();
            otherTcpListener.CreateServer();
        }
    }
}
