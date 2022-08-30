using Gambling.Model.Game;
using Gambling.Service.Dtos.Account;

namespace Gambling.Service;

public interface IAccountService
{
	Task<Result<AccountOutputDto>> InitializeAccount(AccountInputDto input, CancellationToken cancellationToken);

    Task<Result<DepositOutputDto>> Deposit(DepositInputDto input, CancellationToken cancellationToken);

    Task<Result<WithdrawOutputDto>> Withdraw(WithdrawInputDto input, CancellationToken cancellationToken);
}
