using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistance.EntityConfigurations
{
    public class HouseholdConfiguration : IEntityTypeConfiguration<Household>
    {
        public void Configure(EntityTypeBuilder<Household> builder)
        {
            builder.ToTable("Household", "Household");

            builder.Property(p => p.Name)
                .HasMaxLength(50);

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.City)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.CreatingDate)
                .HasConversion<DateOnlyConverter>();
        }
    }
}
