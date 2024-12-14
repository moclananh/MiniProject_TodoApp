using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using TodoApp.Domain.Models.Entities;

namespace TodoApp.Domain.FluentAPIs
{
    public class TodoConfig : IEntityTypeConfiguration<Todo>
    {
        public void Configure(EntityTypeBuilder<Todo> builder)
        {
            // Primary key
            builder.HasKey(t => t.Id);

            // Properties
            builder.Property(t => t.Id)
                   .IsRequired()
                   .ValueGeneratedOnAdd();

            builder.Property(t => t.Title)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(t => t.Description)
                   .HasMaxLength(1000); // Optional or default max length

            builder.Property(t => t.Status)
                       .IsRequired()
                       .HasConversion<string>() // Save enum as string in the database
                       .HasMaxLength(20);

            builder.Property(t => t.Priority)
                   .IsRequired();

            builder.Property(t => t.CreatedDate)
                   .IsRequired();

            builder.Property(t => t.EndDate)
                   .IsRequired();

            builder.Property(t => t.Star)
                   .IsRequired();

            builder.Property(t => t.IsActive)
                   .IsRequired();

            // Indexes
            builder.HasIndex(t => t.Title); // Add an index to improve search
        }
    }
}
