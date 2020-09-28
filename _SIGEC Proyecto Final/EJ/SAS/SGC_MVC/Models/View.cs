using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class View
    {
        public View()
        {
            this.SubMenus = new List<SubMenu>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public int actionID { get; set; }
        public string description { get; set; }
        public virtual Action Action { get; set; }
        public virtual ICollection<SubMenu> SubMenus { get; set; }
    }
}
