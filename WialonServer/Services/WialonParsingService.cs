using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WialonServer.Models;

namespace WialonServer.Services
{
    class WialonParsingService
    {

        List<byte> data = new List<byte>
        {0x74,0x00,0x00,0x00,0x33,0x35,0x33,0x39,0x37,0x36,0x30,0x31,0x33,0x34,0x34,0x35,0x34,0x38,0x35,0x00,0x4B,0x0B, 0xFB,0x70,0x00,0x00,0x00,0x03,0x0B,
            0xBB,0x00,0x00,0x00,0x27,0x01,0x02,0x70,0x6F,0x73,0x69,0x6E,0x66,0x6F ,0x00,0xA0,0x27,0xAF,0xDF,0x5D,0x98,0x48,0x40,0x3A,0xC7,0x25,0x33,0x83,
            0xDD,0x4B,0x40,0x00,0x00,0x00,0x00,0x00,0x80,0x5A,0x40,0x00,0x36,0x01,0x46,0x0B,0x0B,0xBB,0x00,0x00,0x00,0x12,0x00,0x04,0x70,
            77,0x72,0x5F,0x65,0x78,0x74,0x00,0x2B,0x87,0x16,0xD9,0xCE,0x97,0x3B,0x40,0x0B,0xBB,0x00,0x00,0x00,0x11,0x01,0x03,0x61,0x76,0x6C,
            0x5F,0x69,0x6E,0x70,0x75,0x74,0x73,0x00,0x00,0x00,0x00,0x01};


        public WialonDataModel ParseData(List<byte> data)
        {
            WialonDataModel wialonDataModel = new();
            byte[] baseArray = data.ToArray();
            int startIndex = 0;

            byte[] bufferArray = new byte[4];
            Array.Copy(baseArray, 0, bufferArray, 0, 4);
            wialonDataModel.PacketLength = (int)ConvertByteArrayToValue(bufferArray);
            startIndex += 4;

            bufferArray = FindArrayByZeroEnd(baseArray, startIndex);
            wialonDataModel.ControllerId = ConvertByteArrayToValue(bufferArray);
            startIndex += bufferArray.Length;

            bufferArray = new byte[4];
            Array.Copy(baseArray, startIndex, bufferArray, 0, 4);
            wialonDataModel.CurrentTime = (int)ConvertByteArrayToValue(bufferArray);
            startIndex += 4;

            bufferArray = new byte[4];
            Array.Copy(baseArray, startIndex, bufferArray, 0, 4);
            wialonDataModel.Flags = (int)ConvertByteArrayToValue(bufferArray);
            startIndex += 4;

            PosInfoModel posInfo = new();
            DataBlockModel dataBlockModel = new();

            (dataBlockModel, startIndex) = CreateDataBlockModel(baseArray, startIndex);

            posInfo.BlockType = dataBlockModel.BlockDataType;

            posInfo.BlockLength = dataBlockModel.BlockLength;

            posInfo.IsHidden = dataBlockModel.IsHidden;

            posInfo.BlockDataType = dataBlockModel.BlockDataType;

            posInfo.Name = dataBlockModel.Name;

            bufferArray = new byte[8];
            Array.Copy(baseArray, startIndex, bufferArray, 0, 8);
            posInfo.Lon = ConvertByteArrayToValue(bufferArray);
            startIndex += 8;

            bufferArray = new byte[8];
            Array.Copy(baseArray, startIndex, bufferArray, 0, 8);
            posInfo.Lat = ConvertByteArrayToValue(bufferArray);
            startIndex += 8;

            bufferArray = new byte[8];
            Array.Copy(baseArray, startIndex, bufferArray, 0, 8);
            posInfo.Height = ConvertByteArrayToValue(bufferArray);
            startIndex += 8;


            bufferArray = new byte[2];
            Array.Copy(baseArray, startIndex, bufferArray, 0, 2);
            posInfo.Speed = (int)ConvertByteArrayToValue(bufferArray);
            startIndex += 2;

            bufferArray = new byte[2];
            Array.Copy(baseArray, startIndex, bufferArray, 0, 2);
            posInfo.Route = (int)ConvertByteArrayToValue(bufferArray);
            startIndex += 2;

            bufferArray = new byte[1];
            Array.Copy(baseArray, startIndex, bufferArray, 0, 1);
            posInfo.SputniksCount = (int)ConvertByteArrayToValue(bufferArray);
            startIndex += 1;
            wialonDataModel.DataBlockModelList.Add(new PosInfoModel());

            while (startIndex < baseArray.Length)
            {
                DefaultBlockModel defaultBlockModel = new();
                (defaultBlockModel, startIndex) = CreateDefaultBlockModel(baseArray, startIndex);
                wialonDataModel.DataBlockModelList.Add(defaultBlockModel);
            }

            return wialonDataModel;
        }

        private (DefaultBlockModel, int) CreateDefaultBlockModel(byte[] baseArray, int startIndex)
        {
            DefaultBlockModel defaultBlockModel = new();
            DataBlockModel dataBlockModel;

            (dataBlockModel, startIndex) = CreateDataBlockModel(baseArray, startIndex);
            defaultBlockModel.BlockType = dataBlockModel.BlockDataType;
            defaultBlockModel.BlockLength = dataBlockModel.BlockLength;
            defaultBlockModel.IsHidden = dataBlockModel.IsHidden;
            defaultBlockModel.BlockDataType = dataBlockModel.BlockDataType;
            defaultBlockModel.Name = dataBlockModel.Name;

            switch (defaultBlockModel.BlockDataType)
            {
                case 1:
                    {
                        //текстовое сообщение
                        byte[] bufferArray = new byte[4];
                        Array.Copy(baseArray, startIndex, bufferArray, 0, 4);
                        string value = BitConverter.ToString(bufferArray).Replace("-", "");
                        defaultBlockModel.Value = (double)ConvertByteArrayToValue(bufferArray);
                        startIndex += 4;
                    }
                    break;
                case 2:
                    {
                        //Не ясен случай
                    }
                    break;
                case 3:
                    {
                        //целое 4 байт
                        byte[] bufferArray = new byte[4];
                        Array.Copy(baseArray, startIndex, bufferArray, 0, 4);
                        string value = BitConverter.ToString(bufferArray).Replace("-", "");
                        defaultBlockModel.Value = (int)ConvertByteArrayToValue(bufferArray);
                        startIndex += 4;
                    }
                    break;
                case 4:
                    {
                        //double
                        byte[] bufferArray = new byte[8];
                        Array.Copy(baseArray, startIndex, bufferArray, 0, 8);
                        defaultBlockModel.Value = ConvertByteArrayToValue(bufferArray);
                        startIndex += 8;
                    }
                    break;
                case 5:
                    {
                        //long 8 байт
                        byte[] bufferArray = new byte[8];
                        Array.Copy(baseArray, startIndex, bufferArray, 0, 8);
                        string value = BitConverter.ToString(bufferArray).Replace("-", "");
                        defaultBlockModel.Value = (long)ConvertByteArrayToValue(bufferArray);
                        startIndex += 8;
                    }
                    break;
            }
            return (defaultBlockModel, startIndex);
        }


        private (DataBlockModel, int) CreateDataBlockModel(byte[] baseArray, int startIndex)
        {
            DataBlockModel dataBlockModel = new DataBlockModel();
            byte[] bufferArray = new byte[2];
            Array.Copy(baseArray, startIndex, bufferArray, 0, 2);
            dataBlockModel.BlockType = (int)ConvertByteArrayToValue(bufferArray);
            startIndex += 2;

            bufferArray = new byte[4];
            Array.Copy(baseArray, startIndex, bufferArray, 0, 4);
            dataBlockModel.BlockLength = (int)ConvertByteArrayToValue(bufferArray);
            startIndex += 4;

            bufferArray = new byte[1];
            Array.Copy(baseArray, startIndex, bufferArray, 0, 1);
            dataBlockModel.IsHidden = (int)ConvertByteArrayToValue(bufferArray);
            startIndex += 1;

            bufferArray = new byte[1];
            Array.Copy(baseArray, startIndex, bufferArray, 0, 1);
            dataBlockModel.BlockDataType = (int)ConvertByteArrayToValue(bufferArray);
            startIndex += 1;

            bufferArray = FindArrayByZeroEnd(baseArray, startIndex);
            dataBlockModel.Name += BitConverter.ToString(bufferArray).Replace("-", "");
            startIndex += bufferArray.Length;
            return (dataBlockModel, startIndex);
        }

        private double ConvertByteArrayToValue(byte[] bufferArray)
        {
            int bytePlace = bufferArray.Length - 1;
            double result = 0;
            for (int i = 0; i < bufferArray.Length; i++, bytePlace--)
            {
                //  double val = (bufferArray[i] * Math.Pow(256, bytePlace));
                result += bufferArray[i] * Math.Pow(256, bytePlace);
            }
            return result;
        }

        private double ConvertByteArrayToFraction(byte[] bufferArray)
        {
            double resultDouble = 0;

            return resultDouble;
        }

        private byte[] FindArrayByZeroEnd(byte[] baseArray, int startIndex)
        {
            List<byte> bufferByteList = new List<byte>();
            for (int i = startIndex; i < baseArray.Length; i++)
            {
                if (baseArray[i] != 0)
                {
                    bufferByteList.Add(baseArray[i]);
                }
                else
                {
                    bufferByteList.Add(baseArray[i]);
                    startIndex = i + 1;
                    break;
                }
            }
            return bufferByteList.ToArray();
        }

    }
}
