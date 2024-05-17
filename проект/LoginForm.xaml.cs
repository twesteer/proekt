using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace проект
{
    public partial class LoginForm : Window
    {
        public MyDbContext context; 
        public LoginForm()
        {
            InitializeComponent();
            context = new MyDbContext(); 
        }

        public void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string enteredUsername = EmailTextBox.Text;
            string enteredPassword = PasswordTextBox.Password;

            if (AuthenticateUser(enteredUsername, enteredPassword))
            {
                User currentUser = context.Users.FirstOrDefault(u => u.Email == enteredUsername);
                if (currentUser.IsBlocked)
                {
                    MessageBox.Show("Ваш аккаунт был заблокирован по причине");
                    return;
                }

                if (currentUser.UserType == UserType.Admin)
                {
                    HomePage homePage = new HomePage(currentUser);
                    homePage.Show();
                }
                else
                {
                    ProductsPage productsPage = new ProductsPage();
                    productsPage.Show();
                }

                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка входа. Пожалуйста, проверьте логин и пароль.");
            }
        }


        public bool AuthenticateUser(string email, string password)
        {
            User user = context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                string hashedPassword = HashPassword(password);
                if (hashedPassword == user.Password)
                {
                    return true;
                }
            }
            return false;
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public void ShowPassword_Click(object sender, RoutedEventArgs e)
        {
            if (passwordText.Visibility == Visibility.Collapsed)
            {
                PasswordTextBox.Visibility = Visibility.Collapsed; 
                passwordText.Text = PasswordTextBox.Password; 
                passwordText.Visibility = Visibility.Visible; 
                ShowPassword.Content = "Скрыть пароль";
            }
            else
            {
                passwordText.Visibility = Visibility.Collapsed; 
                PasswordTextBox.Visibility = Visibility.Visible; 
                ShowPassword.Content = "Показать пароль";
            }
        }
        public void OtherTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            PasswordTextBox.Password = passwordText.Text;
        }
    }
}