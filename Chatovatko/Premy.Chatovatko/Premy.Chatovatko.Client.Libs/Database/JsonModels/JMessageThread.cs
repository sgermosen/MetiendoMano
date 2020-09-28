using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Database.JsonModels
{
    public class JMessageThread : IJType
    {
        public string Name { get; set; }
        public bool Onlive { get; set; }
        public bool Archived { get; set; }
        public long WithUserId { get; set; }
        public long PublicId { get; set; }
        public bool DoOnlyDelete { get; set; } = false;

        public JsonTypes GetJsonType()
        {
            return JsonTypes.MESSAGES_THREAD;
        }
        public long GetPriority()
        {
            return 10;
        }
    }
}
