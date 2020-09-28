using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Menu
    {
        public Menu()
        {
            this.SubMenus = new List<SubMenu>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public int noOrder { get; set; }
        public int createUser { get; set; }
        public int companyID { get; set; }
        public string description { get; set; }
        public virtual Company Company { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<SubMenu> SubMenus { get; set; }
    }
}
