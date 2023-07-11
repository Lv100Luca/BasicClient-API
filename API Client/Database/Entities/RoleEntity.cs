namespace API_Client.Database.Entities;

public class RoleEntity
{
    public int Id { get; set; }
    public string RoleName { get; set; }

    public List<UserEntity> Users { get; set; }


    override public string ToString()
    {
        return $"Id: {Id}, RoleName: {RoleName}";
    }
}
