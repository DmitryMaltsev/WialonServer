using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WialonServer.Models;

namespace WialonServer.Services.Interfaces
{
    public interface IWialonParsingService
    {
        WialonDataModel ParseData(List<byte> data);
    }
}
