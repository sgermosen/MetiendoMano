using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Org.BouncyCastle.Asn1;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.Utilities;
using Org.BouncyCastle.X509;
using Premy.Chatovatko.Libs.Logging;

namespace Premy.Chatovatko.Client.Cryptography
{
    public static class X509Certificate2Generator
    {
        
        public static X509Certificate2 GenerateCACertificate(Logger logger, string subjectName = "CN=root ca", int keyStrength = 4096)
        {
            logger.Log("X509Certificate2Generator", "X509 certificate generator", "--Making new X509 certificate.--", false);
            logger.Log("X509Certificate2Generator", "X509 certificate generator", "Generating random numbers.", false);
            var randomGenerator = new CryptoApiRandomGenerator();
            var random = new SecureRandom(randomGenerator);

            // The Certificate Generator
            var certificateGenerator = new X509V3CertificateGenerator();

            // Serial Number
            var serialNumber = BigIntegers.CreateRandomInRange(BigInteger.One, BigInteger.ValueOf(Int64.MaxValue), random);
            certificateGenerator.SetSerialNumber(serialNumber);

            // Signature Algorithm
            const string signatureAlgorithm = "SHA256WithRSA";
            certificateGenerator.SetSignatureAlgorithm(signatureAlgorithm);

            // Issuer and Subject Name
            var subjectDN = new X509Name(subjectName);
            var issuerDN = subjectDN;
            certificateGenerator.SetIssuerDN(issuerDN);
            certificateGenerator.SetSubjectDN(subjectDN);

            // Valid For
            var notBefore = DateTime.UtcNow.Date;
            var notAfter = notBefore.AddYears(20);

            certificateGenerator.SetNotBefore(notBefore);
            certificateGenerator.SetNotAfter(notAfter);

            
            logger.Log("X509Certificate2Generator", "X509 certificate generator", "Making RSA key pair.", false);
            AsymmetricCipherKeyPair subjectKeyPair;
            var keyGenerationParameters = new KeyGenerationParameters(random, keyStrength);
            var keyPairGenerator = new RsaKeyPairGenerator();
            keyPairGenerator.Init(keyGenerationParameters);
            subjectKeyPair = keyPairGenerator.GenerateKeyPair();
            // Subject Public Key
            certificateGenerator.SetPublicKey(subjectKeyPair.Public);

            // Generating the Certificate
            var issuerKeyPair = subjectKeyPair;
            logger.Log("X509Certificate2Generator", "X509 certificate generator", "Generating the X509 certificate without private key.", false);
            // selfsign certificate
            var certificate = certificateGenerator.Generate(issuerKeyPair.Private, random);

            logger.Log("X509Certificate2Generator", "X509 certificate generator", "Merging the X509 certificate.", false);
            // in-memory PFX stream
            var pkcs12Store = new Pkcs12Store();
            var certEntry = new X509CertificateEntry(certificate);
            pkcs12Store.SetCertificateEntry(subjectName, certEntry);
            pkcs12Store.SetKeyEntry(subjectName, new AsymmetricKeyEntry(subjectKeyPair.Private), new[] { certEntry });
            X509Certificate2 keyedCert;
            using (MemoryStream pfxStream = new MemoryStream())
            {
                pkcs12Store.Save(pfxStream, new char[0], new SecureRandom());
                pfxStream.Seek(0, SeekOrigin.Begin);
                keyedCert = new X509Certificate2(pfxStream.ToArray(), string.Empty, X509KeyStorageFlags.Exportable);
            }

            return keyedCert;

        }

    }
}
