using API_Client.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace API_Client.Controllers;

[ApiController]
[Route("DB")]
public class DbEndpointsController : ControllerBase
{
    private IConfiguration _configuration;


    public DbEndpointsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    [HttpGet("all")]
    public IActionResult GetAllUsers() //todo implement properly EntityMapping
    {
        string? connectionString = _configuration["Db:ConnectionString"];
        if (connectionString is null)
        {
            throw new Exception("No connection string found in appsettings.json");
        }
        Console.Out.WriteLine(connectionString);

        try
        {
            using (var connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                Console.WriteLine("Connection successful!");

                string sql = "SELECT * FROM tbl_users";
                using (var command = new NpgsqlCommand(sql, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int id = reader.GetInt32(0); // Assuming the ID is stored as an integer in the first column
                            string username = reader.GetString(1); // Assuming the username is stored as a string in the second column
                            string name = reader.GetString(3);
                            string sirname = reader.GetString(4);
                            // Read other columns as needed

                            Console.WriteLine($"ID: {id}, Username: {username}, Name: {name}, Surname: {sirname}");
                        }
                    }
                }
                connection.Close();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error connecting to the database: " + ex.Message);
        }
        return Ok();
    }


    [HttpGet("all2")]
    public IActionResult GetAllUsers2()
    {
        string? connectionString = _configuration["Db:ConnectionString"];
        if (connectionString is null)
        {
            throw new Exception("No connection string found in appsettings.json");
        }
        using (var dbContext = new ApiDbContext(new DbContextOptionsBuilder<ApiDbContext>().UseNpgsql(connectionString).Options))
        {
            var users = dbContext.Users.ToList();

            foreach (var user in users)
            {
                Console.WriteLine($"User ID: {user.Id}, Name: {user.name}");
            }
        }
        return Ok();
    }
}
