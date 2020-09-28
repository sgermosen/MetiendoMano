using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Option
    {
        public int ID { get; set; }
        public int questionID { get; set; }
        public int noOrder { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public virtual Question Question { get; set; }
        public virtual User User { get; set; }
    }
}
