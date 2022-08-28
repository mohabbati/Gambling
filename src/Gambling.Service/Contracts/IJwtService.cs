using Gambling.Model.Identity;
using Gambling.Service.Dtos.Identity;

namespace Gambling.Service;

public interface IJwtService
{
    Task<SignInOutputDto> GenerateToken(User user);
}
