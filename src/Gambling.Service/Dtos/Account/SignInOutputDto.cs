namespace Gambling.Service.Dtos.Account;

public class SignInOutputDto
{
    public string? AccessToken { get; set; }

    public long ExpiresIn { get; set; }
}
