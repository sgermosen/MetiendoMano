using Premy.Chatovatko.Libs;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Cryptography
{
    public class AESPassword
    {
        private readonly byte[] password;
        
        public AESPassword(byte[] password)
        {
            this.password = password ?? throw new Exception("Password can't be null.");
        }

        public byte[] Password => password;

        public static AESPassword GenerateAESPassword()
        {
            return new AESPassword(LUtils.GenerateRandomBytes(AESConstants.PASSWORD_LENGHT));
        }

        public byte[] Encrypt (byte[] data)
        {
            return AESEncoder.Encrypt(data, password);
        }

        public byte[] Decrypt (byte[] data)
        {
            return AESEncoder.Decrypt(data, password);
        }


    }
}
