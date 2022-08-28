namespace Gambling.Service;

public interface IAuthService
{
    Task<Result<SignUpOutputDto>> SignUp(SignUpInputDto user, CancellationToken cancellationToken);

    Task<Result<SignInOutputDto>> SignIn(SignInInputDto user, CancellationToken cancellationToken);
}
