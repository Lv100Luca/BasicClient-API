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

        List<Role> roles = user.roles
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
            Id = 1,
            RoleName = "admin",
        };
        Role role2 = new Role
        {
            Id = 2,
            RoleName = "user",
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


    public User GetUserById(int id)
    {
        var user = _context.Users.FirstOrDefault(u => u.Id == id)!;
        Console.Out.WriteLine("========================================================================================");
        _context.UserRoles.Where(ur => ur.UserId == id).ToList().ForEach(u => Console.Out.WriteLine(_context.GetRoleById(u.RoleId).RoleName));

        var rolesInt = new List<int>();

        Console.Out.WriteLine(rolesInt.Count);
        List<Role> roles = new List<Role>();
        foreach (var i in rolesInt)
        {
            var role = _context.GetRoleById(i);
            Console.Out.WriteLine("ROLE: " + role);
            if (role is not null)
            {
                Console.Out.WriteLine($"Added Role {role.RoleName} to User {user.Username}");
                roles.Add(role);
            }
        }

        user.Roles = roles;
        return user;
    }
}
