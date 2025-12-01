namespace BlogOnAPI.Application.Interfaces;

public interface ICacheableQuery
{
    string CacheKey { get; }
    TimeSpan? Expiration { get; }
}