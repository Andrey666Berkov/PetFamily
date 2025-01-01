namespace Petfamily.Accounts.Domain.DataModels;

public class RefreshSession 
{
    public Guid Id { get; init; }
    public Guid UserId { get; init; }
    public Guid Jti { get; init; }
    public User User { get; init; } = null!;
    public Guid RefreshTokenId { get; init; }
    public DateTime ExpiresIn { get; init; }
    public DateTime CreatedAt { get; init; }
}