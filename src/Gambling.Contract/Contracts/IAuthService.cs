using Gambling.Shared.Dtos.Identity;

namespace Gambling.Contract;

public interface IAuthService
{
    Task<Result<SignUpOutputDto>> SignUp(SignUpInputDto input, CancellationToken cancellationToken);

    Task<Result<SignInOutputDto>> SignIn(SignInInputDto input, CancellationToken cancellationToken);
}
