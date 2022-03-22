using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WialonServer.Services.Interfaces
{
    public interface ITcpServerService
    {
        List<ITcpClientservice> ClientsList { get; set; }
        void CloseAllConnections();
        void StartLIstening(int port);
    }
}
