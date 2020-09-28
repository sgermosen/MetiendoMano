using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Period
    {
        public Period()
        {
            this.Indicators = new List<Indicator>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public int interval { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public virtual ICollection<Indicator> Indicators { get; set; }
        public virtual User User { get; set; }
    }
}
