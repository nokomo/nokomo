using MyGame.Endpoints;
using MyGame.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddValidation();

builder.Services.AddDbContextAndSqlServer(builder.Configuration);

var app = builder.Build();

app.MapGamesEndpoints();

app.MapGenresEndpoints();

// Apply last migrations to database
app.MigrationDb();

app.AddSeeding();

app.Run();
