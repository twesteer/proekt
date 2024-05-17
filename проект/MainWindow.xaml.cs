using System.Windows;

namespace проект
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Close();
        }

        public void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            RegistrationForm registrationForm = new RegistrationForm();
            registrationForm.Show();
            this.Close();
        }
    }
}