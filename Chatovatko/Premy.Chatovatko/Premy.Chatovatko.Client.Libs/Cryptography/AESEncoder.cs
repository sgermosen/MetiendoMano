using Premy.Chatovatko.Libs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Cryptography
{
    public static class AESEncoder
    {
        private static byte[] GetSalt()
        {
            return LUtils.GenerateRandomBytes(AESConstants.SALT_LENGHT);
        }

        public static byte[] Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;
            byte[] saltBytes = GetSalt();

            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(saltBytes, 0, saltBytes.Length);

                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = AESConstants.KEYSIZE;
                    AES.BlockSize = AESConstants.BLOCKSIZE;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AESConstants.KEYSIZE / 8);
                    AES.IV = key.GetBytes(AESConstants.BLOCKSIZE / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }

        public static byte[] Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;
            byte[] saltBytes = new byte[AESConstants.SALT_LENGHT];

            Array.Copy(bytesToBeDecrypted, saltBytes, AESConstants.SALT_LENGHT);

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = AESConstants.KEYSIZE;
                    AES.BlockSize = AESConstants.BLOCKSIZE;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AESConstants.KEYSIZE / 8);
                    AES.IV = key.GetBytes(AESConstants.BLOCKSIZE / 8);

                    AES.Mode = CipherMode.CBC;

                    using (var cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, AESConstants.SALT_LENGHT, bytesToBeDecrypted.Length - AESConstants.SALT_LENGHT);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

    }
}
