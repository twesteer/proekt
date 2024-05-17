using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using Moq;
using System.Data.Entity;

namespace проект.Tests
{
    [TestFixture]
    public class EditGameTests
    {
        [Test]
        public void Test_EditGameButton_Click()
        {
            // Arrange
            var game = new Game
            {
                Id = 1,
                Name = "Game 1",
                YearOfRelease = 2000,
                Genre = "Action",
                AgeRestrictions = 16,
                Price = 29.99m,
                Photo = null
            };

            var gamesList = new ObservableCollection<Game> { game };
            var contextMock = new Mock<MyDbContext>();
            var gamesDbSetMock = new Mock<DbSet<Game>>();
            contextMock.Setup(x => x.Games).Returns(gamesDbSetMock.Object);
            gamesDbSetMock.Setup(x => x.Find(game.Id)).Returns(game);

            var editGameWindow = new EditGame();
            editGameWindow.context = contextMock.Object;
            editGameWindow.GamesList = gamesList;
            editGameWindow.SelectedGame = game;

            editGameWindow.NewNameTextBox.Text = "Updated Game 1";
            editGameWindow.NewReleaseYearTextBox.Text = "2020";
            editGameWindow.NewGenreTextBox.Text = "Adventure";
            editGameWindow.NewAgeRestrictionsComboBox.Text = "12";
            editGameWindow.NewPriceTextBox.Text = "39.99";

            // Act
            editGameWindow.EditGameButton_Click(null, null);

            // Assert
            Assert.AreEqual("Updated Game 1", game.Name);
            Assert.AreEqual(2020, game.YearOfRelease);
            Assert.AreEqual("Adventure", game.Genre);
            Assert.AreEqual(12, game.AgeRestrictions);
            Assert.AreEqual(39.99m, game.Price);
        }

        [Test]
        public void Test_ParseAgeRestrictions_ValidInput()
        {
            // Arrange
            var editGameWindow = new EditGame();

            // Act
            var result = editGameWindow.ParseAgeRestrictions("16");

            // Assert
            Assert.AreEqual(16, result);
        }

        [Test]
        public void Test_ParseAgeRestrictions_ValidInputWithPlus()
        {
            // Arrange
            var editGameWindow = new EditGame();

            // Act
            var result = editGameWindow.ParseAgeRestrictions("12+");

            // Assert
            Assert.AreEqual(12, result);
        }

        [Test]
        public void Test_ParseAgeRestrictions_InvalidInput()
        {
            // Arrange
            var editGameWindow = new EditGame();

            // Act and Assert
            Assert.Throws<FormatException>(() => editGameWindow.ParseAgeRestrictions("abc"));
        }
    }
}
