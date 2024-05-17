using System.Windows;

namespace проект
{
    public partial class HomePage : Window
    {
        public User currentUser;
        public HomePage(User user)
        {
            InitializeComponent();
            currentUser = user;
            UserEmailTextBlock.Text = currentUser.Email;
        }

        public void AddGame_Click(object sender, RoutedEventArgs e)
        {
            AddGame addGame = new AddGame(currentUser);
            addGame.Show();
            this.Close();
        }

        public void EditGame_Click(object sender, RoutedEventArgs e)
        {
            EditGame editGame = new EditGame();
            editGame.Show();
            this.Close();
        }

        public void DeleteGame_Click(object sender, RoutedEventArgs e)
        {
            DeleteGame deleteGame = new DeleteGame(currentUser);
            deleteGame.Show();
            this.Close();
        }

        public void EditAchievement_Click(object sender, RoutedEventArgs e)
        {
            EditAchiev editAchiev = new EditAchiev(currentUser);
            editAchiev.Show();
            this.Close();
        }

        public void DeleteAchievement_Click(object sender, RoutedEventArgs e)
        {
            DeleteAchiev deleteAchiev = new DeleteAchiev(currentUser);
            deleteAchiev.Show();
            this.Close();
        }

        public void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            DeleteUser deleteUser = new DeleteUser(currentUser);
            deleteUser.Show();
            this.Close();
        }

        public void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public void AddCompany_Click(object sender, RoutedEventArgs e)
        {
            AddCompany addCompanyWindow = new AddCompany(currentUser);
            addCompanyWindow.Show();
            this.Close();
        }

        public void EditCompany_Click(object sender, RoutedEventArgs e)
        {
            EditCompany edit = new EditCompany(currentUser);
            edit.Show();
            this.Close();
        }

        public void DeleteCompany_Click(object sender, RoutedEventArgs e)
        {
            DeleteCompany deleteCompany = new DeleteCompany(currentUser);
            deleteCompany.Show();
            this.Close();
        }

        public void AddAchievement_Click(object sender, RoutedEventArgs e)
        {
            AddAchievements addAchievement = new AddAchievements(currentUser);
            addAchievement.Show();
            this.Close();
        }
    }
}