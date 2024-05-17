using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace проект
{
    public partial class AddAchievements : Window
    {
        public MyDbContext context;
        public User currentUser;
        public AddAchievements(User user)
        {
            InitializeComponent();
            context = new MyDbContext();
            GameComboBox.ItemsSource = context.Games.ToList();
            GameComboBox.DisplayMemberPath = "Name";
            currentUser = user;
        }

        public void AddAchievement_Click(object sender, RoutedEventArgs e)
        {
            if (GameComboBox.SelectedItem is Game selectedGame)
            {
                Achievement newAchievement = new Achievement
                {
                    Name = AchievementsTextBox.Text,
                    GameId = selectedGame.Id
                };
                context.Achievements.Add(newAchievement);
                context.SaveChanges();
                MessageBox.Show("Ачивка успешно добавлена!");
                AchievementsTextBox.Text = string.Empty;
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



