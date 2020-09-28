using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Infrastructure
    {
        public int ID { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public int companyID { get; set; }
        public Nullable<int> createUser { get; set; }
        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
    }
}
