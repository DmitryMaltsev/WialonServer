using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

using WialonServer.Models;
using WialonServer.Models.Interfaces;
using WialonServer.Services.Interfaces;

namespace WialonServer.Services
{
    public class TcpClientService : ITcpClientservice
    {
        public IClientModel ClientModel { get; set; }
        public bool IsDataRecieved { get; set; }
        public event EventHandler<List<byte>> DataRecievedEvent;
        public event EventHandler<string> ClientClosingEvent;
       
        public TcpClientService(IClientModel clientModel,TcpClient tcpClient)
        {
            ClientModel = clientModel;         
            ClientModel.ClientTcp = tcpClient;
            ClientModel.ClientId = Guid.NewGuid().ToString();
            ClientModel.NetWorkStream = tcpClient.GetStream();
            ClientModel.RecievedDataList = new List<byte>();
            IsDataRecieved = false;
        }

        public void Process()
        {
            while (true)
            {
                if (RecieveData())
                {
                    byte[] buffer = { 0x11 };
                    SendData(buffer);
                }
                else
                {
                    //ClientModel closingClient = TcpServerService.ClientsList.FirstOrDefault(p => p.ClientId == client.ClientId);
                    //if (closingClient != null)
                    //    TcpServerService.ClientsList.Remove(closingClient);
                    ClientClosingEvent?.Invoke(this, ClientModel.ClientId);
                    Console.WriteLine($"{ ClientModel.ClientId} покинул чат");
                    Disconnect();
                    break;
                }
            }
        }

        public bool RecieveData()
        {
            IsDataRecieved = false;
            ClientModel.RecievedDataList = new List<byte>();
            byte[] recievedData = new byte[100];
            try
            {
                do
                {
                    int length = ClientModel.NetWorkStream.Read(recievedData, 0, recievedData.Length);
                    byte[] buffer = recievedData.Take(length).ToArray();
                    ClientModel.RecievedDataList.AddRange(buffer);
                }
                while (ClientModel.NetWorkStream.DataAvailable);
                Console.WriteLine(ClientModel.RecievedDataList.Count);
             //   DataRecievedEvent?.Invoke(this, RecievedDataList);
                IsDataRecieved = true;
                return true;
            }
            catch
            {
                IsDataRecieved = false;
                return false;
            }
        }

        public void SendData(byte[] sendingData)
        {
   //         ClientModel.NetWorkStream.Write(sendingData, 0, sendingData.Length);
        }

        public void Disconnect()
        {
            if (ClientModel.NetWorkStream != null)
                ClientModel.NetWorkStream.Close();
            if (ClientModel.ClientTcp != null)
                ClientModel.ClientTcp.Close();
        }
    }
}
