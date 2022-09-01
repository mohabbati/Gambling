namespace Gambling.Models.Common;

public interface IGamblingEntity<TKey>
{
    TKey Id { get; set; }
}

public interface IGamblingEntity : IGamblingEntity<Guid>
{
}
