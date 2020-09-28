using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SGC_MVC.Models.ViewModels
{
    public class PlanViewModel
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Código")]
        [Required]
        public string code { get; set; }
        [Display(Name = "Nombre")]
        [Required]
        public string name { get; set; }
        [Display(Name = "Responsable")]
        [Required]
        public int responsible { get; set; }
        [Display(Name = "Objetivos")]
        [Required]
        public string planObjetives { get; set; }
        [Display(Name = "Responsables")]
        [Required]
        public string responsibles { get; set; }
        [Display(Name = "Acciones")]
        [Required]
        public string actions { get; set; }
        [DisplayName("Proceso relacionado")]
        [Required]
        public int processID { get; set; }
        [Display(Name = "Fecha Inicio")]
        [Required]
        public Nullable<System.DateTime> startDate { get; set; }
        [Display(Name = "Fecha Fin")]
        [Required]
        public Nullable<System.DateTime> endDate { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        [Display(Name = "Ultima Modificación")]
        public Nullable<System.DateTime> updateDate { get; set; }
        public int companyID { get; set; }
        public Nullable<int> createUser { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<PlanResource> PlanResources { get; set; }
        [DisplayName("Objetivos")]
        public virtual ICollection<PlanObjective> PlanObjectives { get; set; }
        [Display(Name = "Posición")]
        public virtual Position Position { get; set; }
        public virtual User User { get; set; }
        public string submitVal { get; set; }
        public PlanViewModel()
        {

        }

        public void setValues(Plan plan){
            plan.code = this.code;
            plan.createUser = this.createUser;
            plan.name = this.name;
            plan.responsible = this.responsible;
            plan.createDate = this.createDate;
            plan.processID = this.processID;
            plan.PlanObjectives = this.PlanObjectives;
        }

        public void getValues(int ID)
        {
            Plan thisPlan =  new SASContext().Plans.Find(ID);
            if (thisPlan == null)
            {
                return;
            }
            this.ID = thisPlan.ID;
            this.code = thisPlan.code;
            this.companyID = thisPlan.companyID;
            this.createUser = thisPlan.createUser;
            this.name = thisPlan.name;
            this.responsible = thisPlan.responsible;
            this.processID = thisPlan.processID;
            this.PlanObjectives = thisPlan.PlanObjectives;
        }
    }
}
