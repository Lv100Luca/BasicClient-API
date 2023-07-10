using Microsoft.EntityFrameworkCore;

namespace API_Client.Database;

public class UserDataContext : DbContext
{
    public UserDataContext(DbContextOptions<UserDataContext> options) : base(options) { }


    public DbSet<Entities.Role> Roles { get; set; }
    public DbSet<Entities.User> Users { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.UseSerialColumns();
    }
}
