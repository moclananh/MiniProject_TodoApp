

using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Models.Entities;

namespace TodoApp.Domain.FluentAPIs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            // Primary key
            builder.HasKey(u => u.Id);

            // Properties
            builder.Property(u => u.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(u => u.UserName)
                   .IsRequired()
                   .HasMaxLength(50);

            builder.Property(u => u.Email)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(u => u.Password)
                   .IsRequired()
                   .HasMaxLength(255);

            // Indexes
            builder.HasIndex(u => u.Email)
                   .IsUnique();

            builder.HasIndex(u => u.UserName)
                   .IsUnique();
        }
    }
}
