using Gambling.Shared.Dtos.Game;

namespace Gambling.Contracts;

public interface IGameService
{
    Task<Result<PlayOutputDto>> PlayAsync(PlayInputDto input, CancellationToken cancellationToken);
}
