using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;
using проект;

namespace проект.Tests
{
    [TestClass]
    public class EditGameTests
    {
        [TestMethod]
        public void EditGameInDatabase_ShouldReturnTrue()
        {
            // Arrange
            var editGameWindow = new EditGame(); // Создаем экземпляр класса EditGame
            var game = new Game
            {
                // Устанавливаем данные для новой игры
                Name = "OldName",
                YearOfRelease = 2020,
                Genre = "OldGenre",
                AgeRestrictions = 18,
                Price = 50m
            };

            var newName = "NewName";
            var newReleaseYear = 2024;
            var newGenre = "NewGenre";
            var newAgeRestrictions = 13;
            var newPrice = 150m;

            // Act
            var result = editGameWindow.EditGameInDatabase(game, newName, newReleaseYear, newGenre, newAgeRestrictions, newPrice);

            // Assert
            Assert.IsTrue(result);
        }
        [TestMethod]
        public void DeleteGameFromDatabase_WhenValidGameIdPassed_ReturnsTrue()
        {
            // Arrange
            User user = new User(); // Создаем фиктивного пользователя
            int gameId = 1; // ID игры

            DeleteGame deleteGameWindow = new DeleteGame(user);

            // Act
            bool result = deleteGameWindow.DeleteGameFromDatabase(gameId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteGameFromDatabase_WhenNullGamePassed_ReturnsFalse()
        {
            // Arrange
            User user = new User(); // Создаем фиктивного пользователя
            Game game = null; // Передаем null в качестве игры
            DeleteGame deleteGameWindow = new DeleteGame(user);

            // Act
            bool result = deleteGameWindow.DeleteGameFromDatabase(game);

            // Assert
            Assert.IsFalse(result);
        }

    }
}
