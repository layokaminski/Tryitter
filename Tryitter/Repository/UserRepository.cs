using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using Tryitter.Repository;
using Tryitter.Models;
using Tryitter.Context;

namespace Tryitter.Repository
{
  public class UserRepository : IUserRepository
  {
    private TryitterDB _context { get; set; }

    public UserRepository(TryitterDB context)
    {
      _context = context;
    }

    public async Task<IEnumerable<User>> GetAllUsers()
    {
      return await _context.Users.ToListAsync();
    }

    public async Task<User?> GetUserById(int id)
    {
      var user = await _context.Users.FindAsync(id);

      if (user != null)
      {
        user.Password = "";
      }

      return user;
    }

    public async Task<User> CreateUser(User entity)
    {
        await _context.AddAsync(entity);

      _context.SaveChanges();

      return entity;
    }

    public async Task<User> UpdateUser(User entity, int id)
    {
      var updated = await _context.Users.FindAsync(id);

      updated.Name = entity.Name;
      updated.Email = entity.Email;
      updated.Password = entity.Password;
      
      _context.SaveChanges();

      return updated;
    }

    public void DeleteUser(int id)
    {
      var user = _context.Users.Find(id);

      if (user is null)
      {
          throw new Exception();
      }

      _context.Users.Remove(user);
      _context.SaveChanges();
    }
  }
}