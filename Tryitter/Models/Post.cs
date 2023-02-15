using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tryitter.Models
{
  public class Post
  {
    [Key]
    public int PostId { get; set; }
    [MaxLength(300)]
    public string Description { get; set; }
    [ForeignKey("UserId")]
    public int UserId { get; set; }
  }
}
