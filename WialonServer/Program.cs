using System;
using System.Threading;

using WialonServer.Services;

namespace WialonServer
{
    class Program
    {
        static ServerObject serverObject;
        static Thread threadListen;
        
        static void Main(string[] args)
        {
            try
            {
                serverObject = new ServerObject();
                threadListen = new Thread(() => serverObject.Listen());
                threadListen.Start();
            }
            catch (Exception)
            {
                serverObject.Disconnect();

            }

        }
    }
}
