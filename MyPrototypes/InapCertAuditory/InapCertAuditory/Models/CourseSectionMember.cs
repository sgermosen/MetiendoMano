using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InapCertAuditory.Models
{
    public class CourseSectionMember
    {
        public int CourseSectionMemberId { get; set; }

        public CourseSection CourseSection { get; set; }

        public Participant  Participant { get; set; }

        public string Observations { get; set; }

        public string Punctuation { get; set; }
    }
}
