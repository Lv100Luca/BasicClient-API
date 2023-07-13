namespace API_Client.Exceptions;

public class JwtTokenException : Exception
{
    public JwtTokenException(string message = "Error with JwtToken") : base(message) { }
}
