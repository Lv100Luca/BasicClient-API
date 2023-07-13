using API_Client.Database;
using API_Client.Database.Entities;
using API_Client.Model.DTO;
using Microsoft.EntityFrameworkCore;

namespace API_Client.Model.services;

public class UserDbService
{
    private readonly DataContext _context;
    private readonly ILogger<UserDbService> _logger;


    public UserDbService(ILogger<UserDbService> logger, DataContext context)
    {
        this._logger = logger;
        this._context = context;
    }


    public List<User> GetAllUsers()
    {
        return _context.Users.Include(u => u.Roles).ToList();
    }


    public User? AddUser(UserDTO user)
    {
        var newUser = new User
        {
            Username = user.username,
            Password = user.password,
            FirstName = user.name,
            LastName = user.surname,
        };
        // List<Role> roles = new List<Role>();
        // foreach (var id in user.roles)
        // {
        //     var role = _context.GetRoleById(id);
        //     if (role is not null)
        //     {
        //         role.Users.Add(newUser);
        //         roles.Add(role);
        //     }
        // }
        // newUser.Roles = roles;

        newUser.Roles = user.roles
        .Select(id => _context.GetRoleById(id))
        .Where(role => role is not null)
        .Select(role =>
        {
            role.Users.Add(newUser); // seems to be no issue -> no roles/not existing roles get skipped
            return role;
        })
        .ToList();

        _context.Users.Add(newUser);
        _context.SaveChangesAsync();
        return newUser;
    }


    public Role? AddRole(RoleDTO role)
    {
        if (_context.Roles.Any(r => r.RoleName == role.RoleName)) // if Role already exists 
        {
            return null;
        }
        var newRole = new Role
        {
            RoleName = role.RoleName,
        };
        _context.Roles.Add(newRole);
        return newRole;
    }


    private User? CompleteRolesOfUser(User? user)
    {
        if (user == null)
        {
            Console.Out.WriteLine("User is null");
            return null;
        }
        user.Roles = _context.GetRolesWithUserId(user.Id); // works like this

        return user;
    }


    public User? GetUserById(int id)
    {
        return _context.Users.Include(u => u.Roles).FirstOrDefault<User>(u => u.Id == id);
    }


    public User? GetUserByName(string username)
    {
        return _context.Users.Include(u => u.Roles).FirstOrDefault<User>(u => u.Username == username);
    }
}
