using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class UserTask
    {
        public UserTask()
        {
            this.UserTask1 = new List<UserTask>();
        }

        public int ID { get; set; }
        public Nullable<int> parentTaskID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int ruleID { get; set; }
        public Nullable<System.DateTime> fromDate { get; set; }
        public Nullable<System.DateTime> toDate { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public int statusID { get; set; }
        public int companyID { get; set; }
        public int responsible { get; set; }
        public virtual Company Company { get; set; }
        public virtual Rule Rule { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
        public virtual ICollection<UserTask> UserTask1 { get; set; }
        public virtual UserTask UserTask2 { get; set; }
    }
}
