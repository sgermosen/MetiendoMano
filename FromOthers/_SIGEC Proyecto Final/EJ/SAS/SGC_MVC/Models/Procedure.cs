using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Procedure
    {
        public Procedure()
        {
            this.ProcedureActivities = new List<ProcedureActivity>();
            this.Documents = new List<Document>();
            this.Glossaries = new List<Glossary>();
            this.Documents1 = new List<Document>();
            this.Documents2 = new List<Document>();
        }

        public int ID { get; set; }
        public int ruleID { get; set; }
        public int processID { get; set; }
        public int responsible { get; set; }
        public string target { get; set; }
        public string procedureScope { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public string name { get; set; }
        public int companyID { get; set; }
        public int statusID { get; set; }
        public int version { get; set; }
        public virtual Company Company { get; set; }
        public virtual Position Position { get; set; }
        public virtual Process Process { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<ProcedureActivity> ProcedureActivities { get; set; }
        public virtual Rule Rule { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual ICollection<Glossary> Glossaries { get; set; }
        public virtual ICollection<Document> Documents1 { get; set; }
        public virtual ICollection<Document> Documents2 { get; set; }
    }
}
