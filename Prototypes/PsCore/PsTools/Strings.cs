using System.Linq;

namespace PsTools
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Web.Configuration;

    public class Strings
    {
        public static string RandomString(int length)
        {
            //const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            const string chars = "0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RemoveCharacters(string param)
        {
            param = param.Replace("(", "");
            param = param.Replace(")", "");
            param = param.Replace("-", "");
            param = param.Replace(" ", "");
            return param;
        }

        public static string EncodeKrypt(string stringKey)
        {
            string encodeKey = WebConfigurationManager.AppSettings["EncodeKey"];


            byte[] key; //Arreglo donde guardaremos la llave para el cifrado 3DES.

            byte[] array = UTF8Encoding.UTF8.GetBytes(stringKey); //Arreglo donde guardaremos la cadena descifrada.

            // Ciframos utilizando el Algoritmo MD5.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(encodeKey));
            md5.Clear();

            //Ciframos utilizando el Algoritmo 3DES.
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = key;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convertir = tripledes.CreateEncryptor(); // Iniciamos la conversión de la cadena
            byte[] result = convertir.TransformFinalBlock(array, 0, array.Length); //Arreglo de bytes donde guardaremos la cadena cifrada.
            tripledes.Clear();

            return Convert.ToBase64String(result, 0, result.Length); // Convertimos la cadena y la regresamos.
        }

        public static string DecodeKrypt(string stringKey)
        {
            string encodeKey = WebConfigurationManager.AppSettings["EncodeKey"];

            byte[] key;

            byte[] array = Convert.FromBase64String(stringKey); // Arreglo donde guardaremos la cadena descovertida.

            // Ciframos utilizando el Algoritmo MD5.
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(encodeKey));
            md5.Clear();

            //Ciframos utilizando el Algoritmo 3DES.
            TripleDESCryptoServiceProvider tripledes = new TripleDESCryptoServiceProvider();
            tripledes.Key = key;
            tripledes.Mode = CipherMode.ECB;
            tripledes.Padding = PaddingMode.PKCS7;
            ICryptoTransform convert = tripledes.CreateDecryptor();
            byte[] result = convert.TransformFinalBlock(array, 0, array.Length);
            tripledes.Clear();

           // string decryptedKey = UTF8Encoding.UTF8.GetString(result); // Obtenemos la cadena
            return UTF8Encoding.UTF8.GetString(result); // decryptedKey; // Devolvemos la cadena
        }

    }

}
