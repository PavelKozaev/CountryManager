using CountryManager.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CountryManager.Infrastructure.Configurations
{
    internal class RegionConfiguration : IEntityTypeConfiguration<Region>
    {
        public void Configure(EntityTypeBuilder<Region> builder)
        {
            builder.ToTable("regions");

            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Code).HasMaxLength(3).IsRequired(false);
        }
    }
}
