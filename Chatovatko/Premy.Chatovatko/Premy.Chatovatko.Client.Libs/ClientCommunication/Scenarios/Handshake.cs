using Microsoft.Extensions.Logging;
using Premy.Chatovatko.Client.Libs.Cryptography;
using Premy.Chatovatko.Libs;
using Premy.Chatovatko.Libs.Cryptography;
using Premy.Chatovatko.Libs.DataTransmission;
using Premy.Chatovatko.Libs.DataTransmission.JsonModels.Handshake;
using Premy.Chatovatko.Libs.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.ClientCommunication.Scenarios
{

    public class Handshake
    {
        public static HandshakeReturnCapsula Login(Logger logger, Stream stream, X509Certificate2 cert, string password, string userName = null, int? clientId = null)
        {
            ClientHandshake clientHandshake = new ClientHandshake()
            {
                PemCertificate = X509Certificate2Utils.ExportToPem(cert),
                UserName = userName,
                ClientId = clientId,
                ServerPassword = password
            };

            if(userName != null && userName.Length > DataConstants.USER_NAME_MAX_LENGHT)
            {
                throw new Exception("Username is too long.");
            }

            TextEncoder.SendJson(stream, clientHandshake);

            byte[] encrypted = BinaryEncoder.ReceiveBytes(stream);
            byte[] decrypted = RSAEncoder.Decrypt(encrypted, cert);
            BinaryEncoder.SendBytes(stream, decrypted);

            ServerHandshake serverHandshake = TextEncoder.ReadJson<ServerHandshake>(stream);
            logger.Log("Handshake", "Handshake", serverHandshake.Errors, false);

            if (!serverHandshake.Succeeded)
            {
                throw new Exception($"Handshake failed\n{serverHandshake.Errors}");
            }

            return new HandshakeReturnCapsula()
            {
                UserId = serverHandshake.UserId,
                UserName = serverHandshake.UserName,
                ClientId = serverHandshake.ClientId,
                SelfAesPassword = serverHandshake.SelfAesKey == null ? null : new AESPassword(RSAEncoder.DecryptAndVerify(serverHandshake.SelfAesKey, cert, cert))
            };
        }

    }
}
