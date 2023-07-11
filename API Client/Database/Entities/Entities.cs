namespace API_Client.Database.Entities
{
    public class User // todo split into seperate classes and rename to UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public List<Role> Roles { get; set; }


        override public string ToString()
        {
            return $"Id: {Id}, Username: {Username}, Password: {Password}, Name: {Name ?? "N/A"}, Surname: {Surname ?? "N/A"}, Roles: {string.Join(", ", Roles)}";
        }
    }

    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public List<User> Users { get; set; }


        override public string ToString()
        {
            return $"Id: {Id}, RoleName: {RoleName}";
        }
    }


    public class UserRole
    {
        public int UserId { get; set; } // Many-to-many with class for join entity
        public int RoleId { get; set; }
    }
    //     public virtual User User { get; set; } = null!; // Many-to-many with navigations to and from join entity
    //     public virtual Role Role { get; set; } = null!;
    // }
}
