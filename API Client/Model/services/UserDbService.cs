using API_Client.Database;
using API_Client.Database.Entities;
using API_Client.Model.DTO;

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


    public List<UserEntity> GetAllUsers()
    {
        return _context.Users.ToList();
    }


    public void AddUser(UserDTO user)
    {
        var tmpUser = new UserEntity(user);
        tmpUser.Roles = _context.GetRolesWithUserId(tmpUser.Id); // works like this

        Console.Out.WriteLine(tmpUser);
        _context.Users.Add(tmpUser);
        _context.SaveChanges();
    }


    public bool AddRole(RoleDTO role)
    {
        if (_context.Roles.Any(r => r.RoleName == role.RoleName)) // if Role already exists 
        {
            return false;
        }
        try
        {
            _context.Roles.Add(new RoleEntity
            {
                RoleName = role.RoleName
            });
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return false;
        }
    }


    public UserEntity? GetUserById(int id)
    {
        Console.Out.WriteLine("Started Get User");
        var user = _context.Users.FirstOrDefault(u => u.Id == id); // select User by Id

        if (user == null)
        {
            Console.Out.WriteLine("User is null");
            return null;
        }
        // var roles = new List<RoleEntity>();
        // foreach (var role in _context.GetRolesWithUserId(user.Id))
        // {
        //     roles.Add(role);
        // }
        //
        // user.Roles = roles;

        user.Roles = _context.GetRolesWithUserId(user.Id); // works like this
        return user;
    }


    // public void AddTmpUser()
    // {
    //     var user = new UserEntity
    //     {
    //         Username = "john_doe",
    //         Password = "password123",
    //         Name = "John",
    //         Surname = "Doe"
    //     };
    //
    //     RoleEntity role1 = new RoleEntity
    //     {
    //         Id = 1,
    //         RoleName = "admin",
    //     };
    //     RoleEntity role2 = new RoleEntity
    //     {
    //         Id = 2,
    //         RoleName = "user",
    //     };
    //
    //     user.Roles = new List<RoleEntity>
    //     {
    //         role1,
    //         role2
    //     };
    //     Console.Out.WriteLine(user);
    //     _context.Users.Add(user);
    //     _context.SaveChanges();
    // }
}
