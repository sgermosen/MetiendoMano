using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InapCertAuditory.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        public string Name { get; set; }

        public string Duration { get; set; }

        public string Observation { get; set; }
    }
}
