namespace API_Client.Database.Entities;

public class UserEntity
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }

    public List<RoleEntity> Roles { get; set; }


    override public string ToString()
    {
        return $"Id: {Id}, Username: {Username}, Password: {Password}, Name: {Name ?? "N/A"}, Surname: {Surname ?? "N/A"}, Roles: {string.Join(", ", Roles)}";
    }
}
