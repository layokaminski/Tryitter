public interface ITryRepository
{
  public IEnumerable<T> GetAll<T>() where T : class;
  public T GetById<T>(int id) where T : class;
  public void Create<T>(T entity) where T : class;
  public void Delete<T>(T entity) where T : class;
}