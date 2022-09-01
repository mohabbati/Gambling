using Gambling.Shared.Dtos.Account;

namespace Gambling.Api.Controllers;

public class AccountController : GamblingControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> InitializeAccount(AccountInputDto input, CancellationToken cancellationToken)
    {
        input.UserId = User.GetUserId();

        var result = await _accountService.InitializeAccount(input, cancellationToken);

        return result.ToOk(c => c);
    }
}
