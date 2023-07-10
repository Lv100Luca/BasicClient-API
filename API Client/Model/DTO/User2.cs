namespace API_Client.Model.DTO;

public record User2(string username, string password, string? name, string? surname, int[] roles);
