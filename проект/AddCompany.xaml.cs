using System;
using System.Windows;

namespace проект
{
    public partial class AddCompany : Window
    {
        public MyDbContext context; 
        public User currentUser;
        public AddCompany(User user)
        {
            InitializeComponent();
            currentUser = user;
            context = new MyDbContext(); 
        }

        public void AddCompanyButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(CompanyNameTextBox.Text) || string.IsNullOrEmpty(GameCountTextBox.Text) 
                    || string.IsNullOrEmpty(RatingComboBox.Text) || RatingComboBox.SelectedItem == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля.");
                    return;
                }
                Company newCompany = new Company
                {
                    Name = CompanyNameTextBox.Text,
                    NumberOfGames = Convert.ToInt32(GameCountTextBox.Text),
                    Rating = Convert.ToInt32(RatingComboBox.Text)
                };
                context.Companies.Add(newCompany);
                context.SaveChanges();
                MessageBox.Show("Компания успешно добавлена!");
                CompanyNameTextBox.Text = "";
                GameCountTextBox.Text = "";
                RatingComboBox.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении компании: {ex.Message}");
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