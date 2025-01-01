namespace PetFamily.Accounts.Contracts.Responces;

public record LoginResponses(string AccessToken, Guid RefreshToken, Guid UserId, string Email);