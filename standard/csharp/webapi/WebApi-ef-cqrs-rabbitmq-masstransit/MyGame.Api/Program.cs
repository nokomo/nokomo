using MyGame.Data;
using MyGame.Services;
using Scalar.AspNetCore;
using FluentValidation;
using MediatR;
using MyGame.Behaviors;
using MyGame.Api.Messaging;
using MyGame.Api.Endpoints;
using MyGame.Api.Exceptions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddValidation();

builder.Services.AddDbContextAndSqlServer(builder.Configuration);

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

builder.Services.AddScoped<IGameService, GameService>();

// MassTransit configuration
builder.Services.AddMassTransitConfigurations(builder.Configuration);

var app = builder.Build();

app.UseMiddleware<ValidationExceptionMiddleware>();

app.MapOpenApi();

app.MapScalarApiReference();
app.MapGamesEndpoints();

app.MapGenresEndpoints();

// Apply last migrations to database
app.MigrationDb();

app.AddSeeding();

app.Run();
