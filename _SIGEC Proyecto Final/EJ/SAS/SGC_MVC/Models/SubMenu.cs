using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class SubMenu
    {
        public int ID { get; set; }
        public int menuID { get; set; }
        public string name { get; set; }
        public int viewID { get; set; }
        public Nullable<int> noOrder { get; set; }
        public virtual Menu Menu { get; set; }
        public virtual View View { get; set; }
    }
}
