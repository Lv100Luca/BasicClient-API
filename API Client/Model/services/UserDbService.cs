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
        // var tmpUser = new UserEntity(user);
        var tmpUser = new UserEntity
        {
            Username = user.username,
            Password = user.password,
            Name = user.name,
            Surname = user.surname
        };
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


    private UserEntity? CompleteRolesOfUser(UserEntity? user)
    {
        if (user == null)
        {
            Console.Out.WriteLine("User is null");
            return null;
        }
        user.Roles = _context.GetRolesWithUserId(user.Id); // works like this

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
}
