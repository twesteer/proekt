using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace проект
{
    public partial class CartWindow : Window
    {
        public List<Game> _cartItems;

        public CartWindow(List<Game> cartItems)
        {
            InitializeComponent();
            _cartItems = cartItems;
            UpdateCart();
        }

        public void OnDeleteButtonClick(object sender, RoutedEventArgs e)
        {
            // Получаем выбранный элемент из ListBox
            Game selectedGame = (Game)cartItemsListBox.SelectedItem;

            // Удаляем выбранный элемент из коллекции _cartItems, если он не равен null
            if (selectedGame != null)
            {
                _cartItems.Remove(selectedGame);
                UpdateCart(); // Обновляем отображение корзины
            }
            else
            {
                MessageBox.Show("Выберите товар для удаления из корзины.");
            }
        }

        public void UpdateCart()
        {
            cartItemsListBox.ItemsSource = null; // Очищаем текущий источник данных
            cartItemsListBox.ItemsSource = _cartItems; // Устанавливаем обновленный источник данных
        }

        // Добавляем публичный доступ к cartItemsListBox
        public ListBox CartItemsListBox => cartItemsListBox;
    }
}
