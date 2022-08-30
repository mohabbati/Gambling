namespace Gambling.Service.Dtos.Game;

public class PlayOutputDto
{
    public int AccountBalance { get; set; }

    [JsonIgnore]
    public PlayResult PlayResult { get; set; }

    public string Status => PlayResult.ToString();

    [JsonIgnore]
    public int Point { get; set; }

    public string? Points => PlayResult is PlayResult.Won ? Point.ToString().ToPlusPoint() : Point.ToString();
}
