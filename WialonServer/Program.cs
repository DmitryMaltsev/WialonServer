using Services;

using System;
using System.IO;
using System.Threading;
using System.Timers;

using WialonServer.Models;
using WialonServer.Services;

namespace WialonServer
{
    class Program
    {
        private static WialonParsingService parsingService {get;set;}
        static void Main(string[] args)
        {

            TcpServerService serverService = new TcpServerService();
            parsingService = new WialonParsingService();
            Thread threadListen = new Thread(() => serverService.StartLIstening(8888));
            threadListen.Start();
            System.Timers.Timer timer = new System.Timers.Timer(200);
            timer.Elapsed += OnTimedEvent;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        /// <summary>
        /// Событие таймера для получения данных от клиента
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void OnTimedEvent(object sender, ElapsedEventArgs e)
        {
            if (TcpServerService.ClientsList.Count > 0)
            {
                for (int i = 0; i < TcpServerService.ClientsList.Count; i++)
                {
                    if (TcpServerService.ClientsList[i].DataRecieved && TcpServerService.ClientsList[i].RecievedDataList.Count > 0)
                    {
                        WialonDataModel wialonDataModel = parsingService.ParseData(TcpServerService.ClientsList[i].RecievedDataList);
                        JsonService jsonService = new JsonService();
                        jsonService.WriteJS(ReadWritePath.JsonPath, wialonDataModel);
                        TcpServerService.ClientsList[i].DataRecieved = false;
                    }
                }
            }
        }
    }
}
