using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Infrastructure.Persistance.EntityConfigurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User", "Administration");

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(u => u.PasswordHash)
                .IsRequired();

            builder.Property(u => u.PasswordSalt)
                .IsRequired();

            builder.Property(u => u.IsActive)
                .IsRequired();

            builder.HasMany(u => u.Permissions)
                .WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserPermission", 
                    j => j.HasOne<Permission>()
                            .WithMany()
                            .HasForeignKey("PermissionId"), 
                    j => j.HasOne<User>()
                            .WithMany()
                            .HasForeignKey("UserId"),
                    j =>
                    {
                        j.ToTable("UserPermission", "Administration");
                        j.HasKey("UserId", "PermissionId");
                    });
        }
    }
}
