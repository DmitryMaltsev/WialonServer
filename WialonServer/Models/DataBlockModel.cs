using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WialonServer.Models.Interfaces;

namespace WialonServer.Models
{
    public class DataBlockModel:IDataBlockModel
    {
        //Тип блока
        public int BlockType { get; set; }
        //Размер блока
        public int BlockLength { get; set; }
        //Атрибут скрытости
        public int IsHidden { get; set; }
        //Тип данных блока
        public int BlockDataType { get; set; }

        public string Name { get; set; }
    }
}
