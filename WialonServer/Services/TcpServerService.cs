using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using WialonServer.Models;
using WialonServer.Services.Interfaces;

namespace WialonServer.Services
{

    public class TcpServerService : ITcpServerService
    {
        public List<ITcpClientservice> ClientsList { get; set; }
        private IJsonService _jsonService { get; set; }
        private IWialonParsingService _wialonParsingService { get; set; }

        public TcpServerService()
        {
            ClientsList = new List<ITcpClientservice>();
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
                    ClientModel clientModel = new();
                    TcpClientService clientService = new(clientModel,client);
              //      clientService.ClientClosingEvent += ClientClosingCallBack;
              //      clientService.DataRecievedEvent += ClientDatRecievedCallBack;    
                    Console.WriteLine($"Клинет с id: {clientService.ClientModel.ClientId} подключился");
                    clientService.Process();
                    ClientsList.Add(clientService);              
                    Thread thread = new Thread(() => clientService.Process());
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

        private void ClientClosingCallBack(object sender, string clietnId)
        {
            ITcpClientservice closingClient = ClientsList.FirstOrDefault(p => p.ClientModel.ClientId == clietnId);
            if (closingClient != null)
            {
                ClientsList.Remove(closingClient);
            }
        }

        public  void ClientDatRecievedCallBack(object sender, List<byte> recievedBytes)
        {
       
        }

        public void CloseAllConnections()
        {
            if (ClientsList != null && ClientsList.Count > 0)
            {
                for (int i = 0; i < ClientsList.Count; i++)
                {
                    ClientsList[i].Disconnect();
                }
            }
        }
    }
}
