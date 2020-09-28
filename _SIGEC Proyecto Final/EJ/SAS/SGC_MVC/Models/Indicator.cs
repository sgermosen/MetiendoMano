using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Indicator
    {
        public Indicator()
        {
            this.FormFields = new List<FormField>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public int ruleID { get; set; }
        public int processID { get; set; }
        public string target { get; set; }
        public string dataSourceMeasure { get; set; }
        public string measureUnit { get; set; }
        public Nullable<int> periodID { get; set; }
        public Nullable<int> responsibleOfGenerate { get; set; }
        public Nullable<int> responsableMonitor { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public int processTypeID { get; set; }
        public string formula { get; set; }
        public Nullable<decimal> lowerLimit { get; set; }
        public int companyID { get; set; }
        public Nullable<decimal> goal { get; set; }
        public Nullable<decimal> upperLimit { get; set; }
        public Nullable<int> frequency { get; set; }
        public Nullable<int> version { get; set; }
        public virtual Company Company { get; set; }
        public virtual Period Period { get; set; }
        public virtual Position Position { get; set; }
        public virtual Position Position1 { get; set; }
        public virtual Process Process { get; set; }
        public virtual Rule Rule { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<FormField> FormFields { get; set; }
    }
}
