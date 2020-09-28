using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class DashboardItem
    {
        public int ID { get; set; }
        public int dashboardID { get; set; }
        public Nullable<bool> expanded { get; set; }
        public Nullable<int> numOrder { get; set; }
        public virtual Dashboard Dashboard { get; set; }
    }
}
