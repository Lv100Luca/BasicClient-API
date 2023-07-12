using API_Client.Database.Entities;

namespace API_Client.Model.services;

public class UserService
{
    private readonly ILogger<UserService> _logger;
    private List<RoleEntity> Roles = new List<RoleEntity>();


    private List<UserEntity> Users = new List<UserEntity>();


    public UserService(ILogger<UserService> logger)
    {
        this._logger = logger;
    }


    public List<UserEntity> GetAllUsers()
    {
        return Users;
    }


    public List<RoleEntity> GetAllRoles()
    {
        return Roles;
    }
}
