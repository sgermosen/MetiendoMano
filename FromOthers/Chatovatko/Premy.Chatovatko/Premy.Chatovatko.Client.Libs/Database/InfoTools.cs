using Premy.Chatovatko.Client.Libs.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Premy.Chatovatko.Client.Libs.Database
{
    public static class InfoTools
    {
        public static long GetMessageSenderUserId(Context context, long messageId)
        {
            var message = context.Messages
                .Where(u => u.Id == messageId)
                .Single();

            var blobMessage = context.BlobMessages.
                Where(u => u.Id == message.BlobMessagesId)
                .Single();

            return blobMessage.SenderId;
        }

        public static string GetMessageSenderShowName(Context context, long messageId)
        {
            long userId = GetMessageSenderUserId(context, messageId);
            var showName = context.Contacts
                .Where(u => u.PublicId == userId)
                .Select(u => u.ShowName)
                .Single();

            return showName;
        }

        public static long GetMessageThreadPublicId(Context context, long privateId)
        {
            return context.MessagesThread
                .Where(u => u.Id == privateId)
                .Select(u => u.PublicId)
                .Single();
        }
    }
}
