using API_Client.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Client.Database;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options) { }


    public DbSet<Role> Roles { get; set; }
    public DbSet<User> Users { get; set; }


    public Role? GetRoleById(int id)
    {
        return Roles.FirstOrDefault(r => r.Id == id);
    }


    public List<Role> GetRolesWithUserId(int id)
    {
        return Roles.Where(r => r.Users.Any(u => u.Id == id)).ToList();
    }
}
