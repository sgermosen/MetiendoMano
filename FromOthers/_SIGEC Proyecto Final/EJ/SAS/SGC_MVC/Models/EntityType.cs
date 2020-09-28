using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class EntityType
    {
        public EntityType()
        {
            this.Entities = new List<Entity>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public bool status { get; set; }
        public virtual ICollection<Entity> Entities { get; set; }
    }
}
