﻿using Microsoft.EntityFrameworkCore;
using TestWebApplication.Entities;

namespace TestWebApplication.Data;

public class GameStoreContext(DbContextOptions<GameStoreContext> options)
: DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();
}
