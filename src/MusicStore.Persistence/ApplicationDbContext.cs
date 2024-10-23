using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;

namespace MusicStore.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Fluent API -> permite Personalización de conversion de Clases a Tablas
            modelBuilder.Entity<Genre>().Property(x => x.Name).HasMaxLength(50);    // Se configura el valor maximo de NVarchar en la tabla
        }

        //Entities to Tables
        public DbSet<Genre> Genres { get; set; }
    }
}
