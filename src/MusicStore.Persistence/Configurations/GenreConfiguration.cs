using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Entities;

namespace MusicStore.Persistence.Configurations
{
    public class GenreConfiguration : IEntityTypeConfiguration<Genre>
    {
        public void Configure(EntityTypeBuilder<Genre> builder)
        {
            builder.Property(x => x.Name).HasMaxLength(50); // Se personaliza el valor maximo de NVarchar en la tabla
            builder.ToTable("Genre", "Musicales");  //Personalización de nombre de la tabla y se añade el schema Musicales
        }
    }
}
