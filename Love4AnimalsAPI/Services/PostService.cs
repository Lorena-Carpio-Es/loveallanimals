using System;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;
using Love4AnimalsAPI.Repositories;

namespace Love4AnimalsAPI.Services;

public class PostService : IPostService
{
    private readonly PostRepository _repo;

    public PostService(PostRepository repo)
    {
        _repo = repo;
    }

    public List<Post> GetAll() => _repo.GetAll();

    public Post GetById(long id) => _repo.GetById(id);

    public List<Post> GetByUser(long userId) => _repo.GetByUser(userId);

    public Post Create(Post pub) => _repo.Create(pub);

    public Post Update(long id, Post pub) => _repo.Update(id, pub);

    public bool Delete(long id) => _repo.Delete(id);

    public void DarLike(long id) => _repo.DarLike(id);

    public void Compartir(long id) => _repo.Compartir(id);
}