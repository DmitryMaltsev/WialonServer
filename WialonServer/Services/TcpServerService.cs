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
        public static List<ClientModel<byte>> TcpClientsList;
        private TcpClientService _tcpClientService { get; set; }
        public TcpServerService()
        {
            TcpClientsList = new List<ClientModel<byte>>();
            _tcpClientService = new TcpClientService();
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
                    _tcpClientService.CreateNewClient(client);
                    TcpClientsList.Add(_tcpClientService.ClientRepository);
                    Console.WriteLine($"Клинет с id: {_tcpClientService.ClientRepository.ClientId} подключился");
                    Thread thread = new Thread(() => _tcpClientService.Process());
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
            if (TcpClientsList != null && TcpClientsList.Count > 0)
            {
                foreach (ClientModel<byte> tcpClient in TcpClientsList)
                {
                    _tcpClientService.Disconnect(tcpClient);
                }
            }
        }
    }
}
