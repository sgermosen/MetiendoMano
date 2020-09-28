using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class ObjectiveResource
    {
        public int ID { get; set; }
        public string infrastructure { get; set; }
        public string humans { get; set; }
        public int objectiveID { get; set; }
        public Nullable<int> createUser { get; set; }
        public virtual PlanObjective PlanObjective { get; set; }
    }
}
