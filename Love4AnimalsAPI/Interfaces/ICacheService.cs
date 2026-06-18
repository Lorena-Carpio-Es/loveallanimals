namespace Love4AnimalsAPI.Interfaces;

public interface ICacheService
{
    Task<T?> GetAsync<T>(string key);

    Task SetAsync<T>(string key, T value, int minutes = 5);

    Task RemoveAsync(string key);
}