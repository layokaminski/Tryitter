using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Tryitter.Repository;
using Tryitter.Models;
using Tryitter.Context;

namespace Tryitter.Repository
{
  public class PostRepository : IPostRepository
  {
    private TryitterDB _context { get; set; }

    public PostRepository(TryitterDB context)
    {
      _context = context;
    }

    public async Task<IEnumerable<Post>> GetAll()
    {
      return await _context.Posts.ToListAsync();
    }

    public async Task<Post?> GetById(int id)
    {
      return await _context.Posts.FindAsync(id);
    }

    public async Task<Post> Create(Post entity)
    {
        await _context.AddAsync(entity);

      _context.SaveChanges();

      return entity;
    }

    public async Task<Post> Update(Post entity, int id)
    {
      var updated = await _context.Posts.FindAsync(id);

      updated.Description = entity.Description;
      
      _context.SaveChanges();

      return updated;
    }

    public void Delete(int id)
    {
      var post = _context.Posts.Find(id);

      if (post is null)
      {
          throw new Exception();
      }

      _context.Posts.Remove(post);
      _context.SaveChanges();
    }
  }
}