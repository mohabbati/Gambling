namespace Gambling.Service.Implemetations;

public class RandomService : IRandomService
{
    private readonly Random _random = new Random();

    public int Generate(int minValue, int maxValue)
    {
        var randomNumber = _random.Next(minValue, maxValue);

        return randomNumber;
    }
}
