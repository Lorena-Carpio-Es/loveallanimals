using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Services;

public class UserService : IUserService
{
    private static List<User> users = new List<User>();
    private static int nextId = 1;

    public List<User> GetAll()
    {
        return users;
    }

    public User GetById(int id)
    {
        return users.FirstOrDefault(u => u.Id == id);
    }

    public User Create(User user)
    {
        user.Id = nextId++;
        users.Add(user);
        return user;
    }

    public User Update(int id, User user)
    {
        var existing = users.FirstOrDefault(u => u.Id == id);
        if (existing == null) return null;

        existing.Email = user.Email;
        existing.Name = user.Name;

        return existing;
    }

    public bool Delete(int id)
    {
        var user = users.FirstOrDefault(u => u.Id == id);
        if (user == null) return false;

        users.Remove(user);
        return true;
    }
}