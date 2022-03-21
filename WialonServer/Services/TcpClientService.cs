using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using WialonServer.Models;

namespace WialonServer.Services
{
    public class TcpClientService
    {
        public ClientModel<byte> ClientRepository { get; set; }

        public void CreateNewClient(TcpClient tcpClient)
        {
            ClientRepository = new ClientModel<byte>();
            ClientRepository.ClientTcp = tcpClient;
            ClientRepository.ClientId = Guid.NewGuid().ToString();
            ClientRepository.NetWorkStream = ClientRepository.ClientTcp.GetStream();
        }

        public void Process()
        {
            //   _networkStream = _tcpClient.GetStream();
            while (true)
            {
                if (RecieveData())
                {
                    Console.WriteLine(ClientRepository.RecievedDataList.Count);
                    byte[] buffer = { 1 };
                    sendData(buffer);
                }
                else
                {
                    ClientModel<byte> closingClient = TcpServerService.TcpClientsList.FirstOrDefault(p => p.ClientId == ClientRepository.ClientId);
                    if (closingClient != null)
                        TcpServerService.TcpClientsList.Remove(closingClient);
                    Console.WriteLine($"{closingClient.ClientId} покинул чат");
                    Disconnect(this.ClientRepository);
                    break;
                }
            }
        }

        public bool RecieveData()
        {
            bool recieved = true;
            bool notRecieved = false;
            ClientRepository.RecievedDataList = new List<byte>();
            byte[] recievedData = new byte[100];
            try
            {
                do
                {
                    int length = ClientRepository.NetWorkStream.Read(recievedData, 0, recievedData.Length);
                    byte[] buffer = recievedData.Take(length).ToArray();
                    ClientRepository.RecievedDataList.AddRange(buffer);
                }
                while (ClientRepository.NetWorkStream.DataAvailable);
            }
            catch
            {
                return notRecieved;
            }
            return recieved;
        }

        public void sendData(byte[] sendingData)
        {
            ClientRepository.NetWorkStream.Write(sendingData, 0, sendingData.Length);
        }

        public void Disconnect(ClientModel<byte> clientRepository)
        {
            if (clientRepository.NetWorkStream != null)
                clientRepository.NetWorkStream.Close();
            if (clientRepository.ClientTcp != null)
                clientRepository.ClientTcp.Close();
        }

    }
}
