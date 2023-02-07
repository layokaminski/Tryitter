using System.ComponentModel.DataAnnotations;

public class Post
{
  [Key]
  public int PostId { get; set; }
  public string Description { get; set; }
  public int UserId { get; set; }
}