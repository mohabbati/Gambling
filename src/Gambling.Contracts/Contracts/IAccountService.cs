using Gambling.Shared.Dtos.Account;

namespace Gambling.Contracts;

public interface IAccountService
{
	Task<Result<AccountOutputDto>> InitializeAccountAsync(AccountInputDto input, CancellationToken cancellationToken);

    Task<Result<DepositOutputDto>> DepositAsync(DepositInputDto input, CancellationToken cancellationToken);

    Task<Result<WithdrawOutputDto>> WithdrawAsync(WithdrawInputDto input, CancellationToken cancellationToken);
}
