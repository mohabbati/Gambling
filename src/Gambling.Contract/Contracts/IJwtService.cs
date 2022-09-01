using Gambling.Shared.Dtos.Identity;

namespace Gambling.Contract;

public interface IJwtService
{
    Task<SignInOutputDto> GenerateToken(SignInInputDto input);
}
