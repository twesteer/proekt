using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace проект
{
    public partial class EditCompany : Window
    {
        public User currentUser;
        public MyDbContext context;
        public Company selectedCompany;
        public ObservableCollection<Company> CompaniesList { get;  set; }
        public EditCompany(User user)
        {
            InitializeComponent();
            context = new MyDbContext();
            currentUser = user;
            CompaniesList = new ObservableCollection<Company>(context.Companies.ToList());
            CompanyDataGrid.ItemsSource = CompaniesList;
        }

        public void CompanyDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CompanyDataGrid.SelectedItem is Company selectedCompany)
            {
                CompanyNameTextBox.Text = selectedCompany.Name;
                GameCountTextBox.Text = selectedCompany.NumberOfGames.ToString();
            }
        }

        public void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage(currentUser);
            homePage.Show();
            this.Close();
        }

        public void SaveChangesButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(CompanyNameTextBox.Text) && string.IsNullOrEmpty(GameCountTextBox.Text) != null)
                {
                    selectedCompany = (Company)CompanyDataGrid.SelectedItem;

                    if (selectedCompany != null)
                    {
                        selectedCompany.Name = CompanyNameTextBox.Text;

                        if (int.TryParse(GameCountTextBox.Text, out int numberOfGames))
                        {
                            selectedCompany.NumberOfGames = numberOfGames;
                        }
                        else
                        {
                            MessageBox.Show("Введите корректное число игр!");
                            return;
                        }
                        context.SaveChanges();
                        MessageBox.Show("Изменения сохранены!");
                    }
                    else
                    {
                        MessageBox.Show("Ошибка: компания не выбрана.");
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля!");
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
            }
        }
    }
}