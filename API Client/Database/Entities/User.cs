using System.Text.Json.Serialization;

namespace API_Client.Database.Entities;

public class User // todo add Mail Field
{
    // public UserEntity(UserDTO user)
    // {
    //     Username = user.username;
    //     Password = user.password;
    //     Name = user.name;
    //     Surname = user.surname;
    // }


    public int Id { get; set; }
    public string Username { get; set; }
    [JsonIgnore] public string Password { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    public List<Role> Roles { get; set; } = new List<Role>();


    override public string ToString()
    {
        return $"Id: {Id}, Username: {Username}, Password: {Password}, Name: {FirstName ?? "N/A"}, Surname: {LastName ?? "N/A"}, Roles: {string.Join(", ", Roles)}";
    }


    // todo get user from DTO
}
