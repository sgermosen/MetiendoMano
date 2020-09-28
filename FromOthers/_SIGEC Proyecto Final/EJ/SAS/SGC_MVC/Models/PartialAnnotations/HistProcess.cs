using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(HistProcessMetadata))]
    public partial class HistProcess
    {


        public HistProcess() { }
        public HistProcess(Process p)
        {
            this.processID = p.ID;
            this.ruleID = p.ruleID;
            this.processTypeID = p.processTypeID;
            this.responsible = p.responsible;
            this.name = p.name;
            this.description = p.description;
            this.statusID = p.statusID;
            this.target = p.target;
            this.entries = p.entries;
            this.activities = p.activities;
            this.outputs = p.outputs;
            this.customerRequirements = p.customerRequirements;
            this.controlMeasures = p.controlMeasures;
            this.outputRequirements = p.outputRequirements;
            this.createUser = p.createUser;
            this.companyID = p.companyID;
            this.version = p.version;
        }
    }

    class HistProcessMetadata
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayName("Fecha promoción")]
        public Nullable<System.DateTime> dateAdded { get; set; }
        [DisplayName("Nombre")]
        public string name { get; set; }
        [DisplayName("Versión")]
        public int version { get; set; }
        [DisplayName("Creado por")]
        public Nullable<int> createUser { get; set; }

    }
}
