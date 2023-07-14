namespace API_Client.Model.DTO;

public record UserDto(int id, string username, string password, string? name, string? surname, int[] roles);
