using Tryitter.Models;

namespace Tryitter.Repository
{
  public interface IPostRepository
  {
    public Task<IEnumerable<Post>> GetAll();
    public Task<IEnumerable<Post>> GetAllById(int id);
    public Task<Post?> GetLastById(int id);
    public Task<Post?> GetById(int id);
    public Task<Post> Create(Post entity);
    public Task<Post> Update(Post entity, int id);
    public void Delete(int id);
  }
}
