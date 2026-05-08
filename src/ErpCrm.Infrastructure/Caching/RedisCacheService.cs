using System.Text.Json;
using ErpCrm.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace ErpCrm.Infrastructure.Caching;

public class RedisCacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    public RedisCacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var data = await _cache.GetStringAsync(key);

        if (string.IsNullOrWhiteSpace(data))
        {
            Console.WriteLine($"CACHE MISS: {key}");
            return default;
        }
        Console.WriteLine($"CACHE HIT: {key}");

        return JsonSerializer.Deserialize<T>(data);
    }

    public async Task SetAsync<T>(
     string key,
     T value,
     TimeSpan? expiration = null)
    {
        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow =
                expiration ?? TimeSpan.FromMinutes(5)
        };

        var json = JsonSerializer.Serialize(value);

        await _cache.SetStringAsync(key, json, options);

        Console.WriteLine($"CACHE SET: {key}");
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);

        Console.WriteLine($"CACHE REMOVED: {key}");
    }
}