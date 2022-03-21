using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WialonServer.Models.Interfaces;

namespace WialonServer.Models
{
    public class PosInfoModel : DataBlockModel
    {
        //Долгота
        public double Lon { get; set; }
        //Широта    
        public double Lat { get; set; }
        //Высота
        public double Height { get; set; }
        //Скорость
        public int Speed { get; set; }
        //Курс
        public int Route { get; set; }
        //Количество спутников
        public int SputniksCount { get; set; }
    }
}
