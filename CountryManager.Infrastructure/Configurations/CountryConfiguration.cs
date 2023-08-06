using CountryManager.Shared.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CountryManager.Infrastructure.Configurations
{
    internal class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("countries");

            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.HasIndex(e => e.Name).IsUnique();
            builder.Property(e => e.CountryCode).HasMaxLength(2).IsRequired();
            builder.HasIndex(e => e.CountryCode).IsUnique();
        }
    }
}
