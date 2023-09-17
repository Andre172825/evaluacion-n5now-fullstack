using Microsoft.EntityFrameworkCore;
using n5now_api.Domain.Models;
using System.Security;

namespace n5now_api.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Permission> Permissions { get; set; }
        public DbSet<PermissionType> PermissionTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Permission>()
                .HasOne(p => p.PermissionType)
                .WithMany(pt => pt.Permissions)
                .HasForeignKey(p => p.PermissionTypeId)
                .HasPrincipalKey(pt => pt.Id);

            modelBuilder.Entity<PermissionType>()
                .HasMany(pt => pt.Permissions)
                .WithOne(p => p.PermissionType)
                .HasForeignKey(p => p.PermissionTypeId)
                .HasPrincipalKey(pt => pt.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
