using Premy.Chatovatko.Client.Libs.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Newtonsoft.Json;
using Premy.Chatovatko.Client.Libs.Cryptography;
using Premy.Chatovatko.Libs.Logging;
using Premy.Chatovatko.Libs.DataTransmission;

namespace Premy.Chatovatko.Client.Libs.Database.JsonModels
{
    public static class JsonEncoder
    {
        public static JsonCapsula GetJsonDecoded(Context context, byte[] message, long senderId)
        {
            byte[] aesBinKey = context.Contacts
                .Where(u => u.PublicId == senderId)
                .Select(u => u.ReceiveAesKey)
                .SingleOrDefault();

            AESPassword key = new AESPassword(aesBinKey);
            byte[] decrypted = key.Decrypt(message);

            MemoryStream stream = new MemoryStream(decrypted);
            JsonTypes type = (JsonTypes)BinaryEncoder.ReadInt(stream);
            string jsonText = TextEncoder.ReadString(stream);

            byte[] attechment = null;
            int isAttechment = BinaryEncoder.ReadInt(stream);

            if(isAttechment == 1)
            {
                attechment = BinaryEncoder.ReceiveBytes(stream);
            }
            
            IJType jmessage;
            switch (type)
            {
                case JsonTypes.ALARM:
                    jmessage = JsonConvert.DeserializeObject<JAlarm>(jsonText);
                    break;
                case JsonTypes.CONTACT:
                    jmessage = JsonConvert.DeserializeObject<JContact>(jsonText);
                    break;
                case JsonTypes.MESSAGES:
                    jmessage = JsonConvert.DeserializeObject<JMessage>(jsonText);
                    break;
                case JsonTypes.MESSAGES_THREAD:
                    jmessage = JsonConvert.DeserializeObject<JMessageThread>(jsonText);
                    break;
                default:
                    throw new Exception("Unknown JsonType.");
            }

            return new JsonCapsula()
            {
                Attechment = attechment,
                Message = jmessage
            };

        }

        public static byte[] GetJsonEncoded(Context context, IJType message, long receiverId, byte[] attechment = null)
        {
            byte[] aesBinKey = context.Contacts
                .Where(u => u.PublicId == receiverId)
                .Select(u => u.SendAesKey)
                .SingleOrDefault();

            AESPassword key = new AESPassword(aesBinKey);

            MemoryStream stream = new MemoryStream();
            BinaryEncoder.SendInt(stream, (int)message.GetJsonType());

            string json = JsonConvert.SerializeObject(message);
            TextEncoder.SendString(stream, json);

            if(attechment == null)
            {
                BinaryEncoder.SendInt(stream, 0);
            }
            else
            {
                BinaryEncoder.SendInt(stream, 1);
                BinaryEncoder.SendBytes(stream, attechment);
            }

            byte[] notEncrypted = stream.ToArray();
            return key.Encrypt(notEncrypted);
        }

        public static byte[] GetJsonEncoded(Context context, JsonCapsula capsula, long receiverId)
        {
            return GetJsonEncoded(context, capsula.Message, receiverId, capsula.Attechment);
        }
    }
}
