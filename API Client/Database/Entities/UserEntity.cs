namespace API_Client.Database.Entities;

public class UserEntity
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
    public string Password { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }

    public List<RoleEntity> Roles { get; set; }


    override public string ToString()
    {
        return $"Id: {Id}, Username: {Username}, Password: {Password}, Name: {Name ?? "N/A"}, Surname: {Surname ?? "N/A"}, Roles: {string.Join(", ", Roles)}";
    }


    override public bool Equals(object? obj)
    {
        if (obj is UserEntity)
        {
            var user = (UserEntity)obj;
            return (
                Username == user.Username &&
                Password == user.Password &&
                Name == user.Name &&
                Surname == user.Surname &&
                Roles.Count == user.Roles.Count
            ); //todo add roles check as well
        }

        return false;
    }
}
