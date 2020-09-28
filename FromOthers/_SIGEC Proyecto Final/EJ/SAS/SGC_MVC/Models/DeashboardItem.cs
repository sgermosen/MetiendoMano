using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class DeashboardItem
    {
        public int ID { get; set; }
        public byte[] name { get; set; }
        public int actionID { get; set; }
        public string description { get; set; }
        public virtual Action Action { get; set; }
    }
}
