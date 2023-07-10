﻿namespace API_Client.Database.Entities;

public class UserEntity : BaseEntity // todo: virtual and sealed
{
    // public UserEntity()
    // {
    //     UserRole = new HashSet<UserRoleEntity>();
    // }


    public int Id { get; set; } // cant be null
    public string username { get; set; } // cant be null
    public string password { get; set; } // cant be null
    public string? name { get; set; } // can be null
    public string? sirname { get; set; } // can be null

    public virtual ICollection<UserRoleEntity> UserRole { get; set; } // todo hmmm
}
