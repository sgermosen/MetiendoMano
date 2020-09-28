using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class ProcessDocument
    {
        public int processID { get; set; }
        public Nullable<int> documentID { get; set; }
        public string text { get; set; }
        public int ID { get; set; }
        public Nullable<bool> ISORequirement { get; set; }
        public virtual Document Document { get; set; }
        public virtual Process Process { get; set; }
    }
}
