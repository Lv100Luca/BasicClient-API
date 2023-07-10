using API_Client.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Client.Database;

public class UserDataContext : DbContext
{
    public UserDataContext(DbContextOptions<UserDataContext> options) : base(options) { }


    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }


    public Role? GetRoleById(int id)
    {
        return Roles.FirstOrDefault(r => r.Id == id);
    }
}
