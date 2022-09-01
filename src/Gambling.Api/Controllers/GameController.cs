using Gambling.Shared.Dtos.Game;

namespace Gambling.Api.Controllers;

public class GameController : GamblingControllerBase
{
    private readonly IGameService _gameService;

    public GameController(IGameService gameService)
    {
        _gameService = gameService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> Play(PlayInputDto input, CancellationToken cancellationToken)
    {
        input.UserId = User.GetUserId();

        var result = await _gameService.Play(input, cancellationToken);

        return result.ToOk(c => c);
    }
}
