namespace API_Client.Database.Entities;

public class UserRoleEntity
{
    public int fk_user_id { get; set; }
    public int fk_role_id { get; set; }

    public virtual UserEntity User { get; set; }
    public virtual RoleEntity Role { get; set; }
}
