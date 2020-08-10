using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InapCertAuditory.Models;

namespace InapCertAuditory.Models
{
    public class DataContext : DbContext
    {
        public DataContext (DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<InapCertAuditory.Models.Configuration> Configuration { get; set; }

        public DbSet<InapCertAuditory.Models.Course> Course { get; set; }

        public DbSet<InapCertAuditory.Models.CourseSection> CourseSection { get; set; }

        public DbSet<InapCertAuditory.Models.CourseSectionMember> CourseSectionMember { get; set; }

        public DbSet<InapCertAuditory.Models.Enterprice> Enterprice { get; set; }

        public DbSet<InapCertAuditory.Models.Facilitator> Facilitator { get; set; }

        public DbSet<InapCertAuditory.Models.Participant> Participant { get; set; }

        public DbSet<InapCertAuditory.Models.Place> Place { get; set; }

        public DbSet<InapCertAuditory.Models.Tanda> Tanda { get; set; }
    }
}
