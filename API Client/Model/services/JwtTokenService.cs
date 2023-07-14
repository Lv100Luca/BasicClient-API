using API_Client.Database.Entities;
using API_Client.Exceptions;
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
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecurityKey"]));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256); // hashes the security
        // claims hold User data
        var claims = new List<Claim>() //claims are what is going to be written into the Payload of the token
        {
            // todo make own claim Interface
            new Claim("Id", user.Id.ToString()),
            new Claim("Username", user.Username),
            new Claim("fullName", user.FirstName + " " + user.LastName),
        };
        foreach (Role role in user.Roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.RoleName));
        }

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            expires: DateTime.Now.AddHours(1),
            // all the above are Claims
            signingCredentials: credentials // this is the secret that is required to verify the tokens authenticity 
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }


    public string GetClaimFromToken(JwtSecurityToken token, string claimName)
    {
        var claim = token.Claims.FirstOrDefault(claim => claim.Type == claimName)?.Value;
        if (claim is null)
        {
            throw new JwtTokenException($"Claim {claimName} not found");
        }
        return claim;
    }


    public JwtSecurityToken ParseToken(HttpContext httpContext)
    {
        if (httpContext.Request.Headers.Authorization.ToString() == "")
        {
            Console.Out.WriteLine("is empty");
            throw new JwtTokenException("no token");
        }

        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        try
        {
            var token = httpContext.Request.Headers.Authorization.ToString().Split(" ")[1];
            return tokenHandler.ReadJwtToken(token); // throws System.ArgumentException if invalidFormat
        }
        catch (System.ArgumentException e)
        {
            throw new JwtTokenException($"Token has invalid Format {e}");
        }
    }
}
