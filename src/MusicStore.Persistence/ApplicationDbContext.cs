using Microsoft.EntityFrameworkCore;
using MusicStore.Entities;
using MusicStore.Entities.info;
using System.Reflection;

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
            //modelBuilder.Entity<Genre>().Property(x => x.Name).HasMaxLength(50);    // Se configura el valor maximo de NVarchar en la tabla

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly()); //Se asegura de que se apliquen todas las congiguraciones del emsamblado actual
            modelBuilder.Ignore<ConcertInfo>();
            modelBuilder.Entity<ConcertInfo>().HasNoKey();
        }

        //Entities to Tables
        //public DbSet<Genre> Genres { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseLazyLoadingProxies();
            }
        }
    }
}
