namespace Gambling.Models;

public class AppSettings
{
    public IdentitySettings IdentitySettings { get; set; }

    public JwtSettings JwtSettings { get; set; }
}

public class IdentitySettings
{
    public int PasswordRequiredLength { get; set; }
}

public class JwtSettings
{
    public string SecretKey { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int NotBeforeMinutes { get; set; }
    public int ExpirationMinutes { get; set; }
}
