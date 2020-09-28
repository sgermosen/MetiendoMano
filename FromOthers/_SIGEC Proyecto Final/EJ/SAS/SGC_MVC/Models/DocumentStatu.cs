using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class DocumentStatu
    {
        public int ID { get; set; }
        public int responsible { get; set; }
        public int documentID { get; set; }
        public int statusID { get; set; }
        public System.DateTime createDate { get; set; }
        public virtual Document Document { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
    }
}
