namespace API_Client.Database.Entities
{
    public class User
    {
        // ask -> why do i have role and roleId in my DB after migration
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int RoleId { get; set; } = -1;


        public override string ToString()
        {
            return $"Id: {Id}, Username: {Username}, Password: {Password}, Name: {Name ?? "N/A"}, Surname: {Surname ?? "N/A"}, RoleId: {RoleId}";
        }
    }

    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public List<User> Users { get; set; }
    }
}
