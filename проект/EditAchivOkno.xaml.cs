using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace проект
{
    public partial class EditAchivOkno : Window
    {
        public MyDbContext context;
        public User currentUser;
        public int gameId; 
        public ObservableCollection<Achievement> AchievementsList { get; set; }
        public EditAchivOkno(User user, int gameId)
        {
            InitializeComponent();
            context = new MyDbContext();
            currentUser = user;
            this.gameId = gameId; 
            GameComboBox.ItemsSource = context.Games.ToList();
            GameComboBox.DisplayMemberPath = "Name";
            LoadAchievementsForGame();
        }

        public void LoadAchievementsForGame()
        {
            AchievementsList = new ObservableCollection<Achievement>(context.Achievements.Where(a => a.Game.Id == gameId) .ToList());
            AchievementDataGrid.ItemsSource = AchievementsList;
            AchievementDataGrid.DisplayMemberPath = "Name";
        }

        public void LoadAchievementsForSelectedGame()
        {
            if (GameComboBox.SelectedItem is Game selectedGame)
            {
                AchievementsList = new ObservableCollection<Achievement>(context.Achievements.Where(a => a.GameId == selectedGame.Id).ToList());
                AchievementDataGrid.ItemsSource = AchievementsList;
                AchievementDataGrid.DisplayMemberPath = "Name";
            }
        }

        public void AchievementDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (AchievementDataGrid.SelectedItem is Achievement selectedAchievement)
            {
                AchievementsTextBox.Text = selectedAchievement.Name;
                GameComboBox.SelectedItem = selectedAchievement.Game;
            }
        }

        public void EditAchievement_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(AchievementsTextBox.Text) || GameComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Заполните все поля!");
                    return;
                }
                Achievement selectedAchievement = (Achievement)AchievementDataGrid.SelectedItem;
                if (selectedAchievement != null)
                {
                    MessageBox.Show($"Статус до изменений: {context.Entry(selectedAchievement).State}");
                    selectedAchievement.Name = AchievementsTextBox.Text;
                    Game selectedGame = (Game)GameComboBox.SelectedItem;
                    if (selectedGame != null)
                    {
                        selectedAchievement.Game = selectedGame;
                    }
                    else
                    {
                        MessageBox.Show("Выберите игру!");
                        return;
                    }
                    MessageBox.Show($"Статус после изменений: {context.Entry(selectedAchievement).State}");
                    context.SaveChanges();
                    MessageBox.Show("Изменения сохранены!");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Ошибка: ачивка не выбрана.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
            }
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage(currentUser);
            homePage.Show();
            this.Close();
        }
    }
}