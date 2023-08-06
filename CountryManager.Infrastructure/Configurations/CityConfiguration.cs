using CountryManager.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CountryManager.Infrastructure.Configurations
{
    internal class CityConfiguration : IEntityTypeConfiguration<City>
    {
        public void Configure(EntityTypeBuilder<City> builder)
        {
            builder.ToTable("cities");

            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
        }
    }
}
