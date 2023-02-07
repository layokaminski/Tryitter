using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

public class TryRepository : ITryRepository
{
  private DbContext _context { get; set; }

  public TryRepository(DbContext context)
  {
    _context = context;
  }

  public IEnumerable<T> GetAll<T>() where T : class
  {
    var result = _context.Set<T>().ToList();

    if (result == null)
    {
      throw new Exception("Users Not Found!");
    }

    return result;
  }

  public T GetById<T>(int id) where T : class
  {
    var result = _context.Find<T>(id);

    if (result == null)
    {
      throw new Exception("Users Not Found!");
    }

    return result;
  }

  public void Create<T>(T entity) where T : class
  {
    _context.Add(entity);
    _context.SaveChanges();
  }

  public void Delete<T>(T entity) where T : class
  {
    _context.Remove<T>(entity);
    _context.SaveChanges();
  }
}