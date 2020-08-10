using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InapCertAuditory.Models
{
    public class Tanda
    {
        public int TandaId { get; set; }

        public string Name { get; set; }

        public ICollection<CourseSection> CourseSections { get; set; }
    }
}
