using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Database.JsonModels
{
    public class JMessage : IJType
    {
        public long MessageThreadId { get; set; }
        public string Text { get; set; }
        public DateTime Time { get; set; }

        public JsonTypes GetJsonType()
        {
            return JsonTypes.MESSAGES;
        }
        public long GetPriority()
        {
            return 10;
        }
    }
}
