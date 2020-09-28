using System;
using System.Collections.Generic;
using System.Text;
using Premy.Chatovatko.Client.Libs.Database.JsonModels;
using Premy.Chatovatko.Client.Libs.Database.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Premy.Chatovatko.Client.Libs.Database.InsertModels
{
    public class CMessageThread : ICInsertModel
    {
        private readonly long recepientId;
        private readonly long myUserId;
        public CMessageThread(Context context, String name, bool onlive, long recepientId, long myUserId, bool archived = false)
        {
            var settings = context.Settings.Single();
            PublicId = settings.LastUniqueId;
            ++settings.LastUniqueId;
            context.SaveChanges();

            this.recepientId = recepientId;
            this.myUserId = myUserId;

            Onlive = onlive;
            Name = name;
            Archived = archived;
        }

        public String Name { get; set; }
        public bool Onlive { get; set; }
        public long PublicId { get; }
        public bool Archived { get; set; }


        public InsertModelTypes GetModelType()
        {
            return InsertModelTypes.MESSAGE_THREAD;
        }

        public JsonCapsula GetRecepientUpdate()
        {
            return new JsonCapsula(new JMessageThread()
            {
                Archived = Archived,
                Name = Name,
                Onlive = Onlive,
                PublicId = PublicId,
                WithUserId = myUserId
            });
        }

        public JsonCapsula GetSelfUpdate()
        {
            if(recepientId == myUserId)
            {
                return null;
            }
            return new JsonCapsula(new JMessageThread()
            {
                Archived = Archived,
                Name = Name,
                Onlive = Onlive,
                PublicId = PublicId,
                WithUserId = recepientId
            });
        }
    }
}
