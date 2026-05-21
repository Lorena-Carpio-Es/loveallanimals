using Microsoft.EntityFrameworkCore;
using Love4AnimalsAPI.Data;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.Users
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<User?> GetByIdAsync(long id)
    {
        return await _context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> RegisterAsync(User user, string password)
    {
        var emailExists = await _context.Users
            .AnyAsync(u => u.Email == user.Email);

        if (emailExists)
            throw new Exception("El email ya está registrado");

        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return user;
    }

    public async Task<User?> LoginAsync(string email, string password)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Email == email);

        if (user == null)
            return null;

        var passwordOk = BCrypt.Net.BCrypt.Verify(password, user.PasswordHash);

        if (!passwordOk)
            return null;

        return user;
    }

    public async Task<bool> UpdateAsync(long id, User user)
    {
        var existing = await _context.Users.FindAsync(id);

        if (existing == null)
            return false;

        existing.Name = user.Name;
        existing.Email = user.Email;

        await _context.SaveChangesAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return true;
    }
}