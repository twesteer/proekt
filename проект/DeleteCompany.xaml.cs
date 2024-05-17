using System;
using System.Linq;
using System.Windows;

namespace проект
{
    public partial class DeleteCompany : Window
    {
        public MyDbContext context;
        public Company selectedCompany;
        public User currentUser;
        public DeleteCompany(User user)
        {
            InitializeComponent();
            context = new MyDbContext();
            LoadCompanies();
            currentUser = user;
        }

        public void LoadCompanies()
        {
            CompaniesDataGrid.ItemsSource = context.Companies.ToList();
        }

        public void CompaniesDataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedCompany = (Company)CompaniesDataGrid.SelectedItem;
            DeleteButton.IsEnabled = selectedCompany != null;
        }

        public void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCompany != null)
            {
                MessageBoxResult result = MessageBox.Show($"Вы уверены, что хотите удалить компанию '{selectedCompany.Name}'?", "Подтверждение удаления", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        context.Companies.Remove(selectedCompany);
                        context.SaveChanges();
                        MessageBox.Show("Компания успешно удалена!");
                        LoadCompanies();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка при удалении компании: {ex.Message}");
                    }
                }
            }
        }

        public void BackButton_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage(currentUser);
            homePage.Show();
            this.Close();
        }
    }
}