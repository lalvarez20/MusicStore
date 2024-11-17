using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicStore.Entities;

namespace MusicStore.Persistence.Configurations
{
    public class SaleConfiguration : IEntityTypeConfiguration<Sale>
    {
        void IEntityTypeConfiguration<Sale>.Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.Property(x => x.OperationNumber)
                 .HasMaxLength(200)
                 .IsUnicode(false);

            builder.Property(x => x.SaleDate)
                .HasColumnType("date")
                .HasDefaultValueSql("GETDATE()");

            builder.Property(x => x.Total)
                .HasColumnType("decimal(10,2)");

            builder.ToTable(nameof(Sale), schema : "Musicales");

        }
    }
}
