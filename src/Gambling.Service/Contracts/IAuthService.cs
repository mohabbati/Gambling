using Gambling.Service.Dtos.Identity;

namespace Gambling.Service;

public interface IAuthService
{
    Task<Result<SignUpOutputDto>> SignUp(SignUpInputDto input, CancellationToken cancellationToken);

    Task<Result<SignInOutputDto>> SignIn(SignInInputDto input, CancellationToken cancellationToken);
}
