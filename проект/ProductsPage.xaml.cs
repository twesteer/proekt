using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace проект
{
    public partial class ProductsPage : Window
    {
        public MyDbContext _dbContext;
        public ObservableCollection<Game> _games;
        public List<Game> _cartItems;

        public ProductsPage()
        {
            InitializeComponent();

            _dbContext = new MyDbContext();
            _games = new ObservableCollection<Game>(GetGames());
            _cartItems = new List<Game>();
            this.DataContext = this; // Set DataContext of the page to itself
        }

        public ObservableCollection<Game> Games
        {
            get { return _games; }
            set { _games = value; }
        }

        public List<Game> GetGames()
        {
            return _dbContext.Games.ToList(); // Retrieve all games from the database
        }

        public void OnAddToCartButtonClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var game = (Game)button.DataContext;

            _cartItems.Add(game);

            MessageBox.Show($"Added {game.Name} to cart!");
        }

        public void OnCartButtonClick(object sender, RoutedEventArgs e)
        {
            // Show cart contents logic here
            MessageBox.Show("Cart contents will be displayed here!");
        }

        public void OnShowCartButtonClick(object sender, RoutedEventArgs e)
        {
            CartWindow cartWindow = new CartWindow(_cartItems);
            cartWindow.Show();
        }
    }
}
