using Tryitter.Models;

namespace Tryitter.Repository
{
  public interface IUserRepository
  {
    public Task<IEnumerable<User>> GetAllUsers();
    public Task<User?> GetUserById(int id);
    public Task<User> CreateUser(User entity);
    public Task<User> UpdateUser(User entity, int id);
    public void DeleteUser(int id);
  }
}
