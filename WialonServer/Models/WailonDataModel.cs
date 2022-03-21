using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WialonServer.Models.Interfaces;

namespace WialonServer.Models
{
    public class WialonDataModel
    {
        //Размер пакета 
        public int PacketLength { get; set; }
        // Идентификатор контроллера
        public double ControllerId { get; set; }
        //Время в секундах(UTC) 
        public int CurrentTime { get; set; }
        //Битовые флаги сообщения. 
        public int Flags { get; set; }
        //блок с данными
        public List<IDataBlockModel> DataBlockModelList { get; set; } = new List<IDataBlockModel>();

    }
}
