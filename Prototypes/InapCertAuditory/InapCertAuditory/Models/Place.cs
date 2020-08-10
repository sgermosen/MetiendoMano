using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InapCertAuditory.Models
{
    public class Place
    {
        public int PlaceId { get; set; }

        public string Name { get; set; }

        public string Observations { get; set; }

        public ICollection<CourseSection> CourseSections { get; set; }
    }
}
