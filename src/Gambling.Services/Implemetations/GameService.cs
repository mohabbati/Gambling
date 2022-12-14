using Gambling.Data;
using Gambling.Models.Game;
using Gambling.Models.Identity;
using Gambling.Shared.Dtos.Game;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gambling.Services.Implemetations;

public class GameService : IGameService
{
    private readonly GamblingDbContext _dbContext;
    private readonly UserManager<User> _userManager;
    private readonly IAccountService _accountService;
    private readonly IRandomService _randomService;

    public GameService(GamblingDbContext dbContext, UserManager<User> userManager, IAccountService accountService, IRandomService randomService)
    {
        _dbContext = dbContext;
        _userManager = userManager;
        _accountService = accountService;
        _randomService = randomService;
    }

    public async Task<Result<PlayOutputDto>> PlayAsync(PlayInputDto input, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(input.UserId.ToString());

        if (user is null)
        {
            var message = "User not found.";
            return new Result<PlayOutputDto>(new LogicException(message));
        }

        var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.UserId == user.Id);

        if (account is null)
        {
            var message = "Account has not initialized.";
            return new Result<PlayOutputDto>(new LogicException(message));
        }

        if (account.Balance < input.BetAmount)
        {
            var message = "The bet amount is over than the balance.";
            return new Result<PlayOutputDto>(new LogicException(message));
        };

        var withdrawResult = await _accountService.WithdrawAsync(new() { AccountId = account.Id, Amount = input.BetAmount }, cancellationToken);
        if (withdrawResult.IsSuccess is false)
        {
            var message = withdrawResult.ToString();
            return new Result<PlayOutputDto>(new LogicException(message));
        }

        var play = await PlayGame(account.Id, input, cancellationToken);

        if (play.PlayResult is PlayResult.Won)
        {
            var depositResult = await _accountService.DepositAsync(new() { AccountId = account.Id, Amount = input.BetAmount * 9 }, cancellationToken);
            if (depositResult.IsSuccess is false)
            {
                var message = depositResult.ToString();
                return new Result<PlayOutputDto>(new LogicException(message));
            }
        }

        var output = play.Adapt<PlayOutputDto>();

        return output;
    }

    private async Task<Play> PlayGame(Guid accountId, PlayInputDto playInput, CancellationToken cancellationToken)
    {
        var randomNumber = (byte)_randomService.Generate(0, 9 + 1);

        var play = new Play
        {
            AccountId = accountId,
            BetAmount = playInput.BetAmount,
            ChanceNumber = playInput.ChanceNumber,
            StartAt = DateTimeOffset.Now,
            PlayResult = randomNumber == playInput.ChanceNumber ? PlayResult.Won : PlayResult.Lost,
            Point = randomNumber == playInput.ChanceNumber ? playInput.BetAmount * 9 : 0
        };

        await _dbContext.Plays.AddAsync(play, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return play;
    }
}