namespace API_Client.Database;

public class UserDbService
{
    private readonly UserDataContext _context;
    private readonly ILogger<UserDbService> _logger;


    public UserDbService(ILogger<UserDbService> logger, UserDataContext context)
    {
        this._logger = logger;
        this._context = context;
    }


    public List<Entities.User> GetAllUsers()
    {
        return _context.Users.ToList();
    }
}
