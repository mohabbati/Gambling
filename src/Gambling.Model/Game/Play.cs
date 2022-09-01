namespace Gambling.Models.Game;

public class Play : IGamblingEntity
{
    [Key]
    public Guid Id { get; set; }
    
    [ForeignKey(nameof(AccountId))]
    public Account.Account Account { get; set; }
    public Guid AccountId { get; set; }
    
    public int BetAmount { get; set; }

    public byte ChanceNumber { get; set; }

    public PlayResult PlayResult { get; set; }

    public int Point { get; set; }

    public DateTimeOffset StartAt { get; set; }
}
