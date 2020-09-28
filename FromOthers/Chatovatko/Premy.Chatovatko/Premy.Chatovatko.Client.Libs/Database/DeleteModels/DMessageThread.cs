using System;
using System.Collections.Generic;
using System.Text;
using Premy.Chatovatko.Client.Libs.Database.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Premy.Chatovatko.Client.Libs.Database.JsonModels;

namespace Premy.Chatovatko.Client.Libs.Database.DeleteModels
{
    public class DMessageThread : IDeleteModel
    {
        private readonly MessagesThread thread;
        private readonly long myUserId;

        public DMessageThread(MessagesThread thread, long myUserId)
        {
            this.thread = thread;
            this.myUserId = myUserId;
        }

        public void DoDelete(Context context)
        {
            foreach(var message in context.Messages
                .Where(u => u.IdMessagesThread == thread.PublicId))
            {
                DMessage dMessage = new DMessage(message, myUserId);
                dMessage.DoDelete(context);
            }

            context.MessagesThread.Remove(thread);
            var jthread = new JMessageThread()
            {
                PublicId = thread.PublicId,
                DoOnlyDelete = true
            };

            PushOperations.SendJsonCapsula(context, new JsonCapsula(jthread), myUserId, myUserId);

            //PushOperations.DeleteBlobMessage(context, thread.GetBlobId(), myUserId);
            //Piggy bug fix
            //throw new NotSupportedException("Not supported, because developer of this app is absolutly useless.");
        }
    }
}
