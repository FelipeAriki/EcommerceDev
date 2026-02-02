
using Microsoft.Extensions.Caching.Memory;

namespace EcommerceDev.Infrastructure.Caching;

public class CacheService : ICacheService
{
    private readonly IMemoryCache _memoryCache;
    private readonly TimeSpan _defaultExpiration = TimeSpan.FromMinutes(10);

    public CacheService(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public Task<T?> GetAsync<T>(string key)
    {
        var value = _memoryCache.Get<T>(key);
        return Task.FromResult(value);
    }

    public Task SetAsync<T>(string key, T value, TimeSpan? expiration = null)
    {
        var expirationTimeSpan = expiration ?? _defaultExpiration;
        var cacheEntryOptions = new MemoryCacheEntryOptions()
            .SetAbsoluteExpiration(expirationTimeSpan);

        _memoryCache.Set(key, value, cacheEntryOptions);
        return Task.CompletedTask;
    }

    public Task RemoveAsync(string key)
    {
        _memoryCache.Remove(key);
        return Task.CompletedTask;
    }
}
