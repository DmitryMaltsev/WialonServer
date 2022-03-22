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
        private static ITcpServerService serverService;
        private static WialonParsingService parsingService { get; set; }
        static void Main(string[] args)
        {

            ITcpServerService serverService = new TcpServerService();
            parsingService = new WialonParsingService();
            Thread threadListen = new Thread(() => serverService.StartLIstening(8888));
            threadListen.Start();     
        }


        public static void ClientDatRecievedCallBack(object sender, List<byte> recievedBytes)
        {
            if (recievedBytes.Count > 0)
            {
                WialonDataModel wialonDataModel = parsingService.ParseData(recievedBytes);
                JsonService jsonService = new JsonService();
                jsonService.WriteJS(ReadWritePath.JsonPath, wialonDataModel);
            }
        }

    }
}
