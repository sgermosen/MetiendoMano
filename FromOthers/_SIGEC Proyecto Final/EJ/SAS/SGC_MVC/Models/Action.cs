using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Action
    {
        public Action()
        {
            this.DeashboardItems = new List<DeashboardItem>();
            this.Templates = new List<Template>();
            this.Views = new List<View>();
            this.Webpages_Roles = new List<Webpages_Roles>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int controllerID { get; set; }
        public bool isView { get; set; }
        public string displayName { get; set; }
        public virtual Controller Controller { get; set; }
        public virtual ICollection<DeashboardItem> DeashboardItems { get; set; }
        public virtual ICollection<Template> Templates { get; set; }
        public virtual ICollection<View> Views { get; set; }
        public virtual ICollection<Webpages_Roles> Webpages_Roles { get; set; }
    }
}
