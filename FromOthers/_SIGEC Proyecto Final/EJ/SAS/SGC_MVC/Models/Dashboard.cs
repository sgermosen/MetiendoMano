using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Dashboard
    {
        public Dashboard()
        {
            this.DashboardItems = new List<DashboardItem>();
        }

        public int ID { get; set; }
        public int userID { get; set; }
        public virtual ICollection<DashboardItem> DashboardItems { get; set; }
        public virtual User User { get; set; }
    }
}
