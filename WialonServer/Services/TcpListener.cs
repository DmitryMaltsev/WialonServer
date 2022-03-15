using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WialonServer.Services
{
    public class StateObject
    {
        // Size of receive buffer.  
        public const int BufferSize = 1024;

        // Receive buffer.  
        public byte[] buffer = new byte[BufferSize];

        // Received data string.
        public StringBuilder sb = new StringBuilder();

        // Client socket.
        public Socket workSocket = null;
    }

    public class TcpListener
    {
        //thread signal
        public  ManualResetEvent allDone = new ManualResetEvent(false);

        public void StartListening()
        {
            IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11_000);

            //Create a TCP/IP socket.
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(100);
                Console.WriteLine("Wait for connection");
                listener.BeginAccept(AcceptCallback, listener);





            }
            catch (Exception)
            {

                throw;
            }
        }

        private void AcceptCallback(IAsyncResult asyncResult)
        {
            // Signal the main thread to continue.  
            allDone.Set();
            Socket listener = (Socket)asyncResult.AsyncState;

        }
    }
}
