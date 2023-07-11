using API_Client.Database;
using API_Client.Model.DTO;
using API_Client.Model.services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;

namespace ApiClient_Tests;

public class Tests
{
    private readonly static UserDbContext _context = new UserDbContext(new DbContextOptionsBuilder<UserDbContext>().UseInMemoryDatabase(databaseName: "Memory").Options);
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
                3
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

        Assert.That(_context.Roles.Count(), Is.EqualTo(3)); // assert that 3 Roles have been added
        Assert.That(_context.GetRoleById(1).RoleName, Is.EqualTo("admin"));
        Assert.That(_context.GetRoleById(2).RoleName, Is.EqualTo("user"));
        Assert.That(_context.GetRoleById(3).RoleName, Is.EqualTo("supporter"));
        Assert.Null(_context.GetRoleById(-1));

        Console.Out.WriteLine("Roles Passed!");

        Assert.That(_context.Users.Count(), Is.EqualTo(3)); // assert that 3 Users have been added

        Assert.That(_db.GetAllUsers().Count, Is.EqualTo(3));

        Assert.That(_db.GetUserById(1));
        Console.Out.WriteLine("Users Passed!");
    }


    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}
