using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Extensions.Caching.Distributed;
using Love4AnimalsAPI.Interfaces;

namespace Love4AnimalsAPI.Services;

public class CacheService : ICacheService
{
    private readonly IDistributedCache _cache;

    private readonly JsonSerializerOptions _jsonOptions = new()
    {
        ReferenceHandler = ReferenceHandler.IgnoreCycles,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };

    public CacheService(IDistributedCache cache)
    {
        _cache = cache;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var json = await _cache.GetStringAsync(key);

        if (string.IsNullOrEmpty(json))
            return default;

        return JsonSerializer.Deserialize<T>(json, _jsonOptions);
    }

    public async Task SetAsync<T>(string key, T value, int minutes = 5)
    {
        var json = JsonSerializer.Serialize(value, _jsonOptions);

        var options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes)
        };

        await _cache.SetStringAsync(key, json, options);
    }

    public async Task RemoveAsync(string key)
    {
        await _cache.RemoveAsync(key);
    }
}