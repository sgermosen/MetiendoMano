//#define DEBUG
#undef  DEBUG

using Premy.Chatovatko.Libs;
using Premy.Chatovatko.Libs.DataTransmission;
using Premy.Chatovatko.Libs.Logging;
using Premy.Chatovatko.Server.ClientListener.Scenarios;
using Premy.Chatovatko.Server.Database;
using Premy.Chatovatko.Server.Database.Models;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using Premy.Chatovatko.Libs.DataTransmission.JsonModels.Pull;
using Premy.Chatovatko.Libs.DataTransmission.JsonModels.Push;
using Premy.Chatovatko.Libs.DataTransmission.JsonModels.SearchContact;

namespace Premy.Chatovatko.Server.ClientListener
{
    public class Godot : ILoggable
    {
        private readonly ulong id;
        private SslStream stream;

        private readonly Logger logger;
        private readonly X509Certificate2 serverCert;
        private readonly GodotCounter godotCounter;

        private readonly ServerConfig config;
        private ConnectionInfo connectionInfo;
        

        public Godot(ulong id, Logger logger, ServerConfig config, X509Certificate2 serverCert, GodotCounter godotCounter)
        {
            this.id = id;
            this.logger = logger;
            this.serverCert = serverCert;
            this.godotCounter = godotCounter;
            this.config = config;
            godotCounter.IncreaseCreated();

            logger.Log(this, "Godot has been born.");

        }


        public void Run(TcpClient client)
        {
            try
            {
                logger.Log(this, String.Format("Godot has been activated. Client IP address is {0}",
                    LUtils.GetIpAddress(client)));
                godotCounter.IncreaseRunning();

                stream = new SslStream(client.GetStream(), false, CertificateValidation);
                stream.AuthenticateAsServer(serverCert, true, SslProtocols.Tls12, false);

                logger.Log(this, "SSL authentication completed. Starting Handshake.");
                this.connectionInfo = Handshake.Run(stream, Log, config);


                bool running = true;
                while (running)
                {
                    ConnectionCommand command = BinaryEncoder.ReadCommand(stream);
                    switch (command)
                    {
                        case ConnectionCommand.TRUST_CONTACT:
                            Log("TRUST_CONTACT command received.");
                            TrustContact();
                            break;

                        case ConnectionCommand.UNTRUST_CONTACT:
                            Log("UNTRUST_CONTACT command received.");
                            UntrustContact();
                            break;

                        case ConnectionCommand.PULL:
#if (DEBUG)
                            Log("PULL command received.");
#endif
                            Push();
                            break;

                        case ConnectionCommand.PUSH:
#if (DEBUG)
                            Log("PUSH command received.");
#endif
                            Pull();
                            break;

                        case ConnectionCommand.SEARCH_CONTACT:
                            Log("SEARCH_CONTACT command received.");
                            SearchContact();
                            break;

                        case ConnectionCommand.END_CONNECTION:
                            Log("END_CONNECTION command received.");
                            running = false;
                            break;

                        default:
                            throw new Exception("Received unknown command.");

                    }
                }

            }
            catch (Exception ex)
            {
                logger.Log(this, "Godot has crashed.");
                logger.LogException(this, ex);
            }
            finally
            {
                stream.Close();
                client.Close();
                godotCounter.IncreaseDestroyed();
                logger.Log(this, "Godot has died.");
            }
        }

        private void SearchContact()
        {
            SearchCClientCapsula searchCapsula = TextEncoder.ReadJson<SearchCClientCapsula>(stream);
            SearchCServerCapsula ret;
            using(Context context = new Context(config))
            {
                ret = new SearchCServerCapsula(context.Users
                    .Where(u => u.Id == searchCapsula.UserId)
                    .SingleOrDefault());

                if(!ret.Succeeded)
                    ret = new SearchCServerCapsula(context.Users
                    .Where(u => u.UserName == searchCapsula.UserName)
                    .SingleOrDefault());

                if(!ret.Succeeded)
                    ret = new SearchCServerCapsula(context.Users
                    .Where(u => u.PublicCertificateSha256 == searchCapsula.CertificateHash)
                    .SingleOrDefault());

            }
            TextEncoder.SendJson(stream, ret);
        }

        
        private void Pull()
        {
#if (DEBUG)
            Log("Pulling started.");
            Log("Receiving PushCapsula.");
#endif

            PushCapsula capsula = TextEncoder.ReadJson<PushCapsula>(stream);
            PushResponse response = new PushResponse()
            {
                MessageIds = new List<long>()
            };

            using (Context context = new Context(config))
            {
#if (DEBUG)
                Log($"Receiving and saving {capsula.PushMessages.Count} blobs.");
#endif
                
                foreach (var pushMessage in capsula.PushMessages)
                {
                    BlobMessages blobMessage = new BlobMessages()
                    {
                        Content = BinaryEncoder.ReceiveBytes(stream),
                        RecepientId = pushMessage.RecepientId,
                        SenderId = connectionInfo.UserId,
                        Priority = pushMessage.Priority
                    };
                    context.BlobMessages.Add(blobMessage);
                    context.SaveChanges();

                    if (pushMessage.RecepientId == connectionInfo.UserId)
                    {
                        response.MessageIds.Add(blobMessage.Id);
                        context.ClientsMessagesDownloaded.Add(new ClientsMessagesDownloaded()
                        {
                            BlobMessagesId = blobMessage.Id,
                            ClientsId = connectionInfo.ClientId
                        });
                    }
                    context.SaveChanges();
                }
#if (DEBUG)
                Log($"Deleting {capsula.MessageToDeleteIds.Count} blobs.");
#endif
                foreach(var toDeleteId in capsula.MessageToDeleteIds)
                {
                    var toDelete = context.BlobMessages
                        .Where(u => u.RecepientId == connectionInfo.UserId && u.Id == toDeleteId).SingleOrDefault();
                    if(toDelete != null)
                    { 
                        context.BlobMessages.Remove(toDelete);
                    }
                }
                context.SaveChanges();
            }


#if (DEBUG)
            Log("Sending push response.");
#endif
            TextEncoder.SendJson(stream, response);
#if (DEBUG)
            Log("Pulling ended.");
#endif
        }

        private void Push()
        {
#if (DEBUG)
            Log("Pushing started.");
#endif
            ClientPullCapsula clientPullCapsula = TextEncoder.ReadJson<ClientPullCapsula>(stream);

            using (Context context = new Context(config))
            {
                ServerPullCapsula capsula = new ServerPullCapsula();

                List<byte[]> messagesBlobsToSend = new List<byte[]>();
                List<byte[]> aesBlobsToSend = new List<byte[]>();

                var messagesIdsUploaded = context.ClientsMessagesDownloaded
                    .Where(u => u.ClientsId == connectionInfo.ClientId)
                    .Select(u => u.BlobMessagesId);

                List<PullMessage> pullMessages = new List<PullMessage>();
                foreach (var message in
                    from messages in context.BlobMessages
                    orderby messages.Priority descending, messages.Id ascending //Message order must be respected
                    where messages.RecepientId == connectionInfo.UserId //Messages of connected user
                    where !messagesIdsUploaded.Contains(messages.Id) //New messages

                    join keys in context.UsersKeys on messages.RecepientId equals keys.SenderId //Keys sended by connected user
                    where keys.Trusted == true //Only trusted
                    where messages.SenderId == keys.RecepientId //Trusted information just only about sending user
                    select new { messages.SenderId, messages.Content, messages.Id }
                    )
                {
                    pullMessages.Add(new PullMessage()
                    {
                        PublicId = message.Id,
                        SenderId = message.SenderId
                    });
                    messagesBlobsToSend.Add(message.Content);
                    context.ClientsMessagesDownloaded.Add(new ClientsMessagesDownloaded()
                    {
                        BlobMessagesId = message.Id,
                        ClientsId = connectionInfo.ClientId
                    });
                }
                capsula.Messages = pullMessages;
#if (DEBUG)
                Log($"{messagesBlobsToSend.Count} messageBlobs will be pushed.");
#endif
                capsula.AesKeysUserIds = new List<long>();
                foreach (var user in
                    from userKeys in context.UsersKeys
                    where userKeys.RecepientId == connectionInfo.UserId
                    where clientPullCapsula.AesKeysUserIds.Contains(userKeys.SenderId)
                    select new { userKeys.SenderId, userKeys.AesKey })
                {
                    capsula.AesKeysUserIds.Add(user.SenderId);
                    aesBlobsToSend.Add(user.AesKey);
                }
#if (DEBUG)
                Log($"{capsula.AesKeysUserIds.Count} AES keys will be pushed.");
                Log($"Sending PullCapsula.");
#endif
                TextEncoder.SendJson(stream, capsula);
#if (DEBUG)
                Log($"Sending AES keys.");
#endif
                foreach (byte[] data in aesBlobsToSend)
                {
                    BinaryEncoder.SendBytes(stream, data);
                }
#if (DEBUG)
                Log($"Sending Messages content.");
#endif
                foreach (byte[] data in messagesBlobsToSend)
                {
                    BinaryEncoder.SendBytes(stream, data);
                }
                stream.Flush();
                context.SaveChanges();
            }
#if (DEBUG)
            Log("Pushing completed.");
#endif
        }


        private void UntrustContact()
        {
            int recepientId = BinaryEncoder.ReadInt(stream);
            using (Context context = new Context(config))
            {
                var key = context.UsersKeys
                    .Where(u => u.RecepientId == recepientId)
                    .Where(u => u.SenderId == connectionInfo.UserId)
                    .SingleOrDefault();
                if(key != null)
                {
                    key.Trusted = false;
                    context.SaveChanges();
                }
            }
            Log("Untrust contact done.");
        }

        private void TrustContact()
        {
            int recepientId = BinaryEncoder.ReadInt(stream);
            using(Context context = new Context(config))
            {
                var key = context.UsersKeys
                    .Where(u => u.RecepientId == recepientId)
                    .Where(u => u.SenderId == connectionInfo.UserId)
                    .SingleOrDefault();
                if(BinaryEncoder.ReadInt(stream) == 0)
                {
                    Log("No new key will be received.");
                    key.Trusted = true;
                }
                else
                {
                    Log("Receiving new key.");
                    var aesKey = BinaryEncoder.ReceiveBytes(stream);
                    if (key != null)
                    {
                        Log("Updating denied!");
                        //key.AesKey = aesKey;
                        key.Trusted = true;
                    }
                    else
                    {
                        key = new UsersKeys()
                        {
                            RecepientId = recepientId,
                            SenderId = connectionInfo.UserId,
                            AesKey = aesKey,
                            Trusted = true
                        };
                        context.Add(key);
                    }
                }
                context.SaveChanges();
            }
            Log("Trust contact done.");
        }



        private bool CertificateValidation(Object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }


        public string GetLogSource()
        {
            return String.Format("Godot {0}", id);
        }

        private void Log(String message)
        {
            logger.Log(this, message);
        }
    }
}
