namespace API_Client.Model.DTO;

public record UserDTO(string username, string password, string? name, string? surname, int[] roles);
