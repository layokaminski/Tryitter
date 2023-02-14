using Tryitter.Context;
using Tryitter.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var Configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<DbContext>(options =>
{
    options.UseSqlServer(Configuration.GetConnectionString(@"
        Server=127.0.0.1;
        User=SA;
        Password=Tryitter123;
        Database=Tryitter;
        trustServerCertificate=true;
    "));
});
builder.Services.AddDbContext<DbContext, TryitterDB>();
builder.Services.AddScoped<DbContext, TryitterDB>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }
