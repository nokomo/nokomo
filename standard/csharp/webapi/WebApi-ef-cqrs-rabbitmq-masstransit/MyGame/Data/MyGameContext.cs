using System;
using Microsoft.EntityFrameworkCore;
using MyGame.Models;

namespace MyGame.Data;

public class MyGameContext(DbContextOptions<MyGameContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();
    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Game>()
          .Property(g => g.Price)
          .HasColumnType("decimal(18,2)");
    }
}
