using API_Client.Model.DTO;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Client.Model.services;

public class JwtTokenService
{
    private readonly IConfiguration _configuration;


    public JwtTokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }


    public string GenerateToken(User user)
    {
        // ask -> prevent null reference?
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // hashes the security
        // claims hold User data
        var claims = new[] //claims are what is going to be written into the Payload of the token
        {
            new Claim(ClaimTypes.NameIdentifier, user.Username),
            new Claim(ClaimTypes.Role, user.Role),
            new Claim("Claim", $"I am {user.Username} Crazy Claim"),
        };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddYears(50),
            // all the above are Claims
            signingCredentials: credentials // this is the secret that is required to verify the tokens authenticity 
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public string GetUsernameFromToken(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        Console.Out.WriteLine(token);
        JwtSecurityToken jwtToken = tokenHandler.ReadJwtToken(token);
        Console.Out.WriteLine(jwtToken);
        return jwtToken.Claims.First(claim => claim.Type == ClaimTypes.NameIdentifier).Value;
    }
}
