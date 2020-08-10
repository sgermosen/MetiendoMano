using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Contactes.Web.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Contactes.Web.Models
{
    public class DataContex : IdentityDbContext<AppUser>
    {
        public DataContex(DbContextOptions<DataContex> options)
            : base(options)
        {
        }

        public DbSet<Persona>  Personas { get; set; }

        public DbSet<Tipo> Tipos { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Persona>().HasKey(p => new { p.Identificador });

            base.OnModelCreating(builder);
           
        }
    }
}
