using Microsoft.EntityFrameworkCore;

namespace Tryitter.Repository
{
  public class TryRepository : ITryRepository
  {
    private DbContext _context { get; set; }

    public TryRepository(DbContext context)
    {
      _context = context;
    }
    public async Task<IEnumerable<T>> GetAll<T>() where T : class
    {
      return await Task.Run(() => _context.Set<T>());
    }

    public async Task<T?> GetById<T>(int id) where T : class
    {
      return await _context.FindAsync<T>(id);
    }

  public async Task<T> Create<T>(T entity) where T : class
  {
    return await Update(entity);
  }

  public async Task<T> Update<T>(T entity) where T : class
  {
    var updated = await Task.Run(() => _context.Update(entity));
    _context.SaveChanges();

    return updated.Entity;
  }

    public void Delete<T>(T entity) where T : class
    {
      _context.Remove<T>(entity);
      _context.SaveChanges();
    }
  }
}