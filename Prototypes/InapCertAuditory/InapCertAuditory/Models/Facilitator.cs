using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InapCertAuditory.Models
{
    public class Facilitator
    {
        public int FacilitatorId { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public string Observations { get; set; }

        public string FullName => $"{Name} {LastName}";

        public ICollection<CourseSection> CourseSections { get; set; }
    }
}
