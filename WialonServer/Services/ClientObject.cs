using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WialonServer.Services
{
    class ClientObject
    {
        public string _id = String.Empty;
        public NetworkStream _stream;
        string _userName;
        public TcpClient _client;
        ServerObject _serverObject;

        public ClientObject(TcpClient tcpClient, ServerObject serverObject)
        {
            _id = Guid.NewGuid().ToString();
            _client = tcpClient;
            _serverObject = serverObject;
        }

        public void Process()
        {
            _stream = _client.GetStream();
            _userName = GetMessage();
            string message = $"{_userName} вошел в чат";
            _serverObject.BroadcastMessage(message, _id);
            Console.WriteLine(message);
            while (true)
            {
                try
                {
                    message = GetMessage();
                    message = String.Format($"{_userName}{message}");
                    Console.WriteLine(message);
                    _serverObject.BroadcastMessage(message, this._id);
                }
                catch(Exception ex)
                {
                //    Console.WriteLine(ex.Message);
                    message = $"{_userName} покинул чат";
                    Console.WriteLine(message);
                    _serverObject.clientObjectList.Remove(this);
                    _serverObject.BroadcastMessage(message, this._id);
                    //Console.WriteLine(ex.Message);
                    break;
                }
            }

        }

        private string GetMessage()
        {
            StringBuilder sb = new StringBuilder();
            byte[] buffer = new byte[100];

            do
            {
                int result = _stream.Read(buffer, 0, buffer.Length);
                sb.Append(Encoding.Unicode.GetString(buffer, 0, result));
            } while (_stream.DataAvailable);
            return sb.ToString();
        }
    }
}
