using Gambling.Service.Dtos.Game;

namespace Gambling.Service;

public interface IGameService
{
    Task<Result<PlayOutputDto>> Play(PlayInputDto input, CancellationToken cancellationToken);
}
