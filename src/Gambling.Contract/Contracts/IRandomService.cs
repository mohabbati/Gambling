namespace Gambling.Contract;

public interface IRandomService
{
    int Generate(int minValue, int maxValue);
}
