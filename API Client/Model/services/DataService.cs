using API_Client.Database;
using API_Client.Database.Entities;
using API_Client.Model.DTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API_Client.Model.services;

public class DataService
{
    private readonly DataContext _context;
    private readonly ILogger<DataService> _logger;
    private readonly IPasswordHasher<User> _passwordHasher;


    public DataService(ILogger<DataService> logger, DataContext context, IPasswordHasher<User> passwordHasher)
    {
        this._logger = logger;
        this._context = context;
        _passwordHasher = passwordHasher;
    }


    public Task<List<User>> GetAllUsers()
    {
        return _context.Users.Include(u => u.Roles).ToListAsync();
    }


    public User? AddUser(UserDto user)
    {
        var newUser = new User // todo remove temporary display of unhashed password
        {
            Username = user.username,
            FirstName = user.name,
            LastName = user.password,
        };
        newUser.Password = _passwordHasher.HashPassword(newUser, user.password);
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

        AddRolesToUser(newUser, user.roles);
        _context.Users.Add(newUser);
        _context.SaveChanges();
        return newUser;
    }


    private void AddRolesToUser(User user, int[] roles)
    {
        if (roles != null)
        {
            user.Roles = roles
            .Select(GetRoleById)
            .Where(role => role is not null)
            .Select(role =>
            {
                role!.Users.Add(user); // seems to be no issue -> no roles/not existing roles get skipped because of .Where
                return role;
            })
            .ToList();
        }
    }


    public bool DeleteUser(int id)
    {
        var user = _context.Users.Find(id);
        if (user is null)
        {
            return false;
        }
        _context.Users.Remove(user);
        _context.SaveChanges();
        return true;
    }


    public bool ChangeUserInformation(UserDto userDto)
    {
        User? user = GetUserById(userDto.id);
        if (user is null)
        {
            return false;
        }
        user.Username = userDto.username;
        user.FirstName = userDto.name;
        user.LastName = userDto.surname;
        user.Roles = new List<Role>();
        AddRolesToUser(user, userDto.roles);
        _context.SaveChanges();
        return true;
    }


    public bool ChangePassword(UserDto userDto)
    {
        User? user = GetUserById(userDto.id);
        if (user is null)
        {
            return false;
        }
        user.Password = _passwordHasher.HashPassword(user, userDto.password);
        _context.SaveChanges();
        return true;
    }


    public bool VerifyPassword(User user, string password)
    {
        return _passwordHasher.VerifyHashedPassword(user, user.Password, password) == PasswordVerificationResult.Success;
    }


    public Role? AddRole(RoleDto role)
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
        _context.SaveChanges();
        return newRole;
    }


    public User? GetUserById(int id)
    {
        return _context.Users.Include(u => u.Roles).FirstOrDefault<User>(u => u.Id == id);
    }


    public User? GetUserByName(string username)
    {
        return _context.Users.Include(u => u.Roles).FirstOrDefault<User>(u => u.Username == username);
    }


    public Role? GetRoleById(int id)
    {
        return _context.Roles.FirstOrDefault(r => r.Id == id) ?? null;
    }


    public User? Authenticate(LoginDto userLogin)
    {
        var user = GetUserByName(userLogin.Username);
        if (user is null)
        {
            return null;
        }
        var success = VerifyPassword(user, userLogin.Password);
        return success ? user : null;
    }
}
