using Gambling.Data;
using Gambling.Model.Account;
using Gambling.Model.Identity;
using Gambling.Service.Dtos.Account;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Gambling.Service.Implemetations;

public class AccountService : IAccountService
{
    private readonly GamblingDbContext _dbContext;
    private readonly UserManager<User> _userManager;

    //TODO: We must ensure the account balance is valid due to the concurrency issue.
    //In the current scenario, Semaphore just uses one thread, so it is not a good solution for the issue, but it works for this challenge.
    //Another low cost approach is using queue for every account.
    private readonly SemaphoreSlim _lock = new(1);

    public AccountService(GamblingDbContext dbContext, UserManager<User> userManager)
    {
        _dbContext = dbContext;
        _userManager = userManager;
    }

    public async Task<Result<AccountOutputDto>> InitializeAccount(AccountInputDto input, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(input.UserId.ToString());

        if (user is null)
        {
            var message = "User not found";
            return new Result<AccountOutputDto>(new LogicException(message));
        }

        await _lock.WaitAsync(cancellationToken);

        var initializedBefore = await _dbContext.Accounts.AnyAsync(x => x.UserId == user.Id);

        if (initializedBefore)
        {
            _lock.Release();

            var message = "Account has initialized before.";
            return new Result<AccountOutputDto>(new LogicException(message));
        }

        var account = input.Adapt<Account>();

        var initialAmount = 10000;

        account.Balance = initialAmount;
        account.LastModifiedAt = DateTimeOffset.Now;
        account.AccountTransactions.Add(
            new AccountTransaction()
            {
                TransactionType = TransactionType.Deposit,
                Amount = initialAmount,
                DoneAt = DateTimeOffset.Now
            });

        await _dbContext.Accounts.AddAsync(account, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _lock.Release();

        var output = account.Adapt<AccountOutputDto>();

        return output;
    }

    public async Task<Result<DepositOutputDto>> Deposit(DepositInputDto input, CancellationToken cancellationToken)
    {
        var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == input.AccountId);

        if (account is null)
        {
            var message = "Account not found. Please initialize your account.";
            return new Result<DepositOutputDto>(new LogicException(message));
        }

        account.Balance += input.Amount;
        account.LastModifiedAt = DateTimeOffset.Now;
        account.AccountTransactions.Add(
            new AccountTransaction()
            {
                TransactionType = TransactionType.Deposit,
                Amount = input.Amount,
                DoneAt = DateTimeOffset.Now
            });

        _dbContext.Accounts.Update(account);
        await _dbContext.SaveChangesAsync(cancellationToken);

        var output = account.Adapt<DepositOutputDto>();

        return output;
    }

    public async Task<Result<WithdrawOutputDto>> Withdraw(WithdrawInputDto input, CancellationToken cancellationToken)
    {
        await _lock.WaitAsync(cancellationToken);

        var account = await _dbContext.Accounts.FirstOrDefaultAsync(x => x.Id == input.AccountId);

        if (account is null)
        {
            _lock.Release();

            var message = "Account not found. Please initialize your account.";
            return new Result<WithdrawOutputDto>(new LogicException(message));
        }

        if (account.Balance < input.Amount)
        {
            _lock.Release();

            var message = "The withdraw amount is over than the balance.";
            return new Result<WithdrawOutputDto>(new LogicException(message));
        }

        account.Balance -= input.Amount;
        account.LastModifiedAt = DateTimeOffset.Now;
        account.AccountTransactions.Add(
            new AccountTransaction()
            {
                TransactionType = TransactionType.Withdraw,
                Amount = input.Amount * -1,
                DoneAt = DateTimeOffset.Now
            });

        _dbContext.Accounts.Update(account);
        await _dbContext.SaveChangesAsync(cancellationToken);

        _lock.Release();

        var output = account.Adapt<WithdrawOutputDto>();

        return output;
    }
}