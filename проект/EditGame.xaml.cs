using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace проект
{
    public partial class EditGame : Window
    {
        public MyDbContext context;

        public ObservableCollection<Game> GamesList { get; set; }
        public Game SelectedGame { get; set; }

        public EditGame()
        {
            InitializeComponent();

            context = new MyDbContext();
            GamesList = new ObservableCollection<Game>(context.Games.ToList());
            GameDataGrid.ItemsSource = GamesList;
            GameDataGrid.DisplayMemberPath = "Name";
        }

        public void EditGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SelectedGame == null)
                {
                    MessageBox.Show("Ошибка: Игра не выбрана.");
                    return;
                }

                string newName = NewNameTextBox.Text;
                int newReleaseYear = Convert.ToInt32(NewReleaseYearTextBox.Text);
                string newGenre = NewGenreTextBox.Text;
                int newAgeRestrictions = ParseAgeRestrictions(NewAgeRestrictionsComboBox.Text);
                decimal newPrice = Convert.ToDecimal(NewPriceTextBox.Text);

                bool isSaved = EditGameInDatabase(SelectedGame, newName, newReleaseYear, newGenre, newAgeRestrictions, newPrice);

                if (isSaved)
                {
                    MessageBox.Show("Игра успешно изменена!");
                    RefreshDataGrid();
                }
                else
                {
                    MessageBox.Show("Ошибка при изменении игры. Данные не были сохранены.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении игры: {ex.Message}");
            }
        }


        public bool EditGameInDatabase(Game game, string newName, int newReleaseYear, string newGenre, int newAgeRestrictions, decimal newPrice)
        {
            try
            {
                game.Name = newName;
                game.YearOfRelease = newReleaseYear;
                game.Genre = newGenre;
                game.AgeRestrictions = newAgeRestrictions;
                game.Price = newPrice;
                context.SaveChanges();
                return true; // Возвращаем true при успешном сохранении данных
            }
            catch (Exception ex)
            {
                // В случае исключения возвращаем false и выводим сообщение об ошибке
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
                return false;
            }
        }


        public int ParseAgeRestrictions(string input)
        {
            if (input.EndsWith("+"))
            {
                input = input.TrimEnd('+');
            }
            if (int.TryParse(input, out int ageRestrictions))
            {
                return ageRestrictions;
            }
            throw new ArgumentException("Неверный формат возрастных ограничений.");
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            //HomePage homePage = new HomePage();
            //homePage.Show();
            //this.Close();
        }

        public void GameDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GameDataGrid.SelectedItem != null)
            {
                SelectedGame = (Game)GameDataGrid.SelectedItem;
                MessageBox.Show($"Выбрана игра: {SelectedGame.Name}");

                if (SelectedGame.Photo != null && SelectedGame.Photo.Length > 0)
                {
                    try
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.BeginInit();
                        bitmap.StreamSource = new MemoryStream(SelectedGame.Photo);
                        bitmap.EndInit();

                        CurrentPhotoImage.Source = bitmap;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при загрузке фото: {ex.Message}");
                    }
                }
                else
                {
                    CurrentPhotoImage.Source = null;
                }
            }
        }

        public void RefreshDataGrid()
        {
            GamesList = new ObservableCollection<Game>(context.Games.ToList());
            GameDataGrid.ItemsSource = GamesList;
        }

        public void NewPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog();
                openFileDialog.Filter = "Image files (*.jpg, *.jpeg, *.png, *.gif)|*.jpg;*.jpeg;*.png;*.gif";
                if (openFileDialog.ShowDialog() == true)
                {
                    byte[] photoBytes = File.ReadAllBytes(openFileDialog.FileName);
                    SelectedGame.Photo = photoBytes;
                    context.SaveChanges();

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = new MemoryStream(photoBytes);
                    bitmap.EndInit();
                    CurrentPhotoImage.Source = bitmap;

                    MessageBox.Show("Фотография успешно изменена!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при изменении фотографии: {ex.Message}");
            }
        }
    }
}
