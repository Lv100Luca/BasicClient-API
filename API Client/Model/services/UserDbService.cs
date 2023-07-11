using API_Client.Database.Entities;
using API_Client.Model.DTO;
using API_Client.Model.Inteface;

namespace API_Client.Model.services;

public class UserDbService
{
    private readonly IUserDbContext _context;
    private readonly ILogger<UserDbService> _logger;


    public UserDbService(ILogger<UserDbService> logger, IUserDbContext context)
    {
        this._logger = logger;
        this._context = context;
    }


    public List<UserEntity> GetAllUsers()
    {
        return _context.Users.ToList();
    }


    public int AddUser(UserDTO user)
    {
        // var tmpUser = new UserEntity(user);
        var tmpUser = new UserEntity
        {
            Username = user.username,
            Password = user.password,
            Name = user.name,
            Surname = user.surname
        };
        var userRoles = new List<RoleEntity>();
        foreach (var id in user.roles)
        {
            var role = GetRoleById(id);
            if (role is not null)
            {
                userRoles.Add(role);
            }
        }

        tmpUser.Roles = userRoles; // works like this

        Console.Out.WriteLine(tmpUser);
        _context.Users.Add(tmpUser);
        _context.SaveChanges();
        return tmpUser.Id;
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
    // old
    // public UserEntity? GetUserById(int id)
    // {
    //     Console.Out.WriteLine("Started Get User");
    //     var user = _context.Users.FirstOrDefault(u => u.Id == id); // select User by Id
    //
    //     if (user == null)
    //     {
    //         Console.Out.WriteLine("User is null");
    //         return null;
    //     }
    //     user.Roles = _context.GetRolesWithUserId(user.Id); // works like this
    //     return user;
    // }
    //
    // public UserEntity? GetUserByName(string username)
    // {
    //     Console.Out.WriteLine("Started Get User");
    //     var user = _context.Users.FirstOrDefault(u => u.Username == username); // select User by Id
    //
    //     if (user == null)
    //     {
    //         Console.Out.WriteLine("User is null");
    //         return null;
    //     }
    //     user.Roles = _context.GetRolesWithUserId(user.Id); // works like this
    //     return user;
    // }


    private UserEntity? CompleteRolesOfUser(UserEntity? user)
    {
        if (user == null)
        {
            Console.Out.WriteLine("User is null");
            return null;
        }
        user.Roles = GetRolesWithUserId(user.Id); // works like this

        return user;
    }


    public UserEntity? GetUserById(int id)
    {
        return CompleteRolesOfUser(_context.Users.FirstOrDefault(u => u.Id == id));
    }


    public UserEntity? GetUserByName(string username)
    {
        return CompleteRolesOfUser(_context.Users.FirstOrDefault(u => u.Username == username));
    }


    public RoleEntity? GetRoleById(int id) // todo move to service
    {
        return _context.Roles.FirstOrDefault(r => r.Id == id);
    }


    public List<RoleEntity> GetRolesWithUserId(int id) // todo move to service
    {
        return _context.Roles.Where(r => r.Users.Any(u => u.Id == id)).ToList();
    }
}
