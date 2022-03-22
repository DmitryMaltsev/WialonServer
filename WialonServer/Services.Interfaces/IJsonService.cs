using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WialonServer.Services.Interfaces
{
    public interface IJsonService
    {
        T ReadJs<T>(string filePath, T trainData);
        void WriteJS<T>(string filePath, T settingsModel);
    }
}
