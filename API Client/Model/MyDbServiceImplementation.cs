namespace API_Client.Model;

public class MyDbServiceImplementation // todo implement proper DB 
{
    private static List<User> users = new List<User>
    {
        new User("Loeka", "Keqing", "Admin"),
        new User("Cinnamonroll", "Sakana", "User"),
        new User("Pikachu", "Pikachu", "User"),
        new User("Eevee", "Eevee", "User"),
        new User("Bulbasaur", "Bulbasaur", "User"),
    };

    //crazy comment
    /// <summary>
    /// Retrieves a user from the list of users based on the provided username.
    /// </summary>
    /// <param name="username">The username to search for.</param>
    /// <returns>The user with the specified username, or null if no user is found.</returns>
    public static User? GetUserByUsername(string username)
    {
        var user = users.Find(user => user.Username == username);
        if (user == null)
        {
            return null;
        }
        return user;
    }

    public static bool DeleteUser(string username)
    {
        var user = users.Find(user => user.Username == username);
        if (user == null)
        {
            return false;
        }
        users.Remove(user);
        return true;
    }

    public static bool AddUser(User newUser)
    {
        //if user with name doesnt exist already
        if (users.Find(user => user.Username == newUser.Username) == null)
        {
            users.Add(newUser);
            return true;
        }
        return false;
    }

}