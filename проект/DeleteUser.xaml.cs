using System;
using System.Linq;
using System.Windows;

namespace проект
{
    public partial class DeleteUser : Window
    {
        public User currentUser;
        public MyDbContext context;
        public DeleteUser(User user)
        {
            InitializeComponent();
            currentUser = user;
            context = new MyDbContext();
            LoadUsers();
        }

        public void LoadUsers()
        {
            var users = context.Users.ToList();
            UserDataGrid.ItemsSource = users;
        }

        public void BackButton_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage(currentUser);
            homePage.Show();
            this.Close();
        }

        public void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (UserDataGrid.SelectedItem != null)
            {
                MessageBoxResult result = MessageBox.Show("Вы уверены, что хотите удалить пользователя?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        User selectedUser = (User)UserDataGrid.SelectedItem;

                        if (selectedUser != null)
                        {
                            context.Users.Remove(selectedUser);
                            context.SaveChanges();
                            MessageBox.Show("Пользователь успешно удален!");
                            LoadUsers(); 
                        }
                        else
                        {
                            MessageBox.Show("Не удалось найти пользователя для удаления.");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении пользователя: {ex.Message}");
                    }
                }
            }
        }
    }
}