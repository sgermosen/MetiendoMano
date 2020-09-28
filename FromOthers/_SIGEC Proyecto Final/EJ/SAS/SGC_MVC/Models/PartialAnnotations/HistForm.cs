using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    public partial class HistForm
    {
        //public int ID { get; set; }
        //public int FormID { get; set; }
        //public int ruleID { get; set; }
        //public int processID { get; set; }
        //public string name { get; set; }
        //public Nullable<System.DateTime> dateAdded { get; set; }
        //public int version { get; set; }
        //public Nullable<int> createUser { get; set; }
        //public int companyID { get; set; }
        //public int processTypeID { get; set; }
        //public string changeReason { get; set; }
        //public int statusID { get; set; }

        public HistForm() { }

        public HistForm(Form form)
        {
            this.FormID = form.ID;
            this.name = form.name;
            this.ruleID = form.ruleID;
            this.processID = form.processID;
            this.version = form.version;
            this.createUser = form.createUser;
            this.companyID = form.companyID;
            this.processTypeID = form.processTypeID;
            this.statusID = form.statusID;
        }

    }

    class HistFormMetadata
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> dateAdded { get; set; }
    }
}
