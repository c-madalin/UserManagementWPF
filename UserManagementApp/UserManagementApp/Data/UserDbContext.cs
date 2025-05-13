using Microsoft.EntityFrameworkCore;
using UserManagementApp.Models;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace UserManagementApp.Data
{
    public class UserDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Get connection string from appsettings.json
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("PostgreSQLConnection");
            optionsBuilder.UseNpgsql(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure EF Core to use non-quoted identifiers
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");  // Use lowercase, unquoted table name

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Username).HasColumnName("username");
                entity.Property(e => e.Password).HasColumnName("password");
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}