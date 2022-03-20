using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WialonServer.Services
{
    public class TcpClientService
    {
        public string _clientId { get; set; }
        TcpClient _tcpClient { get; set; }
        NetworkStream _networkStream { get; set; }
        public List<byte> recievedBytesList;
        public TcpClientService(TcpClient tcpClient)
        {
            _tcpClient = tcpClient;
            _clientId = Guid.NewGuid().ToString();


        }

        public void Process()
        {
            _networkStream = _tcpClient.GetStream();
            while (true)
            {
                if (RecieveData())
                {
                    Console.WriteLine(recievedBytesList.Count);
                    byte[] buffer = { 1 };
                    //     sendData(buffer);
                }
                else
                {

                    TcpClientService clientService = TcpServerService.TcpClientServices.FirstOrDefault(p => p._clientId == _clientId);
                    if (clientService != null)
                    TcpServerService.TcpClientServices.Remove(clientService);
                    Console.WriteLine($"{_clientId} покинул чат");
                    Disconnect();
                    break;
                }
            }
        }

        public bool RecieveData()
        {
            bool recieved = true;
            bool notRecieved = false;
            recievedBytesList = new List<byte>();
            byte[] recievedData = new byte[100];
            try
            {
                do
                {
                    int length = _networkStream.Read(recievedData, 0, recievedData.Length);
                    byte[] buffer = recievedData.Take(length).ToArray();
                    recievedBytesList.AddRange(buffer);
                }
                while (_networkStream.DataAvailable);
            }
            catch
            {
                return notRecieved;
            }
            return recieved;
        }

        public void sendData(byte[] sendingData)
        {
            _networkStream.Write(sendingData, 0, sendingData.Length);
        }

        public void Disconnect()
        {
            if (_networkStream != null)
                _networkStream.Close();
            if (_tcpClient != null)
                _tcpClient.Close();
        }

    }
}
