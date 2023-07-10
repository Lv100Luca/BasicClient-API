using API_Client.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Client.Database;

public class UserDbContext : DbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options) { }


    public Microsoft.EntityFrameworkCore.DbSet<Role> Roles { get; set; }
    public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
        modelBuilder.Entity<User>()
        .HasMany(e => e.Roles)
        .WithMany(e => e.Users);
    }


    public Role? GetRoleById(int id)
    {
        return Roles.FirstOrDefault(r => r.Id == id);
    }
}
