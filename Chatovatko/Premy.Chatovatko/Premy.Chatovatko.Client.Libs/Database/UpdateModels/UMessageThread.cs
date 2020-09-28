using Premy.Chatovatko.Client.Libs.Database.JsonModels;
using Premy.Chatovatko.Client.Libs.Database.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Database.UpdateModels
{
    public class UMessageThread : IUpdateModel
    {
        private readonly long recepientId;
        private readonly long myUserId;

        public UMessageThread(MessagesThread thread, long myUserId)
        {
            this.recepientId = thread.WithUser;
            this.myUserId = myUserId;

            Archived = thread.Archived == 1;
            BlobMessageId = thread.BlobMessagesId;
            Name = thread.Name;
            Onlive = thread.Onlive == 1;
            WithUser = thread.WithUser;
            PublicId = thread.PublicId;
        }

        public bool Archived { get; set; }
        public long BlobMessageId { get; }
        public string Name { get; set; }
        public bool Onlive { get; }
        public long WithUser { get; }
        public long PublicId { get; }


        public UpdateModelTypes GetModelType()
        {
            return UpdateModelTypes.MESSAGE_THREAD;
        }

        public JsonCapsula GetRecepientUpdate()
        {
            return new JsonCapsula(new JMessageThread()
            {
                Archived = this.Archived,
                Name = this.Name,
                Onlive = this.Onlive,
                PublicId = this.PublicId,
                WithUserId = myUserId
            });
        }

        public JsonCapsula GetSelfUpdate()
        {
            if (recepientId == myUserId)
            {
                return null;
            }
            return new JsonCapsula(new JMessageThread()
            {
                Archived = this.Archived,
                Name = this.Name,
                Onlive = this.Onlive,
                PublicId = this.PublicId,
                WithUserId = recepientId
            });
        }
    }
}
