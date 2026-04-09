using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
using Love4AnimalsAPI.Repositories;

namespace Love4AnimalsAPI.Services;

public class UserService : IUserService
{
    private readonly UserRepository _repo;

    public UserService(UserRepository repo)
    {
        _repo = repo;
    }

    public List<User> GetAll() => _repo.GetAll();
    public User GetById(int id) => _repo.GetById(id);

    public User Create(User user) => _repo.Create(user);

    public User Update(int id, User user) => _repo.Update(id, user);

    public bool Delete(int id) => _repo.Delete(id);
}