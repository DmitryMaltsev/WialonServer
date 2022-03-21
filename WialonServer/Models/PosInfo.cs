using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WialonServer.Models.Interfaces;

namespace WialonServer.Models
{
    public class PosInfoModel : IDataBlockModel
    {
        public int BlockType { get; set; }

        public int BlockLength { get; set; }

        public int IsHidden { get; set; }

        public int BlockDataType { get; set; }

        public string Name { get; set; }
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
