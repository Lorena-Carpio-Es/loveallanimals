using System;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Interfaces;

public interface IUserService
{
     List<User> GetAll();
    User GetById(int id);
    User Create(User user);
    User Update(int id, User user);
    bool Delete(int id);
}
