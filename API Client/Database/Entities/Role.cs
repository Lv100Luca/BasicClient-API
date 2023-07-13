using System.Text.Json.Serialization;

namespace API_Client.Database.Entities;

public class Role
{
    public int Id { get; set; }
    public string RoleName { get; set; }

    [JsonIgnore] //todo what does this do
    public List<User> Users { get; set; } = new List<User>();


    override public string ToString()
    {
        return $"Id: {Id}, RoleName: {RoleName}";
    }
}
