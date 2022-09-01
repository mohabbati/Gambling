using Gambling.Shared.Dtos.Identity;

namespace Gambling.Contracts;

public interface IAuthService
{
    Task<Result<SignUpOutputDto>> SignUpAsync(SignUpInputDto input, CancellationToken cancellationToken);

    Task<Result<SignInOutputDto>> SignInAsync(SignInInputDto input, CancellationToken cancellationToken);
}
