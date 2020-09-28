using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(HistProcedureMetadata))]
    public partial class HistProcedure
    {

        [NotMapped]
        SASContext db = new SASContext();

        public HistProcedure(Procedure procedure) 
        {
            this.procedureID = procedure.ID;
            this.ruleID = procedure.ruleID;
            this.processID = procedure.processID;
            this.responsible = procedure.responsible;
            this.target = procedure.target;
            this.procedureScope = procedure.procedureScope;
            this.name = procedure.name;
            this.statusID = procedure.statusID;
            this.version = procedure.version;
            this.companyID = procedure.companyID;
            this.createUser = procedure.createUser;
        }

        public HistProcedure() { }

        [NotMapped]
        [DisplayName("Creado por")]
        public virtual User CreatedBy
        {
            get
            {
                return db.Users.Find(createUser);
            }
        }

        [NotMapped]
        [DisplayName("Responsable")]
        public virtual Position Responsible
        {
            get
            {
                return db.Positions.Find(responsible);
            }
        }
    }

    class HistProcedureMetadata 
    {
        [Key]
        public int ID { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayName("Fecha Promocion")]
        public Nullable<System.DateTime> dateAdd { get; set; }
        [DisplayName("Nombre")]
        public string name { get; set; }
        [DisplayName("Version")]
        public int version { get; set; }
        [DisplayName("Creado por")]
        public Nullable<int> createUser { get; set; }
    }
}
