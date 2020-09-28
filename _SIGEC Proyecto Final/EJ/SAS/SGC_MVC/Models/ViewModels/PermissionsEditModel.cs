using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SGC_MVC.Models.ViewModels
{
    public class PermissionsEditModel
    {
        [Display(Name="Rol")]
        public int RoleId { get; set; }
        public int [] actions { get; set; }
        public int[] departments { get; set; }
        public int[] statuses { get; set; }
    }

    public class PermissionsEditPartial
    {
        public Webpages_Roles Role { get; set; }
        public int [] SelectedActions { get; set; }
        public int[] SelectedDepartments { get; set; }
        public int[] SelectedStatuses { get; set; }
        public List<Action> actions { get; set; }
        public List<Department> departments { get; set; }
        public List<Status> statuses { get; set; }
        public List<Controller> controllers { get; set; }
    }
}