using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WialonServer.Services
{
    class TcpServerService
    {
        public List<TcpClientService> TcpClientServices;
        public TcpServerService()
        {
            TcpClientServices = new List<TcpClientService>();
        }

        public void StartLIstening(int port)
        {
            try
            {
                TcpListener tcpListener = new TcpListener(port);
                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();
                    TcpClientService tcpClientService = new TcpClientService(client);
                    Thread thread = new Thread(() => tcpClientService.Process());
                    thread.Start();
                    TcpClientServices.Add(tcpClientService);
                    Console.WriteLine($"Клинет с id: {tcpClientService._clientId} подключился");
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
