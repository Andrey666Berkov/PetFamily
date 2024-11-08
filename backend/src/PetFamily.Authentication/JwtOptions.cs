namespace PetFamily.Authentication;

public class JwtOptions
{
    public static string JWT { get; } = nameof(JWT);
    public string Issuer { get; init; }
    public string Audience { get; init; }
    public string Key { get; init; }
    public int ExpiredMinutesTime { get; init; }
}