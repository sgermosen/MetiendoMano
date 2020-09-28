using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Entity
    {
        public Entity()
        {
            this.Processes = new List<Process>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string phone { get; set; }
        public int entityTypeID { get; set; }
        public bool status { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public int companyID { get; set; }
        public Nullable<int> createUser { get; set; }
        public virtual Company Company { get; set; }
        public virtual EntityType EntityType { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Process> Processes { get; set; }
    }
}
