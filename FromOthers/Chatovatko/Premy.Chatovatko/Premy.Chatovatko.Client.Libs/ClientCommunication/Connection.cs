//#define DEBUG
#undef DEBUG

using Premy.Chatovatko.Client.Libs.ClientCommunication.Scenarios;
using Premy.Chatovatko.Client.Libs.Database.Models;
using Premy.Chatovatko.Client.Libs.UserData;
using Premy.Chatovatko.Libs;
using Premy.Chatovatko.Libs.DataTransmission;
using Premy.Chatovatko.Libs.Logging;
using System;
using System.Collections.Generic;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Premy.Chatovatko.Client.Libs.Database;
using Premy.Chatovatko.Client.Libs.Cryptography;
using Premy.Chatovatko.Client.Libs.Database.JsonModels;
using Premy.Chatovatko.Libs.Cryptography;
using Premy.Chatovatko.Client.Libs.Database.UpdateModels;
using Premy.Chatovatko.Libs.DataTransmission.JsonModels.Pull;
using Premy.Chatovatko.Libs.DataTransmission.JsonModels.Push;
using Premy.Chatovatko.Libs.DataTransmission.JsonModels.SearchContact;
using System.Threading;

namespace Premy.Chatovatko.Client.Libs.ClientCommunication
{
    public class Connection : ILoggable
    {
        private readonly int ServerPort = TcpConstants.MAIN_SERVER_PORT;
        private TcpClient client;
        private bool isConnected = false;
        private readonly IClientDatabaseConfig config;

        private SslStream stream;
        private Logger logger;

        private readonly String serverAddress;
        private readonly IConnectionVerificator verificator;

        public int UserId { get; private set; }
        public string UserName { get; private set; }
        public int? ClientId { get; private set; }
        public X509Certificate2 MyCertificate { get; }
        public AESPassword SelfAesPassword { get; private set; }

        public Mutex mutex = new Mutex();

        /// <summary>
        /// Constructor for init operations.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="verificator"></param>
        /// <param name="serverAddress"></param>
        /// <param name="clientCertificate"></param>
        /// <param name="userName"></param>
        public Connection(Logger logger, IConnectionVerificator verificator, String serverAddress,
            X509Certificate2 clientCertificate, IClientDatabaseConfig config, String userName = null)
        {
            this.logger = logger;
            this.verificator = verificator;
            this.serverAddress = serverAddress;
            this.MyCertificate = clientCertificate;
            this.UserName = userName;
            this.config = config;
            this.ClientId = null;
        }

        /// <summary>
        /// Constructor for regular use.
        /// Verification will run against public key in settings.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="settings"></param>
        public Connection(Logger logger, SettingsCapsula settings)
        {
            this.logger = logger;
            this.verificator = new ConnectionVerificator(logger, settings.ServerPublicCertificate);
            this.serverAddress = settings.ServerAddress;
            this.MyCertificate = settings.ClientCertificate;
            this.UserName = settings.UserName;
            this.config = settings.Config;
            this.ClientId = (int)settings.ClientId;
        }

        public bool IsConnected()
        {
            return isConnected && stream.IsEncrypted;
        }

        public void Connect(String password = null)
        {
            lock (mutex)
            {
                client = new TcpClient(serverAddress, ServerPort);
                logger.Log(this, "Client connected.");

                stream = new SslStream(client.GetStream(), false, verificator.AppCertificateValidation);
                X509CertificateCollection clientCertificates = new X509CertificateCollection();
                clientCertificates.Add(MyCertificate);

                stream.AuthenticateAsClient("Dummy", clientCertificates, SslProtocols.Tls12, false);
                logger.Log(this, "SSL authentication completed.");


                logger.Log(this, "Handshake started.");
                var handshake = Handshake.Login(logger, stream, MyCertificate, password, UserName, ClientId);
                logger.Log(this, "Handshake successeded.");

                UserName = handshake.UserName;
                UserId = handshake.UserId;
                ClientId = handshake.ClientId;
                SelfAesPassword = handshake.SelfAesPassword;
                logger.Log(this, $"User {UserName} has id {UserId}. Client has id {ClientId}.");

                isConnected = true;
            }
        }

        public void Disconnect()
        {
            lock (mutex)
            {
                Log("Sending END_CONNECTION command.");
                isConnected = false;
                BinaryEncoder.SendCommand(stream, ConnectionCommand.END_CONNECTION);
                stream.Close();
                client.Close();
            }
        }

        public void Push()
        {
            ThrowExceptionIfNotConnected();
            lock (mutex)
            {

                Log("Sending PUSH command.");
                BinaryEncoder.SendCommand(stream, ConnectionCommand.PUSH);
                using (Context context = new Context(config))
                {
                    List<long> selfMessages = new List<long>();
                    var toSend = context.ToSendMessages.ToList();

                    PushCapsula capsula = new PushCapsula();

                    foreach (var message in toSend)
                    {
                        if (message.RecepientId == UserId)
                        {
                            selfMessages.Add((long)message.BlobMessagesId);
                        }
                        capsula.PushMessages.Add(new PushMessage()
                        {
                            RecepientId = (int)message.RecepientId,
                            Priority = (int)message.Priority
                        });
                    }
                    capsula.MessageToDeleteIds = context.BlobMessages
                        .Where(u => u.DoDelete == 1)
                        .Select(u => (long)u.PublicId).ToList();

#if (DEBUG)
                    Log($"Sending capsula with {toSend.Count} messages. {capsula.MessageToDeleteIds.Count} will be deleted.");
#endif
                    TextEncoder.SendJson(stream, capsula);
#if (DEBUG)
                    Log($"Sending message blobs.");
#endif
                    foreach (var message in toSend)
                    {
                        BinaryEncoder.SendBytes(stream, message.Blob);
                    }
#if (DEBUG)
                    Log($"Receiving PushResponse");
#endif
                    PushResponse response = TextEncoder.ReadJson<PushResponse>(stream);
                    var selfMessagesZip = selfMessages.Zip(response.MessageIds, (u, v) =>
                        new { PrivateId = u, PublicId = v });

                    foreach (var message in selfMessagesZip)
                    {
                        context.BlobMessages.Where(u => u.Id == message.PrivateId)
                            .SingleOrDefault().PublicId = message.PublicId;
                    }
#if (DEBUG)
                    Log("Saving new public ids.");
#endif
                    context.SaveChanges();
#if (DEBUG)
                    Log("Cleaning queue.");
#endif
                    context.Database.ExecuteSqlCommand("delete from TO_SEND_MESSAGES;");
                    context.Database.ExecuteSqlCommand("delete from BLOB_MESSAGES where DO_DELETE=1 and PUBLIC_ID<>null;;");
                    context.SaveChanges();

                }

                Log("Push have been done.");
            }
        }

        public int Pull()
        {
            int changes = 0;
            ThrowExceptionIfNotConnected();

            lock (mutex)
            {
                Log("Sending PULL command.");
                BinaryEncoder.SendCommand(stream, ConnectionCommand.PULL);
#if (DEBUG)
                Log("Sending ClientPullCapsula.");
#endif
                ClientPullCapsula clientCapsula;
                using (Context context = new Context(config))
                {
                    clientCapsula = new ClientPullCapsula()
                    {
                        AesKeysUserIds = context.Contacts
                            .Where(u => u.ReceiveAesKey == null)
                            .Select(u => u.PublicId)
                            .ToArray()
                    };
                }
                TextEncoder.SendJson(stream, clientCapsula);

                ServerPullCapsula capsula = TextEncoder.ReadJson<ServerPullCapsula>(stream);
#if (DEBUG)
                Log("Received ServerPullCapsula.");
#endif
                changes += capsula.AesKeysUserIds.Count;
                changes += capsula.Messages.Count;
                using (Context context = new Context(config))
                {
#if (DEBUG)
                    Log("Receiving and saving AES keys.");
#endif
                    foreach (var id in capsula.AesKeysUserIds)
                    {
                        var user = new UContact(context.Contacts.Where(con => con.PublicId == id).SingleOrDefault());
                        try
                        {
                            user.ReceiveAesKey = RSAEncoder.DecryptAndVerify(BinaryEncoder.ReceiveBytes(stream), MyCertificate, X509Certificate2Utils.ImportFromPem(user.PublicCertificate));
                        }
                        catch (Exception ex)
                        {
                            Log($"Loading of Receive AESKey from {user.PublicId} has failed.");
                            logger.LogException(this, ex);
                        }
                        PushOperations.Update(context, user, UserId, UserId);
                    }
                    context.SaveChanges();

#if (DEBUG)
                    Log("Receiving and saving messages.");
#endif
                    foreach (PullMessage metaMessage in capsula.Messages)
                    {

                        BlobMessages metaBlob = new BlobMessages()
                        {
                            PublicId = metaMessage.PublicId,
                            SenderId = metaMessage.SenderId,
                            Failed = 0,
                            DoDelete = 0
                        };
                        context.BlobMessages.Add(metaBlob);
                        context.SaveChanges();

                        try
                        {
                            PullMessageParser.ParseEncryptedMessage(context, BinaryEncoder.ReceiveBytes(stream), metaBlob.SenderId, metaBlob.Id, UserId);
                        }
                        catch (Exception ex)
                        {
                            Log($"Loading of message {metaMessage.PublicId} has failed.");
                            metaBlob.Failed = 1;
                            logger.LogException(this, ex);
                        }
                        context.SaveChanges();


                    }
                }
                Log("Pull have been done.");
            }
            return changes;
        }


        public void UntrustContact(int contactId)
        {
            ThrowExceptionIfNotConnected();
            lock (mutex)
            {

                if (contactId == this.UserId)
                {
                    throw new Exception("You really don't want untrust yourself.");
                }

                Log("Sending UNTRUST_CONTACT command.");
                BinaryEncoder.SendCommand(stream, ConnectionCommand.UNTRUST_CONTACT);
                BinaryEncoder.SendInt(stream, contactId);
                using (Context context = new Context(config))
                {
                    var contact = new UContact(context.Contacts
                        .Where(u => u.PublicId == contactId)
                        .SingleOrDefault())
                    {
                        Trusted = false
                    };

                    PushOperations.SendJsonCapsula(context, contact.GetSelfUpdate(), UserId, UserId);

                    context.SaveChanges();
                }
            }
            Push();
        }

        public void TrustContact(int contactId)
        {
            
            ThrowExceptionIfNotConnected();

            Pull();
            lock (mutex)
            {
                Log("Sending TRUST_CONTACT command.");
                BinaryEncoder.SendCommand(stream, ConnectionCommand.TRUST_CONTACT);

                BinaryEncoder.SendInt(stream, contactId);
                using (Context context = new Context(config))
                {
                    var contact = new UContact(context.Contacts
                        .Where(u => u.PublicId == contactId)
                        .SingleOrDefault())
                    {
                        Trusted = true
                    };


                    if (contact.SendAesKey == null)
                    {
                        Log("Sending new key.");
                        BinaryEncoder.SendInt(stream, 1);
                        AESPassword password = AESPassword.GenerateAESPassword();

                        contact.SendAesKey = password.Password;
                        X509Certificate2 recepientCert = X509Certificate2Utils.ImportFromPem(
                            context.Contacts
                            .Where(u => u.PublicId == contactId)
                            .Select(u => u.PublicCertificate)
                            .SingleOrDefault());

                        byte[] toSend = RSAEncoder.EncryptAndSign(password.Password, recepientCert, MyCertificate);
                        BinaryEncoder.SendBytes(stream, toSend);

                    }
                    else
                    {
                        Log("No new key will be sended.");
                        BinaryEncoder.SendInt(stream, 0);
                    }

                    if (contactId != this.UserId)
                    {
                        PushOperations.SendJsonCapsula(context, contact.GetSelfUpdate(), UserId, UserId);
                    }
                    else
                    {
                        var me = context.Contacts
                            .Where(u => u.PublicId == UserId)
                            .Single();
                        me.SendAesKey = contact.SendAesKey;
                        me.ReceiveAesKey = contact.ReceiveAesKey;
                    }

                    context.SaveChanges();
                }
                Log("Trustification has been done.");
            }
            Push();

            
        }

        public SearchCServerCapsula SearchContact(int publicId)
        {
            return SearchContact(new SearchCClientCapsula()
            {
                UserId = publicId
            });
        }

        public SearchCServerCapsula SearchContact(String username)
        {
            return SearchContact(new SearchCClientCapsula()
            {
                UserName = username
            });
        }

        public SearchCServerCapsula SearchContact(byte[] certificateHash)
        {
            return SearchContact(new SearchCClientCapsula()
            {
                CertificateHash = certificateHash
            });
        }

        public SearchCServerCapsula SearchContact(SearchCClientCapsula searchCClientCapsula)
        {
            lock (mutex)
            {
                ThrowExceptionIfNotConnected();
                BinaryEncoder.SendCommand(stream, ConnectionCommand.SEARCH_CONTACT);

                TextEncoder.SendJson(stream, searchCClientCapsula);

                return TextEncoder.ReadJson<SearchCServerCapsula>(stream);
            }
            
        }

        private void ThrowExceptionIfNotConnected()
        {
            if (!IsConnected())
            {
                throw new Exception("The connection is disconnected.");
            }
        }


        public string GetLogSource()
        {
            return "Connection";
        }

        private void Log(String message)
        {
            logger.Log(this, message);
        }


    }
}
