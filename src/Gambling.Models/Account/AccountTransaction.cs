namespace Gambling.Models.Account;

public class AccountTransaction : IGamblingEntity
{
    [Key]
    public Guid Id { get; set; }

    [ForeignKey(nameof(AccountId))]
    public Account Account { get; set; }
    public Guid AccountId { get; set; }

    public TransactionType TransactionType { get; set; }

    public int Amount { get; set; }

    public DateTimeOffset DoneAt { get; set; }
}
