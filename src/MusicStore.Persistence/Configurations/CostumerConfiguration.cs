
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Entities;

namespace MusicStore.Persistence.Configurations
{
    public class CostumerConfiguration : IEntityTypeConfiguration<Costumer>
    {
        public void Configure(EntityTypeBuilder<Costumer> builder)
        {
            builder.Property(x => x.Email)
                 .HasMaxLength(200)
                 .IsUnicode(false);

            builder.Property(x => x.FullName)
                .HasMaxLength(200);

            builder.ToTable(nameof(Costumer), "Musicales");
        }
    }
}
