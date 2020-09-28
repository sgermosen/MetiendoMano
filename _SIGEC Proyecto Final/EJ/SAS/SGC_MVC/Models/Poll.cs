using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Poll
    {
        public Poll()
        {
            this.HistPolls = new List<HistPoll>();
            this.Questions = new List<Question>();
        }

        public int ID { get; set; }
        public int ruleID { get; set; }
        public string code { get; set; }
        public string name { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public int companyID { get; set; }
        public Nullable<int> createUser { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<HistPoll> HistPolls { get; set; }
        public virtual Rule Rule { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
