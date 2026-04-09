using System.Collections.Generic;
using System.Linq;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Repositories;

public class UserRepository
{
    private static readonly List<User> users = new();
    private static int nextId = 1;

    public List<User> GetAll() => users;

    public User GetById(int id) => users.FirstOrDefault(u => u.Id == id);

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
        var existing = users.FirstOrDefault(u => u.Id == id);
        if (existing == null) return false;
        users.Remove(existing);
        return true;
    }
}