using MyGame.Endpoints;
using MyGame.Data;
using MyGame;
using MyGame.Services;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

builder.Services.AddDbContextAndSqlServer(builder.Configuration);

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<GameProfile>();
    config.AddProfile<GenreProfile>();
});

builder.Services.AddScoped<IGameService, GameService>();

var app = builder.Build();

app.MapGamesEndpoints();

app.MapGenresEndpoints();

// Apply last migrations to database
app.MigrationDb();

app.AddSeeding();

app.Run();
