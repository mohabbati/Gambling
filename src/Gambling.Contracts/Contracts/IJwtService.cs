using Gambling.Shared.Dtos.Identity;

namespace Gambling.Contracts;

public interface IJwtService
{
    Task<SignInOutputDto> GenerateTokenAsync(SignInInputDto input, CancellationToken cancellationToken);
}
