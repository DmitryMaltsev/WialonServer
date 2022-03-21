using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using WialonServer.Models;

namespace WialonServer.Services
{
    class TcpServerService
    {
        public static List<ClientModel> ClientsList { get; set; }

        public TcpServerService()
        {
            ClientsList = new List<ClientModel>();
        }

        public void StartLIstening(int port)
        {
            TcpClientService clientService = new();
            try
            {
                TcpListener listener = new TcpListener(IPAddress.Any, 8888);
                listener.Start();
                Console.WriteLine("Сервер начал свою работу");
                while (true)
                {
                    TcpClient client = listener.AcceptTcpClient();
                    ClientModel newClient = clientService.CreateNewClient(client);
                    ClientsList.Add(newClient);
                    Console.WriteLine($"Клинет с id: {newClient.ClientId} подключился");
                    Thread thread = new Thread(() => clientService.Process(newClient));
                    thread.Start();
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseAllConnections(clientService);
            }
        }

        public void CloseAllConnections(TcpClientService ClientService)
        {
            if (TcpServerService.ClientsList != null && TcpServerService.ClientsList.Count > 0)
            {
                for (int i = 0; i < TcpServerService.ClientsList.Count; i++)
                {
                    ClientService.Disconnect(TcpServerService.ClientsList[i]);
                }
            }
        }
    }
}
