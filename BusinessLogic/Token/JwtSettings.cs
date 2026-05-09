namespace BusinessLogic.Token;

public class JwtSettings(string key, string issuer, string audience, double expiryMinutes)
{
    public string SecretKey { get; init; } = key;
    public string Issuer { get; init; } = issuer;
    public string Audience { get; init; } = audience;
    public double ExpiryMinutes { get; init; } = expiryMinutes;
};
