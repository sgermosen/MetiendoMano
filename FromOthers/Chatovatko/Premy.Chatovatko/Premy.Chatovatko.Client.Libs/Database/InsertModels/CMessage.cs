using System;
using System.Collections.Generic;
using System.Text;
using Premy.Chatovatko.Client.Libs.Database.JsonModels;

namespace Premy.Chatovatko.Client.Libs.Database.InsertModels
{
    public class CMessage : JMessage, ICInsertModel
    {
        private readonly long recepientId;
        private readonly long myUserId;
        private readonly byte[] attechment;
        public CMessage(long messageThreadPublicId, string text, DateTime time, long recepientId, long myUserId, byte[] attechment = null)
        {
            this.MessageThreadId = messageThreadPublicId;
            this.Text = text;
            this.Time = time;
            this.recepientId = recepientId;
            this.myUserId = myUserId;
            this.attechment = attechment;
        }

        public InsertModelTypes GetModelType()
        {
            return InsertModelTypes.MESSAGE;
        }

        public JsonCapsula GetRecepientUpdate()
        {
            if (recepientId == myUserId)
            {
                return null;
            }
            return new JsonCapsula()
            {
                Attechment = attechment,
                Message = this
            };
        }

        public JsonCapsula GetSelfUpdate()
        {
            return new JsonCapsula()
            {
                Attechment = attechment,
                Message = this
            };
        }
    }
}
