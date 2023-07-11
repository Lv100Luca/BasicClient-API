using API_Client.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Client.Database;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }


    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();

        // modelBuilder.Entity<User>() // ?
        // .HasMany(e => e.Roles)
        // .WithMany(e => e.Users).UsingEntity(
        //     "UserRole",
        //     l => l.HasOne(typeof(Role)).WithMany().HasForeignKey("RoleId").HasPrincipalKey(nameof(Role.Id)),
        //     r => r.HasOne(typeof(User)).WithMany().HasForeignKey("UserId").HasPrincipalKey(nameof(User.Id)),
        //     j => j.HasKey("UserId", "RoleId"));

        modelBuilder.Entity<User>() // Many-to-many with class for join entity
        .HasMany(e => e.Roles)
        .WithMany(e => e.Users)
        .UsingEntity<UserRole>(
            l => l.HasOne<Role>().WithMany().HasForeignKey(e => e.RoleId),
            r => r.HasOne<User>().WithMany().HasForeignKey(e => e.UserId));
    }


    public Role? GetRoleById(int id)
    {
        return Roles.FirstOrDefault(r => r.Id == id);
    }
}
