using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class UserIndexColum
    {
        public int ID { get; set; }
        public string config { get; set; }
        public Nullable<int> userID { get; set; }
        public virtual User User { get; set; }
    }
}
