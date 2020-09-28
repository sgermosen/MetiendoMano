using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class ProcedureActivity
    {
        public int ID { get; set; }
        public int procedureID { get; set; }
        public int responsible { get; set; }
        public string activity { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public virtual Procedure Procedure { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
