using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InapCertAuditory.Models
{
    public class CourseSection
    {
        public int CourseSectionId { get; set; }

        public string Code { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public string Observations { get; set; }

        public Place Place { get; set; }

        public Tanda Tanda { get; set; }

        public Facilitator   Facilitator { get; set; }

    }
}
