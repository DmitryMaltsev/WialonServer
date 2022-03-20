using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WialonServer.Models
{
    public class WailonDataModel
    {
        public int PacketLength { get; set; }
        public double ControllerId { get; set; }
        public int CurrentTime { get; set; }
        public int Flags { get; set; }
        public int BlockType { get; set; }
        public int BlockLength { get; set; }
        public byte IsHidden { get; set; }
        public byte BlockDataType { get; set; }
        public string Posinfo { get; set; }
        public string Currency { get; set; }
    }
}
