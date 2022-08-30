namespace Gambling.Service.Dtos.Account;

public class DepositInputDto
{
    [Required]
    public Guid AccountId { get; set; }
    
    [Required]
    [Range(1, 999999)]
    public int Amount { get; set; }
}
