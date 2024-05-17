using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls; // Не забудьте про это пространство имен

namespace проект
{
    public partial class DeleteGame : Window
    {
        public User currentUser;
        public MyDbContext context;

        public DeleteGame(User user)
        {
            InitializeComponent();
            context = new MyDbContext();
            currentUser = user;
            LoadUserGames();
        }

        public void LoadUserGames()
        {
            ObservableCollection<Game> userGames = new ObservableCollection<Game>(context.Games);
            GamesDataGrid.ItemsSource = userGames;
        }

        public bool DeleteGameFromDatabase(int gameId)
        {
            try
            {
                var game = context.Games.Find(gameId);
                if (game != null)
                {
                    context.Games.Remove(game);
                    context.SaveChanges();
                    return true;
                }
                else
                {
                    MessageBox.Show("Game not found");
                    return false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"удаление {ex.Message}");
                return false;
            }
        }

        public bool DeleteSelectedGameFromDataGrid(DataGrid dataGrid) // Изменение типа параметра
        {
            // Проверка, выбран ли элемент
            if (dataGrid.SelectedItem == null)
            {
                MessageBox.Show("Выберите игру для удаления!");
                return false;
            }

            // Получение ID выбранной игры
            var selectedGame = (Game)dataGrid.SelectedItem;
            int gameId = selectedGame.Id;

            // Удаление игры из БД
            bool deleteResult = DeleteGameFromDatabase(gameId);

            // Обновление DataGrid
            if (deleteResult)
            {
                dataGrid.Items.Remove(selectedGame);
                dataGrid.Items.Refresh();
            }

            return deleteResult;
        }

        public void GamesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GamesDataGrid.SelectedItem != null)
            {
                DeleteButton.IsEnabled = true;
            }
            else
            {
                DeleteButton.IsEnabled = false;
            }
        }

        public void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Game selectedGame = (Game)GamesDataGrid.SelectedItem;
            MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить компанию '{selectedGame.Name}'?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                if (selectedGame != null)
                {
                    int gameId = selectedGame.Id;
                    DeleteGameFromDatabase(gameId); // Вызываем метод удаления игры из базы данных

                    MessageBox.Show("Игра успешно удалена!");

                    LoadUserGames();
                }
            }
        }

        public void BackButton_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage(currentUser);
            homePage.Show();
            this.Close();
        }
    }
}
