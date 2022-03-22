using System.IO;
using System.Xml.Serialization;

using Microsoft.Win32;

using Newtonsoft.Json;

using WialonServer.Services.Interfaces;

namespace Services
{
    public class JsonService : IJsonService
    {


        /// <summary>
        /// Сериализует объект в json-файл
        /// </summary>
        /// <typeparam name="T">Тип объекта для сериализации</typeparam>
        /// <param name="filePath">Куда сохранить</param>
        /// <param name="settingsModel">Объект для сериализации</param>
        public void WriteJS<T>(string filePath, T settingsModel)
        {
            if (string.IsNullOrEmpty(filePath)) return;
            // if (!File.Exists(filePath)) File.Create(filePath);
            string js = JsonConvert.SerializeObject(settingsModel, Formatting.Indented);
            using (StreamWriter stream = File.AppendText(filePath))
            {
                stream.Write(js);
            }
        }

        /// <summary>
        /// Десериализует объект из json файла
        /// </summary>
        /// <typeparam name="T">Тип объекта для сериализации</typeparam>
        /// <param name="filePath">Куда сохранить</param>
        /// <param name="settingsModel">Объект для сериализации</param>
        public T ReadJs<T>(string filePath, T trainData)
        {
            if (File.Exists(filePath))
            {
                using (StreamReader fs = new StreamReader(filePath))
                {
                    string fileString = fs.ReadToEnd();
                    trainData = JsonConvert.DeserializeObject<T>(fileString);
                }
            }
            return trainData;
        }



    }
}
