namespace Gambling.Shared.Dtos.Game;

public class PlayInputDto
{
    [Required]
    [JsonIgnore]
    public Guid UserId { get; set; }

    [Required]
    [Range(1, 999999)]
    public int BetAmount { get; set; }

    [Required]
    [Range(0, 9)]
    public byte ChanceNumber { get; set; }
}
