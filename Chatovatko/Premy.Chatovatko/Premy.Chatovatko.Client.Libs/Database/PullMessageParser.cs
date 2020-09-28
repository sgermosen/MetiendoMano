using Premy.Chatovatko.Client.Libs.Database.JsonModels;
using Premy.Chatovatko.Client.Libs.Database.Models;
using Premy.Chatovatko.Client.Libs.UserData;
using Premy.Chatovatko.Libs.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Premy.Chatovatko.Client.Libs.Database
{
    public static class PullMessageParser
    {
        public static void ParseEncryptedMessage(Context context, byte[] message, long senderId, long messageId, long myUserId)
        {
            JsonCapsula decoded = JsonEncoder.GetJsonDecoded(context, message, senderId);
            ParseIJTypeMessage(context, decoded, senderId, messageId, myUserId);    
        }

        public static void ParseIJTypeMessage(Context context, JsonCapsula decoded, long senderId, long messageId, long myUserId)
        {
            if(decoded == null)
            {
                return;
            }
            if(context.Contacts
                .Where(u => u.PublicId == senderId)
                .Select(u => u.Trusted)
                .SingleOrDefault() != 1)
            {
                throw new Exception($"User with id {senderId} isn't trusted.");
            }

            bool permission = senderId == myUserId;
            switch (decoded.Message.GetJsonType())
            {
                case JsonTypes.ALARM:
                    JAlarm alarm = (JAlarm)decoded.Message;
                    permission = permission || 
                        context.Contacts
                        .Where(u => u.PublicId == senderId)
                        .Select(u => u.AlarmPermission)
                        .SingleOrDefault() == 1;

                    if (permission)
                    { 
                        context.Alarms.Add(new Alarms()
                        {
                            BlobMessagesId = messageId,
                            Text = alarm.Text,
                            Time = alarm.Date.GetChatovatkoString()
                        });
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception($"User with id {senderId} doesn't have permission to set alarm.");
                    }
                    break;

                case JsonTypes.CONTACT:
                    JContact detail = (JContact)decoded.Message;
                    
                    if (permission)
                    {
                        var toUpdate = context.Contacts
                            .Where(u => u.PublicId == detail.PublicId)
                            .SingleOrDefault();
                        if(toUpdate != null)
                        {
                            toUpdate.NickName = detail.NickName;
                            toUpdate.PublicCertificate = detail.PublicCertificate;
                            toUpdate.AlarmPermission = detail.AlarmPermission ? 1 : 0;
                            toUpdate.PublicId = detail.PublicId;

                            toUpdate.ReceiveAesKey = detail.ReceiveAesKey;
                            toUpdate.SendAesKey = detail.SendAesKey;
                            toUpdate.UserName = detail.UserName;
                            toUpdate.Trusted = detail.Trusted ? 1 : 0;

                            toUpdate.BlobMessagesId = messageId;
                        }
                        else
                        { 
                            context.Contacts.Add(new Contacts()
                            {
                                NickName = detail.NickName,
                                PublicCertificate = detail.PublicCertificate,
                                AlarmPermission = detail.AlarmPermission ? 1 : 0,
                                PublicId = detail.PublicId,

                                ReceiveAesKey = detail.ReceiveAesKey,
                                SendAesKey = detail.SendAesKey,
                                UserName = detail.UserName,
                                Trusted = detail.Trusted ? 1 : 0,

                                BlobMessagesId = messageId                                
                            });
                        }
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception($"User with id {senderId} doesn't have permission to set contact detail.");
                    }
                    break;

                case JsonTypes.MESSAGES:
                    JMessage jmessage = (JMessage)decoded.Message;
                    long threadWithUser =(
                       from threads in context.MessagesThread
                       where threads.PublicId == jmessage.MessageThreadId
                       select threads.WithUser
                       ).SingleOrDefault();
                    permission = permission || threadWithUser == senderId;

                    if (permission)
                    {
                        bool onlive = (from threads in context.MessagesThread
                                       where threads.PublicId == jmessage.MessageThreadId
                                       select threads.Onlive)
                            .SingleOrDefault() == 1;

                        bool updated = false;
                        if (onlive)
                        {
                            var toUpdateInfo = (from bmessages in context.BlobMessages
                                            join messages in context.Messages on bmessages.Id equals messages.BlobMessagesId
                                            where bmessages.SenderId == senderId && messages.IdMessagesThread == jmessage.MessageThreadId
                                            select new { messages.BlobMessagesId, messages.Id })
                                            .SingleOrDefault();
                            if(toUpdateInfo != null)
                            {
                                var toUpdate = context.Messages
                                    .Where(m => m.Id == toUpdateInfo.Id)
                                    .SingleOrDefault();
                                updated = true;

                                toUpdate.Text = jmessage.Text;
                                toUpdate.Date = jmessage.Time.GetChatovatkoString();
                                toUpdate.BlobMessagesId = messageId;

                                context.SaveChanges();
                            }
                        }

                        if (!updated) { 
                            context.Messages.Add(new Messages()
                            {
                                Date = jmessage.Time.GetChatovatkoString(),
                                Text = jmessage.Text,
                                IdMessagesThread = jmessage.MessageThreadId,
                                BlobMessagesId = messageId
                            });
                            context.SaveChanges();
                        }
                    }
                    else
                    {
                        throw new Exception($"User with id {senderId} doesn't have permission to send this message.");
                    }
                    break;

                case JsonTypes.MESSAGES_THREAD:
                    JMessageThread messageThread = (JMessageThread)decoded.Message;
                    permission = permission || (messageThread.WithUserId == senderId && !messageThread.DoOnlyDelete);
                    if (permission)
                    {
                        var old = context.MessagesThread
                            .Where(u => u.PublicId == messageThread.PublicId)
                            .SingleOrDefault();
                        if(messageThread.DoOnlyDelete && old != null)
                        { 
                            context.Remove(old);
                        }
                        else if (messageThread.DoOnlyDelete)
                        {

                        }
                        else if(old != null)
                        {
                            old.Name = messageThread.Name;
                            old.BlobMessagesId = messageId;
                            old.Archived = messageThread.Archived ? 1 : 0;
                        }
                        else                        
                        { 
                            context.MessagesThread.Add(new MessagesThread
                            {
                                Name = messageThread.Name,
                                PublicId = messageThread.PublicId,
                                Onlive = messageThread.Onlive ? 1 : 0,
                                Archived = messageThread.Archived ? 1 : 0,
                                WithUser = messageThread.WithUserId,
                                BlobMessagesId = messageId
                            });
                        }
                        context.SaveChanges();
                    }
                    else
                    {
                        throw new Exception($"User with id {senderId} doesn't have permission to create/edit/delete this message thread.");
                    }
                    break;

                default:
                    throw new Exception($"Json type unknown.");
            }
        }

        
    }

    
}
