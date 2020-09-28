using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGC_MVC.Models.ViewModels
{
    public class FormViewModel
    {
        public int ID { get; set; }
        public int ruleID { get; set; }
        public int processID { get; set; }
        [System.Web.Mvc.Remote("CheckUniqGeneral", "Home", AdditionalFields = "validateUniq")]
        public string name { get; set; }
        public int processTypeID { get; set; }
        public int[] selectedFormFields { get; set; }
        public ICollection<FormField> FormFields { get; set; }
        public string submitVal { get; set; }
        public int fieldId { get; set; }
        [AllowHtml]
        public string changeReason { get; set; }
        public FormViewModel() { }

        public FormViewModel(Form form)
        {
            this.ID = form.ID;
            this.ruleID = form.ruleID;
            this.processID = form.processID;
            this.name = form.name;
            this.FormFields = form.FormFields;
        }

        public void UpdateData(Form form)
        {
            form.ruleID = this.ruleID;
            form.name = this.name;
            form.processID = this.processID;
        }
    }
}