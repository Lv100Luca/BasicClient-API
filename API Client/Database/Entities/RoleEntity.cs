namespace API_Client.Database.Entities;

public class RoleEntity : BaseEntity
{
    // public RoleEntity()
    // {
    //     UserRole = new HashSet<UserRoleEntity>();
    // }


    public int Id { get; set; } // cant be null
    public string role { get; set; } // cant be null

    public virtual ICollection<UserRoleEntity> UserRole { get; set; } // todo hmmm
}
