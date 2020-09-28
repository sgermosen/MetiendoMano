using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Subcategory
    {
        public Subcategory()
        {
            this.Processes = new List<Process>();
        }

        public int categoryID { get; set; }
        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public virtual Category Category { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Process> Processes { get; set; }
    }
}
