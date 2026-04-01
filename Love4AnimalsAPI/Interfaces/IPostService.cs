using System;
using Love4AnimalsAPI.Models;
namespace Love4AnimalsAPI.Interfaces;

public interface IPostService
{
    List<Post> GetAll();
    Post GetById(long id);
    List<Post> GetByUser(long userId);

    Post Create(Post pub);
    Post Update(long id, Post pub);
    bool Delete(long id);

    void DarLike(long id);
    void Compartir(long id);

}
