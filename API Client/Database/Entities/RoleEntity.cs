namespace API_Client.Database.Entities;

public class RoleEntity
{
    public RoleEntity()
    {
        UserRoleEntities = new HashSet<UserRoleEntity>();
    }


    public int pk_id { get; set; } // cant be null
    public string role { get; set; } // cant be null

    public virtual ICollection<UserRoleEntity> UserRoleEntities { get; set; } // todo hmmm
}
