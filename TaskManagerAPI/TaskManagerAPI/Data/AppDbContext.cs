using Microsoft.EntityFrameworkCore;
using TaskManagerAPI.Entities;

namespace TaskManagerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserTask> UserTasks { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>(user =>
            {
                user.HasKey(u => u.UserGuid);
                user.HasAlternateKey(t => t.UserId);
                user.HasIndex(u => u.Email).IsUnique();
                user.HasIndex(u => u.UserId).IsUnique();
                user.Property(u => u.UserId).UseIdentityColumn(1001, 1);
                user.Property(u => u.Name).IsRequired().HasMaxLength(100);
                user.Property(u => u.Email).IsRequired().HasMaxLength(255);
                user.Property(u => u.PasswordHash).IsRequired().HasMaxLength(255);
                user.Property(u => u.IsActive).HasDefaultValue(true);
                user.Property(u => u.CreateDt).HasDefaultValue(DateTime.UtcNow);
                user.Property(u => u.IsVerified).HasDefaultValue(false);
            });

            modelBuilder.Entity<UserTask>(ut =>
            {
                ut.HasKey(t => t.UserTaskId);
                ut.Property(t => t.UserTaskId).UseIdentityColumn(101, 1);
                ut.Property(t => t.Status).HasDefaultValue("Pending");
                ut.Property(t => t.Name).IsRequired().HasMaxLength(200);
                ut.Property(t => t.Description).HasMaxLength(1000);
                ut.Property(t => t.DueDate).HasDefaultValueSql("GETUTCDATE()").IsRequired();
                ut.HasOne(t => t.User)
                    .WithMany(u => u.Tasks)
                    .HasForeignKey(t => t.UserId)
                    .HasPrincipalKey(u => u.UserId)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
