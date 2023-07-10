namespace API_Client.Database.Entities
{
    public class User // todo split into seperate classes and rename to UserEntity
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }

        public ICollection<Role> Roles { get; set; }


        override public string ToString()
        {
            return $"Id: {Id}, Username: {Username}, Password: {Password}, Name: {Name ?? "N/A"}, Surname: {Surname ?? "N/A"}, Roles: {string.Join(", ", Roles)}";
        }
    }

    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public ICollection<User> Users { get; set; }


        override public string ToString()
        {
            return $"Id: {Id}, RoleName: {RoleName}";
        }
    }
}
