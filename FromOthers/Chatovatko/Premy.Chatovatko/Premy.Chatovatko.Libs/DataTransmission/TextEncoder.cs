using Newtonsoft.Json;
using System;
using System.IO;

namespace Premy.Chatovatko.Libs.DataTransmission
{
    public static class TextEncoder
    {
        
        public static String ReadString(Stream stream)
        {
            return LUtils.GetText(BinaryEncoder.ReceiveBytes(stream));
        }

        public static void SendString(Stream stream, String message)
        {
            byte[] byteStr = LUtils.GetBytes(message);
            BinaryEncoder.SendBytes(stream, byteStr);
        }


        public static void SendJson(Stream stream, object obj)
        {
            String json = JsonConvert.SerializeObject(obj);
            SendString(stream, json);
        }


        public static T ReadJson<T>(Stream stream)
        {
            String json = ReadString(stream);
            return JsonConvert.DeserializeObject<T>(json);
        }


    }
}
