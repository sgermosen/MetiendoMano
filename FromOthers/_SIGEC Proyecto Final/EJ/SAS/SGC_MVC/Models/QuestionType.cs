using System;
using System.Collections.Generic;

namespace SGC_MVC.Models
{
    public partial class QuestionType
    {
        public QuestionType()
        {
            this.Questions = new List<Question>();
        }

        public int ID { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
