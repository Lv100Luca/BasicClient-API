namespace API_Client.Database.Entities;

public class UserRoleEntity
{
    public int UserId { get; set; } // Many-to-many with class for join entity
    public int RoleId { get; set; }
}
//     public virtual User User { get; set; } = null!; // Many-to-many with navigations to and from join entity
//     public virtual Role Role { get; set; } = null!;
// }
