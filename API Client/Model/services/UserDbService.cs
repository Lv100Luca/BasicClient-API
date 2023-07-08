using API_Client.Model.DTO;

namespace API_Client.Model.services;

public abstract class UserDbService // todo implement proper DB 
{
    // todo save passwords as hash
    private readonly static List<User> Users = new List<User>
    {
        new User("Loeka", "Keqing", "Admin"),
        new User("Cinnamonroll", "Sakana", "User"),
        new User("Pikachu", "Pikachu", "User"),
        new User("Eevee", "Eevee", "User"),
        new User("Bulbasaur", "Bulbasaur", "User"),
        new User("admin", "admin", "Admin"),
    };


    public static User? GetUserByUsername(string username)
    {
        return Users.Find(user => user.Username == username);
    }


    public static bool DeleteUser(string username)
    {
        return Users.Remove(Users.Find(user => user.Username == username));
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
