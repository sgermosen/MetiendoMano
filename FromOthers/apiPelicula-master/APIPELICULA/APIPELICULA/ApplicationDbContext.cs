using APIPELICULA.Entidades;
using Microsoft.EntityFrameworkCore;

namespace APIPELICULA
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)    
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculasActores>()
                .HasKey(x => new {x.ActorId, x.PeliculaId});
            modelBuilder.Entity<PeliclasGeneros>()
                .HasKey(x => new {x.GeneroId, x.peliculaId});
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Genero> Generos { get; set; }    
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        
        public DbSet<PeliculasActores> PeliculasActoreses  { get; set; }
        
        public DbSet<PeliclasGeneros> PeliclasGeneroses { get; set; }
        
    }
}