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
    class TcpServerService
    {
        public static List<TcpClientService> TcpClientServices;
        public TcpServerService()
        {
            TcpClientServices = new List<TcpClientService>();
        }

        public void StartLIstening(int port)
        {
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Any, 8888);
                listener.Start();
                Console.WriteLine("Сервер начал свою работу");
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    TcpClientService tcpClientService = new TcpClientService(client);
                    TcpClientServices.Add(tcpClientService);
                    Console.WriteLine($"Клинет с id: {tcpClientService._clientId} подключился");
                    Thread thread = new Thread(() => tcpClientService.Process());
                    thread.Start();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseAllConnections();
            }
        }

        public void CloseAllConnections()
        {
            if (TcpClientServices != null && TcpClientServices.Count > 0)
            {
                foreach (TcpClientService tcpClientService in TcpClientServices)
                {
                    tcpClientService.Disconnect();
                }
            }
        }
    }
}
