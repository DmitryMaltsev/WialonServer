using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WialonServer.Services
{
    class OtherTcpListener
    {
        public void CreateServer()
        {
            TcpListener server = null;

            try
            {
                IPAddress address = IPAddress.Parse("127.0.0.1");
                int port = 8888;
                server = new TcpListener(address, port);
                //Запуск слушателя
                server.Start();
                Byte[] bytes = new byte[256];
                string data = null;
                while (true)
                {
                    Console.Write("Waiting for a connection... ");
                    TcpClient client = server.AcceptTcpClient();
                    Console.WriteLine("Connected");

                    NetworkStream stream = client.GetStream();

                    while (stream.Read(bytes, 0, bytes.Length) != 0)
                    {
                        data = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
                        Console.WriteLine($"Recieve: {data}");
                        data = "Hello client";
                        byte[] msg = Encoding.ASCII.GetBytes(data);
                        stream.Write(msg, 0, msg.Length);
                        Console.WriteLine($"Send:{data} ");
                    }
                    client.Close();
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                server.Stop();
            }

            Console.WriteLine("\nHit enter to continue...");
            Console.Read();
        }

    }
}
