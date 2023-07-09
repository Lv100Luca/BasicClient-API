using API_Client.Model.DTO;

namespace API_Client.Model.services;

public abstract class UserDbService // todo implement proper DB 
{
    // todo save passwords as hash
    private readonly static List<User> Users = new List<User>
    {
        new User("Loeka", "Keqing", "2"),
        new User("Cinnamonroll", "Sakana", "1"),
        new User("Pikachu", "Pikachu", "1"),
        new User("Eevee", "Eevee", "User"),
        new User("Bulbasaur", "Bulbasaur", "User"),
        new User("admin", "admin", "Admin"),
        new User("string", "string", "Admin"),
    };


    public static User? GetUserByUsername(string username)
    {
        return Users.Find(user => user.Username == username);
    }


    public static bool DeleteUser(string username)
    {
        User? user = Users.FirstOrDefault(u => u.Username == username);
        return user != null && Users.Remove(user);
    }


    public static bool AddUser(User newUser)
    {
        //if user with name doesnt exist already
        if (Users.Find(user => user.Username == newUser.Username) == null)
        {
            Users.Add(newUser);
            return true;
        }
        return false;
    }


    public static User[] GetAllUsers()
    {
        return Users.ToArray();
    }


    public static User? Authenticate(UserLoginDto userLogin)
    {
        return Users.Find(user => user.Username == userLogin.Username && user.Password == userLogin.Password);
    }
}
