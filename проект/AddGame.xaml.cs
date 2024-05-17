using System;
using System.IO;
using System.Linq;
using System.Windows;
using Microsoft.Win32;

namespace проект
{
    public partial class AddGame : Window
    {
        public User currentUser;
        public MyDbContext context;
        public byte[] gameImageBytes;

        public AddGame(User user)
        {
            InitializeComponent();
            context = new MyDbContext();
            currentUser = user;
            CompanyComboBox.ItemsSource = context.Companies.ToList();
        }

        public void AddGameButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(GameNameTextBox.Text) || string.IsNullOrEmpty(ReleaseYearComboBox.Text) ||
                    string.IsNullOrEmpty(GenreComboBox.Text) || string.IsNullOrEmpty(AgeRestrictionsComboBox.Text) ||
                    string.IsNullOrEmpty(PriceTextBox.Text) || CompanyComboBox.SelectedItem == null || gameImageBytes == null)
                {
                    MessageBox.Show("Пожалуйста, заполните все поля и добавьте изображение.");
                    return;
                }
                if (context.Games.Any(g => g.Name.Equals(GameNameTextBox.Text, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageBox.Show("Игра с таким названием уже существует.");
                    return;
                }

                if (!int.TryParse(ReleaseYearComboBox.Text, out int releaseYear) ||
                    !decimal.TryParse(PriceTextBox.Text, out decimal price))
                {
                    MessageBox.Show("Пожалуйста, убедитесь, что введенные данные имеют правильный формат.");
                    return;
                }

                Company selectedCompany = (Company)CompanyComboBox.SelectedItem;
                int ageRestrictions = ParseAgeRestrictions(AgeRestrictionsComboBox.Text);
                Game newGame = new Game
                {
                    Name = GameNameTextBox.Text,
                    YearOfRelease = releaseYear,
                    Genre = GenreComboBox.Text,
                    AgeRestrictions = ageRestrictions,
                    Price = price,
                    CompanyId = selectedCompany.Id,
                    // Сохранение изображения
                    Photo = gameImageBytes
                };
                context.Games.Add(newGame);
                context.SaveChanges();
                MessageBox.Show("Игра успешно добавлена!");
                GameNameTextBox.Text = "";
                ReleaseYearComboBox.Text = "";
                GenreComboBox.Text = "";
                AgeRestrictionsComboBox.Text = "";
                PriceTextBox.Text = "";
                CompanyComboBox.SelectedItem = null;
                gameImageBytes = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении игры: {ex.Message}");
            }
        }

        // Обработчик нажатия кнопки для добавления изображения
        public void AddImageButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image Files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            if (openFileDialog.ShowDialog() == true)
            {
                using (FileStream fileStream = new FileStream(openFileDialog.FileName, FileMode.Open, FileAccess.Read))
                {
                    using (BinaryReader binaryReader = new BinaryReader(fileStream))
                    {
                        gameImageBytes = binaryReader.ReadBytes((int)fileStream.Length);
                    }
                }
            }
        }

        public void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            HomePage homePage = new HomePage(currentUser);
            homePage.Show();
            this.Close();
        }

        public int ParseAgeRestrictions(string input)
        {
            if (input.EndsWith("+"))
            {
                input = input.TrimEnd('+');
            }

            if (int.TryParse(input, out int ageRestrictions))
            {
                return ageRestrictions;
            }

            throw new ArgumentException("Неверный формат возрастных ограничений.");
        }
    }
}
