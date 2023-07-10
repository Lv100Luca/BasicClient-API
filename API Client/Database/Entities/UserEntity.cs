namespace API_Client.Database.Entities;

public class UserEntity // todo: virtual and sealed
{
    public UserEntity()
    {
        UserRoleEntities = new HashSet<UserRoleEntity>();
    }


    public int pk_id { get; set; } // cant be null
    public string username { get; set; } // cant be null
    public string password { get; set; } // cant be null
    public string? name { get; set; } // can be null
    public string? sirname { get; set; } // can be null

    public virtual ICollection<UserRoleEntity> UserRoleEntities { get; set; } // todo hmmm
}
