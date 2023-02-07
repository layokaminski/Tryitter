using Microsoft.EntityFrameworkCore;

public class TryitterDB : DbContext
{
  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
  {
    if(!optionsBuilder.IsConfigured)
    {
      optionsBuilder.UseSqlServer(@"
        Server=127.0.0.1;
        Password=Tryitter123;
        Database=Tryitter;
        trustServerCertificate=true;
      ");
    }
  }
}