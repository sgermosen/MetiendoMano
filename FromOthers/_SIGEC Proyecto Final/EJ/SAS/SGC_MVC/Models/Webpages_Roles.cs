using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Webpages_Roles
    {
        public Webpages_Roles()
        {
            this.Actions = new List<Action>();
            this.Departments = new List<Department>();
            this.Status = new List<Status>();
            this.Users = new List<User>();
        }

        public int RoleID { get; set; }
        public string RoleName { get; set; }
        public string RoleDescription { get; set; }
        public virtual ICollection<Action> Actions { get; set; }
        public virtual ICollection<Department> Departments { get; set; }
        public virtual ICollection<Status> Status { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
