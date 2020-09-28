using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class Question
    {
        public Question()
        {
            this.Options = new List<Option>();
        }

        public int ID { get; set; }
        public int pollID { get; set; }
        public int noOrder { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public int questionTypeID { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        public Nullable<System.DateTime> updateDate { get; set; }
        public Nullable<int> createUser { get; set; }
        public virtual ICollection<Option> Options { get; set; }
        public virtual Poll Poll { get; set; }
        public virtual User User { get; set; }
        public virtual QuestionType QuestionType { get; set; }
    }
}
