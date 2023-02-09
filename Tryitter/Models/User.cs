using System.ComponentModel.DataAnnotations;

namespace Tryitter.Models
{
  public class User
  {
    [Key]
    public int UserID { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public ICollection<Post> Posts { get; set; }
  }
}
