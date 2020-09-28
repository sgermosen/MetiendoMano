using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SGC_MVC.Models
{
    [MetadataType(typeof(HistPlanMetadata))]
    public partial class HistPlan
    {
        [NotMapped]
        SASContext db = new SASContext();

        public HistPlan() { }

        public HistPlan(Plan plan)
        {
            this.code = plan.code;
            this.name = plan.name;
            this.planID = plan.ID;
            this.responsible = plan.responsible;
            this.createUser = plan.createUser;
            this.version = plan.version;
            this.companyID = plan.companyID;
        }

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

    class HistPlanMetadata
    {
        [Key]
        public int ID { get; set; }
        [DisplayName("Plan")]
        public int planID { get; set; }
        [DisplayName("Codigo")]
        public string code { get; set; }
        [DisplayName("Nombre")]
        public string name { get; set; }
        [DisplayName("Responsable")]
        public int responsible { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [DisplayName("Fecha Promocion")]
        public Nullable<System.DateTime> dateAdded { get; set; }
        public int companyID { get; set; }
        [DisplayName("Creado Por")]
        public Nullable<int> createUser { get; set; }
        [DisplayName("Proceso Relacionado")]
        public int processID { get; set; }
        [DisplayName("Versión")]
        public int version { get; set; }
        public string changeReason { get; set; }
    }
}
