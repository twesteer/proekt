using System;
using System.Data.Entity;

namespace проект
{
    public class MyDbContext : DbContext
    {
    
            public DbSet<Company> Companies { get; set; }
            public DbSet<Game> Games { get; set; }
            public DbSet<User> Users { get; set; }
            public DbSet<Order> Orders { get; set; }
            public DbSet<Achievement> Achievements { get; set; }
            public DbSet<Score> Scores { get; set; }
            public DbSet<Currency> Currencies { get; set; }

        


            
    }
}