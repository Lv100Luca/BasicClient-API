namespace API_Client.Database.Entities;

public class UserRoleEntity : BaseEntity
{
    public int UserId { get; set; }
    public int RoleId { get; set; }

    public virtual UserEntity User { get; set; }
    public virtual RoleEntity Role { get; set; }
}
