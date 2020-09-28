using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Document
    {
        public Document()
        {
            this.AuditProcesses = new List<AuditProcess>();
            this.DocumentStatus = new List<DocumentStatu>();
            this.HistRules = new List<HistRule>();
            this.Document1 = new List<Document>();
            this.ProcessDocuments = new List<ProcessDocument>();
            this.Audits = new List<Audit>();
            this.Rules = new List<Rule>();
            this.Procedures = new List<Procedure>();
            this.Procedures1 = new List<Procedure>();
            this.Procedures2 = new List<Procedure>();
            this.Processes = new List<Process>();
        }

        public int ID { get; set; }
        public Nullable<int> documentParentID { get; set; }
        public int documentTypeID { get; set; }
        public double EDT { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string documentText { get; set; }
        public string url { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public int version { get; set; }
        public int companyID { get; set; }
        public Nullable<int> statusID { get; set; }
        public Nullable<int> responsible { get; set; }
        public virtual ICollection<AuditProcess> AuditProcesses { get; set; }
        public virtual Company Company { get; set; }
        public virtual DocumentType DocumentType { get; set; }
        public virtual Position Position { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<DocumentStatu> DocumentStatus { get; set; }
        public virtual ICollection<HistRule> HistRules { get; set; }
        public virtual ICollection<Document> Document1 { get; set; }
        public virtual Document Document2 { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<ProcessDocument> ProcessDocuments { get; set; }
        public virtual ICollection<Audit> Audits { get; set; }
        public virtual ICollection<Rule> Rules { get; set; }
        public virtual ICollection<Procedure> Procedures { get; set; }
        public virtual ICollection<Procedure> Procedures1 { get; set; }
        public virtual ICollection<Procedure> Procedures2 { get; set; }
        public virtual ICollection<Process> Processes { get; set; }
    }
}
