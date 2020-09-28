using System;
using System.Collections.Generic;
using System.Text;
using Premy.Chatovatko.Client.Libs.Database.Models;

namespace Premy.Chatovatko.Client.Libs.Database.DeleteModels
{
    public class DAlarm : IDeleteModel
    {
        private readonly Alarms alarm;
        private readonly long myUserId;

        public DAlarm(Alarms alarm, long myUserId)
        {
            this.alarm = alarm;
            this.myUserId = myUserId;
        }

        public void DoDelete(Context context)
        {
            context.Alarms.Remove(alarm);
            PushOperations.DeleteBlobMessage(context, alarm.GetBlobId(), myUserId);
        }
    }
}
