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
            _networkStream = _tcpClient.GetStream();

        }

        public void Process()
        {
            while (true)
            {
                try
                {
                    if (RecieveData())
                    {
                        byte[] buffer = { 1 };
                        sendData(buffer);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Disconnect();
                }
            }
        }

        public bool RecieveData()
        {
            bool recieved = true;
            recievedBytesList = new List<byte>();
            byte[] recievedData = new byte[50];
            do
            {
                int length = _networkStream.Read(recievedData, 0, recievedData.Length);
                byte[] buffer = recievedData.Take(length).ToArray();
                recievedBytesList.AddRange(buffer);
            }
            while (_networkStream.DataAvailable);
            for (int i = 0; i < recievedBytesList.Count; i++)
            {
                Console.Write(recievedBytesList);
            }
            Console.WriteLine();
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
