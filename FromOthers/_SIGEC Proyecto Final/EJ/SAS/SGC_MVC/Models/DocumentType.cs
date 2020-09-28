using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class DocumentType
    {
        public DocumentType()
        {
            this.Documents = new List<Document>();
            this.Rules = new List<Rule>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Rule> Rules { get; set; }
    }
}
