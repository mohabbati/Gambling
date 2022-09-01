using Gambling.Shared.Dtos.Game;

namespace Gambling.Contract;

public interface IGameService
{
    Task<Result<PlayOutputDto>> Play(PlayInputDto input, CancellationToken cancellationToken);
}
