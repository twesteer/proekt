using System;
using System.Data.Entity;

namespace тест
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

        


            public void UpdateGame(Game updatedGame)
            {
                if (updatedGame == null)
                {
                    throw new ArgumentNullException(nameof(updatedGame));
                }

                var existingGame = Games.Find(updatedGame.Id);

                if (existingGame != null)
                {
                    Entry(existingGame).CurrentValues.SetValues(updatedGame);
                    SaveChanges();
                }

                else
                {
                    throw new InvalidOperationException("Игра не найдена в базе данных.");
                }
            }
    }
}