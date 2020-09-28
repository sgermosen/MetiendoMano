using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

namespace Premy.Chatovatko.Libs
{
    public static class LUtils
    {
        public static string GetIpAddress(TcpClient client)
        {
            return ((IPEndPoint)(client.Client.RemoteEndPoint)).Address.ToString();
        }

        public static byte[] GetBytes(String text)
        {
            return Encoding.UTF8.GetBytes(text);
        }

        public static String GetText(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }

        public static void WriteToFile(byte[] bytes, string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Create))
            {
                fileStream.Write(bytes, 0, bytes.Length);
            }
        }

        public static byte[] ReadFromFile(string fileName)
        {
            using (FileStream fileStream = new FileStream(fileName, FileMode.Open))
            {
                byte[] ret = new byte[fileStream.Length];
                fileStream.Read(ret, 0, (int)fileStream.Length);
                return ret;
            }
        }
        
        public static byte[] GenerateRandomBytes(int lenght)
        {
            byte[] ret = new byte[lenght];
            RNGCryptoServiceProvider.Create().GetBytes(ret);
            return ret;
        }
    }
}
