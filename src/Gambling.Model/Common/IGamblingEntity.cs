namespace Gambling.Model.Common;

public interface IGamblingEntity<TKey>
{
    Guid Id { get; set; }
}

public interface IGamblingEntity : IGamblingEntity<Guid>
{
}
