using API_Client.Database;
using API_Client.Database.Entities;
using API_Client.Model.DTO;
using API_Client.Model.Inteface;
using API_Client.Model.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace ApiClient_Tests;

public class FakeDbContext : IUserDbContext
{
    public DbSet<RoleEntity> Roles { get; set; }
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<UserRoleEntity> UserRoles { get; set; }


    public int SaveChanges()
    {
        return 1;
    }
}
public class Tests // ask how to properly test on DB
{
    // private readonly static UserDbContext _context = new UserDbContext(new DbContextOptionsBuilder<UserDbContext>().UseInMemoryDatabase(databaseName: "Memory").Options);
    private readonly static FakeDbContext _context = new FakeDbContext();
    private readonly static UserDbService _db = new UserDbService(new Mock<ILogger<UserDbService>>().Object, _context);


    public UserDbContext GetMemoryContext()
    {
        var options = new DbContextOptionsBuilder<UserDbContext>().UseInMemoryDatabase(databaseName: "Memory").Options;
        return new UserDbContext(options);
    }


    [SetUp]
    public void Setup() // test
    {
        Console.Out.WriteLine("Stared Setup");

        _db.AddRole(new RoleDTO("admin")); // 1
        _db.AddRole(new RoleDTO("user")); // 2
        _db.AddRole(new RoleDTO("supporter")); // 3


        _db.AddUser(new UserDTO("Luca",
            "admin123",
            "Luca",
            "Diegel",
            new[]
            {
                1,
                3,
            }));
        _db.AddUser(new UserDTO("User",
            "user123",
            "User",
            "Diegel",
            new[]
            {
                2
            }));
        _db.AddUser(new UserDTO("Supporter",
            "supporter123",
            "Supporter",
            "Diegel",
            new[]
            {
                2,
                3
            }));
    }


    [Test]
    public void ValidateSetup()
    {
        Console.Out.WriteLine("Test");
        var user = new UserEntity
        {
            Id = 1,
            Username = "Luca",
            Password = "admin123",
            Name = "Luca",
            Surname = "Diegel",
            Roles = new List<RoleEntity>
            {
                _db.GetRoleById(1)!,
                _db.GetRoleById(3)!,
            },
        };
        var eqUser = _db.GetUserById(1);
        // var eqUser = _db.GetUserByName("Luca");
        Console.Out.WriteLine(user);
        Console.Out.WriteLine(eqUser);
        Console.Out.WriteLine(user.Equals(eqUser));

        Assert.That(_context.Roles.Count(), Is.EqualTo(3)); // assert that 3 Roles have been added
        Assert.That(_db.GetRoleById(1)!.RoleName, Is.EqualTo("admin"));
        Assert.That(_db.GetRoleById(2)!.RoleName, Is.EqualTo("user"));
        Assert.That(_db.GetRoleById(3)!.RoleName, Is.EqualTo("supporter"));
        Assert.Null(_db.GetRoleById(-1));

        Console.Out.WriteLine("Roles Passed!");

        Assert.That(_context.Users.Count(), Is.EqualTo(3)); // assert that 3 Users have been added

        Assert.That(_db.GetAllUsers().Count, Is.EqualTo(3));

        Assert.That(_db.GetUserByName("Luca").Username, Is.EqualTo("Luca"));
        Assert.That(_db.GetUserByName("User").Username, Is.EqualTo("User"));
        Assert.That(_db.GetUserByName("Supporter").Username, Is.EqualTo("Supporter"));

        Console.Out.WriteLine("Users Passed!");
    }
}
