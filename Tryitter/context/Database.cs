using Microsoft.EntityFrameworkCore;

public class TryitterDB : DbContext
{
  public DbSet<User> Users { get; set; }
  public DbSet<Post> Posts { get; set; }
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if(!optionsBuilder.IsConfigured)
    {
      optionsBuilder.UseSqlServer(@"
        Server=127.0.0.1;
        User=SA;
        Password=Tryitter123;
        Database=Tryitter;
        trustServerCertificate=true;
      ");
    }
  }
}