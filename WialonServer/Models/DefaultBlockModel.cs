using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WialonServer.Models
{
    /// <summary>
    /// Значения остальных блоков. Тип зависит от Значения BlockType
    /// </summary>
    class DefaultBlockModel : DataBlockModel
    {
        public object Value { get; set; }
    }
}
