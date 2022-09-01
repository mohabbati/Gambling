namespace Gambling.Contracts;

public interface IRandomService
{
    int Generate(int minValue, int maxValue);
}
