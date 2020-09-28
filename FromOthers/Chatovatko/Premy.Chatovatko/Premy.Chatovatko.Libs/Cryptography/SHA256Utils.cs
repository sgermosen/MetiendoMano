using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Premy.Chatovatko.Libs.Cryptography
{
    public static class SHA256Utils
    {
        public static string ComputeSha256Hash(string rawData)
        {
            byte[] bytes = ComputeByteSha256Hash(rawData);
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();

        }

        public static string ComputeSha256Hash(X509Certificate2 cert)
        {
            return ComputeSha256Hash(X509Certificate2Utils.ExportToPem(cert));
        }

        public static byte[] ComputeByteSha256Hash(X509Certificate2 cert)
        {
            return ComputeByteSha256Hash(X509Certificate2Utils.ExportToPem(cert));
        }

        public static byte[] ComputeByteSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                return sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(rawData));
            }
        }

        public static string ComputeCertHash(string certPem)
        {
            return ComputeSha256Hash(X509Certificate2Utils.ImportFromPem(certPem));
        }
        
    }
}
