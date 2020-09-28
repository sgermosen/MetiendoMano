using System;
using System.Collections.Generic;
using System.Text;

namespace Premy.Chatovatko.Client.Libs.Database.JsonModels
{
    public class JAlarm : IJType
    {
        public DateTime Date { get; set; }
        public String Text { get; set; }

        public JsonTypes GetJsonType()
        {
            return JsonTypes.ALARM;
        }

        public long GetPriority()
        {
            return 10;
        }
    }
}
