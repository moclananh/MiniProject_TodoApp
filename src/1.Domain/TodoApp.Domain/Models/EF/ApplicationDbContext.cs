using Microsoft.EntityFrameworkCore;
using System.Reflection;
using TodoApp.Domain.FluentAPIs;
using TodoApp.Domain.Models.Entities;

namespace TodoApp.Domain.Models.EF
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Todo> Todos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoConfig());
            modelBuilder.ApplyConfiguration(new UserConfig());
        }
    }
}
