using Microsoft.EntityFrameworkCore;
using MyGame.Models;

namespace MyGame.Data;

public static class DataExtensions
{
  public static void MigrationDb(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<MyGameContext>();
    context.Database.Migrate();
  }

  public static void AddDbContextAndSqlServer(
                        this IServiceCollection services, IConfiguration configuration)
  {
    var connectionString = configuration.GetConnectionString("MyStoreDb") ??
             throw new InvalidOperationException("Connection string 'MyStoreDb' not found.");
    services.AddDbContext<MyGameContext>(options =>
      options.UseNpgsql(connectionString));
  }

  public static void AddSeeding(this WebApplication app)
  {
    using var scope = app.Services.CreateScope();
    var context = scope.ServiceProvider.GetRequiredService<MyGameContext>();
    if (!context.Set<Genre>().Any())
    {
      context.Set<Genre>().AddRange(
          new Genre { Name = "Fighting" },
          new Genre { Name = "RPG" },
          new Genre { Name = "Platformer" },
          new Genre { Name = "Racing" },
          new Genre { Name = "Sports" }
      );
      context.SaveChanges();
    }
  }
}
