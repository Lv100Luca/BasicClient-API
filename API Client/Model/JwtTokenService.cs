using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Client.Model;

public class JwtTokenService // ask -> inject this service into the controller ??
{
    private readonly IConfiguration _config;


    public JwtTokenService(IConfiguration config)
    {
        _config = config;
    }


    public string GenerateToken(User user) // generates token somehow 
    // todo move to own class
    {
        // ask -> prevent null reference?
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // uhh
        // claims hold User data
        var claims = new[] //todo: what are claims? -> claims are what is going to be written into the Payload of the token
        {
            new Claim("Username", user.Username),
            new Claim("Role", user.Role),
            new Claim("Claim", $"I am {user.Username} Crazy Claim"),
        };

        var token = new JwtSecurityToken(
            _config["Jwt:Issuer"],
            _config["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddMinutes(60), //token expires in 60 minutes
            // all the above are Claims
            signingCredentials: credentials // this is the secret that is required to verify the tokens authenticity 
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
