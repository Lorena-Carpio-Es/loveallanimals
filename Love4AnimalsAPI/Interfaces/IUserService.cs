using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllAsync();

    Task<User?> GetByIdAsync(long id);

    Task<User> RegisterAsync(User user, string password);

    Task<User?> LoginAsync(string email, string password);

    Task<bool> UpdateAsync(long id, User user);

    Task<bool> DeleteAsync(long id);
}