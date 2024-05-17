using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace проект
{
    public partial class DeleteAchiev : Window
    {
        public MyDbContext context;
        public Achievement selectedAchievement;
        public User currentUser;
        public DeleteAchiev(User user)
        {
            InitializeComponent();
            context = new MyDbContext();
            LoadAchievements();
            currentUser = user;
        }
        public void LoadAchievements()
        {
            var achievementsWithGames = context.Achievements
                
                .Select(a => new
                {
                    ID = a.Id,
                    Name = a.Name,
                    GameID = a.Game.Id,
                    GameName = a.Game.Name
                })
                .AsEnumerable() 
                .Select(a => new
                {
                    ID = a.ID,
                    Name = a.Name,
                    GameID = a.GameID,
                    GameName = a.GameName
                })
                .ToList();
            AchievementsDataGrid.ItemsSource = achievementsWithGames;
        }
        public void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAchievement != null)
            {
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить ачивку '{selectedAchievement.Name}'?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        context.Achievements.Remove(selectedAchievement);
                        context.SaveChanges();
                        MessageBox.Show("Компания успешно удалена!");
                        LoadAchievements();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении компании: {ex.Message}");
                    }
                }
            }
        }
        public void AchievementDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        public void BackButton_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage(currentUser);
            homePage.Show();
            this.Close();
        }
    }
}