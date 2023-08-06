using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CountryManager.Shared.Models;

namespace CountryManager.Infrastructure.Configurations
{
    internal class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("roles");

            builder.Property(e => e.Code).HasMaxLength(10).IsRequired();
            builder.HasIndex(e => e.Code).IsUnique();
            builder.Property(e => e.Description).HasMaxLength(255).IsRequired(false);

            builder.HasData(
                new Role { Id = 1, Code = "admin", Description = "read, add, update, delete" },
                new Role { Id = 2, Code = "guest", Description = "read" }
                );
        }
    }
}
