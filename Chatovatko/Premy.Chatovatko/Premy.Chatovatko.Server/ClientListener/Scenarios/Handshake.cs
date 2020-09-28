using Premy.Chatovatko.Libs;
using Premy.Chatovatko.Libs.Cryptography;
using Premy.Chatovatko.Libs.DataTransmission;
using Premy.Chatovatko.Libs.DataTransmission.JsonModels.Handshake;
using Premy.Chatovatko.Server.Database.Models;
using Premy.Chatovatko.Server.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace Premy.Chatovatko.Server.ClientListener.Scenarios
{
    public static class Handshake
    {
        public static ConnectionInfo Run(Stream stream, Action<string> log, ServerConfig config)
        {
            
            ClientHandshake clientHandshake = TextEncoder.ReadJson<ClientHandshake>(stream);
            X509Certificate2 clientCertificate = X509Certificate2Utils.ImportFromPem(clientHandshake.PemCertificate);
            log($"Logging user sent username {clientHandshake.UserName}\n Certificate:\n {clientHandshake.PemCertificate}");

            ServerHandshake errorHandshake = new ServerHandshake()
            {
                Errors = "",
                NewUser = false,
                Succeeded = false,
                UserId = -1,
                UserName = ""
            };

            if (config.Password != null && !config.Password.Equals(clientHandshake.ServerPassword))
            {
                errorHandshake.Errors = "Server password is wrong.";
                TextEncoder.SendJson(stream, errorHandshake);
                throw new Exception(errorHandshake.Errors);
            }

            log("Generating random bytes");
            byte[] randomBytes = LUtils.GenerateRandomBytes(TcpConstants.HANDSHAKE_LENGHT);

            log("Sending encrypted bytes");
            BinaryEncoder.SendBytes(stream, RSAEncoder.Encrypt(randomBytes, clientCertificate));

            byte[] received = BinaryEncoder.ReceiveBytes(stream);
            if (!randomBytes.SequenceEqual(received))
            {
                log("Client's certificate verification failed.");
                errorHandshake.Errors = "Client's certificate verification failed.";
                TextEncoder.SendJson(stream, errorHandshake);
                throw new Exception(errorHandshake.Errors);
            }

            log("Certificate verification succeeded.");

            Users user;
            String message;
            Clients client;
            byte[] aesKey = null;
            bool newUser = false;
            using (Context context = new Context(config))
            {
                byte[] hash = SHA256Utils.ComputeByteSha256Hash(clientCertificate);
                user = context.Users.SingleOrDefault(u => u.PublicCertificateSha256.SequenceEqual(hash));

                if (user == null){
                    log("User doesn't exist yet. I'll try to create him.");
                    newUser = true;

                    log("Checking the uniquity of username.");
                    String userName = clientHandshake.UserName;
                    if (context.Users.SingleOrDefault(u => u.UserName.Equals(userName)) != null)
                    {
                        errorHandshake.Errors = "Username isn't unique.";
                        TextEncoder.SendJson(stream, errorHandshake);
                        throw new Exception(errorHandshake.Errors);
                    }
                    else if(userName.Length > 45)
                    {
                        errorHandshake.Errors = "Username is too long (max. 45 chars)";
                        TextEncoder.SendJson(stream, errorHandshake);
                        throw new Exception(errorHandshake.Errors);
                    }
                    else if (userName.Length < 4)
                    {
                        errorHandshake.Errors = "Username is too short (min. 4 chars)";
                        TextEncoder.SendJson(stream, errorHandshake);
                        throw new Exception(errorHandshake.Errors);
                    }
                    else if(!Validators.ValidateRegexUserName(userName))
                    {
                        errorHandshake.Errors = "Username must match this regex ^[a-zA-Z][-a-zA-Z0-9_]+$ (Vaguely can't contain special chars and spaces)";
                        TextEncoder.SendJson(stream, errorHandshake);
                        throw new Exception(errorHandshake.Errors);
                    }

                    log("Creating user.");
                    user = new Users()
                    {
                        PublicCertificate = clientHandshake.PemCertificate,
                        PublicCertificateSha256 = hash,
                        UserName = clientHandshake.UserName
                    };

                    context.Users.Add(user);
                    context.SaveChanges();

                    message = "User successfully created.";
                    log("User successfully created.");
                }
                else{
                    message = "User exists.";
                    log("User exists.");
                }

                client = new Clients()
                {
                    UserId = user.Id
                };

                if (clientHandshake.ClientId == null)
                {
                    log($"Loading self-aes key.");
                    aesKey = context.UsersKeys
                        .Where(u => u.RecepientId == user.Id)
                        .Where(u => u.SenderId == user.Id)
                        .Select(u => u.AesKey)
                        .SingleOrDefault();

                    context.Add(client);
                    context.SaveChanges();

                    log($"Added client with Id {client.Id}.");
                }
                else
                {
                    client.Id = (int)clientHandshake.ClientId;
                    if(context.Clients.Where(u => u.Id == client.Id).Single().UserId != user.Id)
                    {
                        errorHandshake.Errors = "This client id isn't owned by this user.";
                        TextEncoder.SendJson(stream, errorHandshake);
                        throw new Exception(errorHandshake.Errors);
                    }
                    
                    log($"Client with Id {client.Id} has logged in.");
                }


            }

            ServerHandshake toSend = new ServerHandshake()
            {
                Errors = message,
                NewUser = newUser,
                Succeeded = true,
                UserId = user.Id,
                UserName = user.UserName,
                ClientId = client.Id,
                SelfAesKey = aesKey
            };
            TextEncoder.SendJson(stream, toSend);

            ConnectionInfo ret = new ConnectionInfo(user, clientCertificate, client.Id);
            log($"Handshake successeded. User {ret.UserName} with id {ret.UserId} has logged in. Client has id {client.Id}.");
            return ret;
        }
    }
}
