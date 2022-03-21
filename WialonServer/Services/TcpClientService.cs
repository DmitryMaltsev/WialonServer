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

        public ClientModel CreateNewClient(TcpClient tcpClient)
        {
            ClientModel client = new ClientModel();
            client.ClientTcp = tcpClient;
            client.ClientId = Guid.NewGuid().ToString();
            client.NetWorkStream = client.ClientTcp.GetStream();
            client.RecievedDataList = new List<byte>();
            client.DataRecieved = false;
            return client;
        }

        public void Process(ClientModel client)
        {
            while (true)
            {
                if (RecieveData(client))
                {
                    Console.WriteLine(client.RecievedDataList.Count);
                    byte[] buffer = { 0x11 };
                    SendData(client, buffer);
                    client.DataRecieved = true;
                }
                else
                {
                    ClientModel closingClient = TcpServerService.ClientsList.FirstOrDefault(p => p.ClientId == client.ClientId);
                    if (closingClient != null)
                        TcpServerService.ClientsList.Remove(closingClient);
                    Console.WriteLine($"{closingClient.ClientId} покинул чат");
                    Disconnect(client);
                    break;
                }
            }
        }

        public bool RecieveData(ClientModel client)
        {
            bool recieved = true;
            bool notRecieved = false;
            client.RecievedDataList = new List<byte>();
            byte[] recievedData = new byte[100];
            try
            {
                do
                {
                    int length = client.NetWorkStream.Read(recievedData, 0, recievedData.Length);
                    byte[] buffer = recievedData.Take(length).ToArray();
                    client.RecievedDataList.AddRange(buffer);
                }
                while (client.NetWorkStream.DataAvailable);
            }
            catch
            {
                return notRecieved;
            }
            return recieved;
        }

        public void SendData(ClientModel client ,byte[] sendingData)
        {
            client.NetWorkStream.Write(sendingData, 0, sendingData.Length);
        }

        public void Disconnect(ClientModel clientRepository)
        {
            if (clientRepository.NetWorkStream != null)
                clientRepository.NetWorkStream.Close();
            if (clientRepository.ClientTcp != null)
                clientRepository.ClientTcp.Close();
        }

    }
}
