using API_Client.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace API_Client.Model.Inteface;

public interface IUserDbContext
{
    DbSet<RoleEntity> Roles { get; set; }
    DbSet<UserEntity> Users { get; set; }
    DbSet<UserRoleEntity> UserRoles { get; set; }


    int SaveChanges();
}
