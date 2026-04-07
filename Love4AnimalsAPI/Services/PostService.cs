using System;
using Love4AnimalsAPI.Interfaces;
using Love4AnimalsAPI.Models;

namespace Love4AnimalsAPI.Services;

public class PostService : IPostService
{
    private static List<Post> publications = new();
    private static long idCounter = 1;

    public List<Post> GetAll() => publications;

    public Post GetById(long id)
        => publications.FirstOrDefault(p => p.Id == id);

    public List<Post> GetByUser(long userId)
        => publications.Where(p => p.UserId == userId).ToList();

    public Post Create(Post pub)
    {
        pub.Id = idCounter++;
        pub.CreationDate = DateTime.Now;
        pub.State = "Active";
        publications.Add(pub);
        return pub;
    }

    public Post Update(long id, Post pub)
    {
        var existing = GetById(id);
        if (existing == null) return null;

        existing.Title = pub.Title;
        existing.Description = pub.Description;

        return existing;
    }

    public bool Delete(long id)
    {
        var pub = GetById(id);
        if (pub == null) return false;

        publications.Remove(pub);
        return true;
    }

    public void DarLike(long id)
    {
        var pub = GetById(id);
        if (pub != null) pub.QuantityLikes++;
    }

    public void Compartir(long id)
    {
        var pub = GetById(id);
        if (pub != null) pub.QuantityShared++;
    }


}
