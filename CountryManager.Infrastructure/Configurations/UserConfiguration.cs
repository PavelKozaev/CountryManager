using CountryManager.Shared.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CountryManager.Infrastructure.Configurations
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.Property(e => e.UserName).HasMaxLength(50).IsRequired();
            builder.HasIndex(e => e.UserName).IsUnique();
            builder.Property(e => e.Email).HasMaxLength(320).IsRequired();
            builder.HasIndex(e => e.Email).IsUnique();

            builder.HasData(
                new User { Id = Guid.NewGuid(), UserName = "user1", RoleId = 1, Password = "123", Email="user1@mail.ru"},
                new User { Id = Guid.NewGuid(), UserName = "user2", RoleId = 2, Password = "123", Email="user2@mail.ru"}
                );
        }
    }
}
