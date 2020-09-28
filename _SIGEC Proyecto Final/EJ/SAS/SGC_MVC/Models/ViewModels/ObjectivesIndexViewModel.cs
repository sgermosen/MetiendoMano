using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGC_MVC.Models.ViewModels
{
    public class ObjectivesIndexViewModel
    {
        public int rule_idFilter { get; set; }
        public int status_idFilter { get; set; }
        public ICollection<Document> objectives { get; set; }
        public ICollection<Rule> rules { get; set; }
    }
}