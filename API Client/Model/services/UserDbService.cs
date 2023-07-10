using API_Client.Database;
using API_Client.Database.Entities;
using API_Client.Model.DTO;
using User = API_Client.Database.Entities.User;

namespace API_Client.Model.services;

public class UserDbService
{
    private readonly UserDbContext _context;
    private readonly ILogger<UserDbService> _logger;


    public UserDbService(ILogger<UserDbService> logger, UserDbContext context)
    {
        this._logger = logger;
        this._context = context;
    }


    public List<User> GetAllUsers()
    {
        return _context.Users.ToList();
    }


    public void AddUser(User2 user)
    {
        var tmpUser = new User
        {
            Username = user.username,
            Password = user.password,
            Name = user.name,
            Surname = user.surname
        };

        ICollection<Role> roles = user.roles
        .Select(roleId => _context.GetRoleById(roleId))
        .Where(role => role != null)
        .ToList()!;

        tmpUser.Roles = roles;

        Console.Out.WriteLine(tmpUser);
        _context.Users.Add(tmpUser);
        _context.SaveChanges();
    }


    public void AddTmpUser()
    {
        var user = new User
        {
            Username = "john_doe",
            Password = "password123",
            Name = "John",
            Surname = "Doe"
        };

        Role role1 = new Role
        {
            RoleName = "admin",
        };
        Role role2 = new Role
        {
            RoleName = "asdf",
        };

        user.Roles = new List<Role>
        {
            role1,
            role2
        };
        Console.Out.WriteLine(user);
        _context.Users.Add(user);
        _context.SaveChanges();
    }


    public bool AddRole(Database.Entities.Role role)
    {
        var entity = _context.Roles.Add(role);
        Console.Out.WriteLine(entity.Entity);
        return entity != null;
    }
}
