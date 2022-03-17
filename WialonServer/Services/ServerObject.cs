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
    class ServerObject
    {
        TcpListener listener;
        List<ClientObject> clientObjectList;
        public void Listen()
        {
            try
            {
                listener = new TcpListener(IPAddress.Any, 8888);
                listener.Start();
                Console.WriteLine("Сервер запущен");
                while (true)
                {
                    TcpClient tcpClient = listener.AcceptTcpClient();
                    ClientObject clientObject = new ClientObject(tcpClient, this);
                    Thread thread = new Thread(() => clientObject.Process());
                    thread.Start();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Disconnect();
            }
        }


        public void BroadcastMessage(string message, string id)
        {
            byte[] data = Encoding.Unicode.GetBytes(message);
            for (int i = 0; i < clientObjectList.Count; i++)
            {
                if (clientObjectList[i]._id != id)
                {
                    clientObjectList[i]._stream.Write(data, 0, data.Length);
                }
            }
        }

        private void AddConnection(ClientObject clientObject)
        {
            clientObjectList.Add(clientObject);
        }

        private void RemoveConnection(string id)
        {
            ClientObject clientObject = clientObjectList.FirstOrDefault(p => p._id == id);
            if (clientObject != null)
            {
                clientObjectList.Remove(clientObject);
            }
        }


        public void Disconnect()
        {
            listener.Stop();
            for (int i = 0; i < clientObjectList.Count; i++)
            {
                clientObjectList[i]._client.Close();
            }
        }
    }
}
