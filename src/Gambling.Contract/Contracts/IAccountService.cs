using Gambling.Shared.Dtos.Account;

namespace Gambling.Contract;

public interface IAccountService
{
	Task<Result<AccountOutputDto>> InitializeAccount(AccountInputDto input, CancellationToken cancellationToken);

    Task<Result<DepositOutputDto>> Deposit(DepositInputDto input, CancellationToken cancellationToken);

    Task<Result<WithdrawOutputDto>> Withdraw(WithdrawInputDto input, CancellationToken cancellationToken);
}
