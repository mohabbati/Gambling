namespace Gambling.Service;

public interface IRandomService
{
    int Generate(int minValue, int maxValue);
}
