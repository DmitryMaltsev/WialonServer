using Services;

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Timers;

using WialonServer.Models;
using WialonServer.Services;
using WialonServer.Services.Interfaces;

namespace WialonServer
{
    class Program
    {
        //private static IWialonParsingService _parsingService { get; set; }
        //private static IJsonService _jsonService { get; set; }
       // private static ITcpServerService _serverService { get; set; }
        static void Main(string[] args)
        {
            IWialonParsingService _parsingService = new WialonParsingService();
            IJsonService _jsonService = new JsonService();
            ITcpServerService _serverService = new TcpServerService(_jsonService, _parsingService); 
            Thread threadListen = new Thread(() => _serverService.StartLIstening(8888));
            threadListen.Start();

            //System.Timers.Timer timer = new System.Timers.Timer(200);
            //timer.Elapsed += OnTimedEvent;
            //timer.AutoReset = true;
         //   timer.Enabled = true;

        }

        //private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        //{
        //    foreach (ITcpClientservice client in _serverService.ClientsList)
        //    {
        //        if (client.IsDataRecieved )
        //        {
        //            WialonDataModel wialonDataModel = _parsingService.ParseData(client.ClientModel.RecievedDataList);
        //            _jsonService.WriteJS(ReadWritePath.JsonPath, wialonDataModel);
        //        }
        //    }
        //}
    }
}
