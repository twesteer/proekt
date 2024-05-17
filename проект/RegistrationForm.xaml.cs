using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Security.Cryptography;
using System.Text;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;

namespace проект
{
    public partial class RegistrationForm : Window
    {
        public MyDbContext context;

        public RegistrationForm()
        {
            InitializeComponent();
            context = new MyDbContext();
        }

        public void GoBackButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        public void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(LastNameTextBox.Text) ||
                    string.IsNullOrWhiteSpace(EmailTextBox.Text) ||
                    string.IsNullOrWhiteSpace(PhoneNumberTextBox.Text) ||
                    CountryComboBox.SelectedItem == null ||
                    string.IsNullOrWhiteSpace(PasswordTextBox.Password) ||
                    Date.SelectedDate == null)
                {
                    MessageBox.Show("Заполните все обязательные поля.");
                    return;
                }
                string email = EmailTextBox.Text;
                if (!IsValidEmail(email))
                {
                    MessageBox.Show("Пожалуйста, введите корректный адрес электронной почты.");
                    return;
                }
                string phoneNumber = PhoneNumberTextBox.Text;
                if (!IsValidPhoneNumber(phoneNumber))
                {
                    MessageBox.Show("Пожалуйста, введите корректный номер телефона, начинающийся с +7.");
                    if (phoneNumber.Length != 11)
                    {
                        MessageBox.Show("Номер телефона должен содержать ровно 11 цифр.");
                    }
                    return;
                }
                string password = PasswordTextBox.Password;
                if (!IsValidPassword(password))
                {
                    MessageBox.Show("Пароль должен содержать минимум 8 символов, хотя бы одну цифру, одну большую букву и один специальный символ.");
                    return;
                }
                if (context.Users.Any(u => u.Email == email))
                {
                    MessageBox.Show("Пользователь с таким email уже зарегистрирован.");
                    return;
                }
                if (context.Users.Any(u => u.PhoneNumber == phoneNumber))
                {
                    MessageBox.Show("Пользователь с таким номером телефона уже зарегистрирован.");
                    return;
                }
                if (!IsValidName(FirstNameTextBox.Text) || !IsValidName(LastNameTextBox.Text) || !IsValidName(MiddleNameTextBox.Text))
                {
                    MessageBox.Show("Имя, фамилия и отчество должны состоять только из букв.");
                    return;
                }

                // Определение типа пользователя на основе выбранной радио кнопки
                UserType userType = UserType.Regular; // По умолчанию обычный пользователь
                if (AdminRadioButton.IsChecked == true)
                {
                    userType = UserType.Admin; // Если выбрана радио кнопка для админа, устанавливаем тип пользователя как админ
                }

                User newUser = new User
                {
                    FirstName = FirstNameTextBox.Text,
                    LastName = LastNameTextBox.Text,
                    MiddleName = MiddleNameTextBox.Text,
                    Email = email,
                    PhoneNumber = phoneNumber,
                    Country = (string)((ComboBoxItem)CountryComboBox.SelectedItem).Content,
                    Password = HashPassword(password),
                    DateOfBirth = Date.SelectedDate ?? DateTime.MinValue,
                    HoursPlayed = 1,
                    IsBlocked = false,
                    UserType = userType // Устанавливаем тип пользователя

                };
                try
                {
                    // Вывод отладочной информации
                    Debug.WriteLine($"Пользователь: {newUser.FirstName} {newUser.LastName}");
                    Debug.WriteLine($"Количество заказов пользователя: {newUser.Orders?.Count}");

                    context.SaveChanges();
                    MessageBox.Show("Регистрация успешна!");
                }
                catch (DbUpdateException ex)
                {
                    MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}");
                    if (ex.InnerException != null)
                    {
                        MessageBox.Show($"Внутреннее исключение: {ex.InnerException.Message}");
                    }
                }
                context.Users.Add(newUser);
                context.SaveChanges();
                MessageBox.Show("Регистрация успешна!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при регистрации: {ex.InnerException.Message}");
            }
        }
        public bool IsValidPassword(string password)
        {
            return !string.IsNullOrWhiteSpace(password) && password.Length >= 8 && password.Any(char.IsDigit) && password.Any(char.IsUpper);
        }

        public bool IsValidEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) && email.Contains("@");
        }

        public bool IsValidPhoneNumber(string phoneNumber)
        {
            return !string.IsNullOrWhiteSpace(phoneNumber) && phoneNumber.StartsWith("+7");
        }

        public bool IsValidName(string name)
        {
            return !string.IsNullOrWhiteSpace(name) && name.All(char.IsLetter);
        }

        public bool RegisterUser(User user)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при регистрации пользователя: {ex.Message}");
                return false;
            }
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