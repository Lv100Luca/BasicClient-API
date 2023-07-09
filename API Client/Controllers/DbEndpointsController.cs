using Microsoft.AspNetCore.Mvc;
using Npgsql;

namespace API_Client.Controllers;

[ApiController]
[Route("DB")]
public class DbEndpointsController : ControllerBase
{
    [HttpGet("all")]
    public IActionResult GetAllUsers() //todo implement properly EntityMapping
    {
        string connectionString = "Host=localhost;Port=5432;Database=Users;Username=postgres;Password=admin";

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
}
