using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace проект
{
    public partial class EditAchiev : Window
    {
        public static EditAchivOkno Okno;
        public MyDbContext context;
        public User currentUser;
        public ObservableCollection<Game> GamesList { get; set; }
        public EditAchiev(User user)
        {
            InitializeComponent();
            context = new MyDbContext();
            currentUser = user;
            GamesList = new ObservableCollection<Game>(context.Games.ToList());
            GameDataGrid.ItemsSource = GamesList;
            GameDataGrid.DisplayMemberPath = "Name"; 
        }

        public void EditAchievement_Click(object sender, RoutedEventArgs e)
        {
            if (GameDataGrid.SelectedItem != null)
            {
                if (GameDataGrid.SelectedItem is Game selectedGame)
                {
                    int selectedGameId = selectedGame.Id;
                        Okno = new EditAchivOkno(currentUser, selectedGameId);
                        Okno.Show();
                }
            }
        }

        public void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage(currentUser);
            homePage.Show();
            this.Close();
        }

        public void GameDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GameDataGrid.SelectedItem != null)
            {
                if (GameDataGrid.SelectedItem is Game selectedGame)
                {
                    MessageBox.Show($"Выбрана игра: {selectedGame.Name}, ID: {selectedGame.Id}");
                }
            }
        }
    }
}