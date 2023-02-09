namespace Tryitter.Repository
{
  public interface ITryRepository
  {
    public Task<IEnumerable<T>> GetAll<T>() where T : class;
    public Task<T?> GetById<T>(int id) where T : class;
    public Task<T> Create<T>(T entity) where T : class;
    public Task<T> Update<T>(T entity) where T : class;
    public void Delete<T>(T entity) where T : class;
  }
}
