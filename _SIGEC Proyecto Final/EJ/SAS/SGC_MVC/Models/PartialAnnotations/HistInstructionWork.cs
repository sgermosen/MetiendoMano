using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(HistInstructionWorkMetadata))]
    public partial class HistInstructionWork
    {
        public HistInstructionWork() { }
        public HistInstructionWork(InstructionWork instructionWork)
        {
            this.createUser = instructionWork.createUser;
            this.ruleID = instructionWork.ruleID;
            this.processID = instructionWork.processID;
            this.departmentID = instructionWork.departmentID;
            this.responsible = instructionWork.responsible;
            this.statusID = instructionWork.statusID;
            this.instructionWorkID = instructionWork.ID;
            this.name = instructionWork.name;
            this.version = instructionWork.version;
            this.companyID = instructionWork.companyID;
        }
    }

    public class HistInstructionWorkMetadata 
    {
        [Key]
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Nullable<System.DateTime> dateAdded { get; set; }
    }
}
