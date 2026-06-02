using Love4AnimalsAPI.Dto;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Interfaces;

public interface IUserService
{
    Task<List<User>> GetAllAsync();

    Task<User?> GetByIdAsync(long id);

    Task<User> RegisterAsync(User user, string password);

    Task<AuthResponseDto?> LoginAsync(string email, string password);

    Task<AuthResponseDto?> RefreshTokenAsync(string refreshToken);

    Task<bool> UpdateAsync(long id, User user);

    Task<bool> DeleteAsync(long id);
}