using Gambling.Models.Identity;

namespace Gambling.Models.Account;

public class Account : IGamblingEntity
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(UserId))]
    public User User { get; set; }
    public Guid UserId { get; set; }

    public int Balance { get; set; }

    public DateTimeOffset LastModifiedAt { get; set; }

    public List<AccountTransaction> AccountTransactions { get; set; } = new();
}
