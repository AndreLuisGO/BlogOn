using MediatR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;
using BlogOnAPI.Application.Interfaces;

namespace BlogOnAPI.Infrastructure.Behaviors;

public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICacheableQuery, IRequest<TResponse>
{
    private readonly IDistributedCache _cache;
    private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;

    public CachingBehavior(IDistributedCache cache, ILogger<CachingBehavior<TRequest, TResponse>> logger)
    {
        _cache = cache;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var cachedResponse = await _cache.GetStringAsync(request.CacheKey, cancellationToken);

        if (!string.IsNullOrEmpty(cachedResponse))
        {
            _logger.LogInformation($"Cache HIT for {request.CacheKey}");
            return JsonSerializer.Deserialize<TResponse>(cachedResponse)!;
        }

        _logger.LogInformation($"Cache MISS for {request.CacheKey}");
        var response = await next();

        if (response != null)
        {
            var options = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = request.Expiration ?? TimeSpan.FromMinutes(5)
            };

            var serializedData = JsonSerializer.Serialize(response);
            await _cache.SetStringAsync(request.CacheKey, serializedData, options, cancellationToken);
        }

        return response;
    }
}