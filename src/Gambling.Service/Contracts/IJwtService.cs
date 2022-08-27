using Gambling.Model.Account;
using Gambling.Service.Dtos.Account;

namespace Gambling.Service;

public interface IJwtService
{
    Task<SignInOutputDto> GenerateToken(User user);
}
