using Tryitter.Models;

namespace Tryitter.Repository
{
  public interface IPostRepository
  {
    public Task<IEnumerable<Post>> GetAll();
    public Task<Post?> GetById(int id);
    public Task<Post> Create(Post post);
    public Task<Post> Update(Post entity, int id);
    public void Delete(int id);
  }
}
