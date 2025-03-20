using System.Windows;
using System.Windows.Controls;

namespace Click_Snup
{
    public partial class LoginWindow : Window
    {
        private UserDatabaseService dbService;

        public LoginWindow()
        {
            InitializeComponent();
            dbService = new UserDatabaseService(); // Создаем экземпляр класса для работы с БД

            // Обработчики для проверки ввода текста в TextBox и PasswordBox
            txtUsername.TextChanged += TxtUsername_TextChanged;
            txtPassword.PasswordChanged += TxtPassword_PasswordChanged;
        }

        // Обработчик для TextBox (Логин)
        private void TxtUsername_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUsername.Text))
            {
                UsernamePlaceholder.Visibility = Visibility.Collapsed; // Скрываем подсказку
            }
            else
            {
                UsernamePlaceholder.Visibility = Visibility.Visible; // Показываем подсказку
            }
        }

        // Обработчик для PasswordBox (Пароль)
        private void TxtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword.Password))
            {
                PasswordPlaceholder.Visibility = Visibility.Collapsed; // Скрываем подсказку
            }
            else
            {
                PasswordPlaceholder.Visibility = Visibility.Visible; // Показываем подсказку
            }
        }

        // Обработчик для кнопки "Войти"
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Password;
            string role = ((ComboBoxItem)cmbRole.SelectedItem).Content.ToString(); // Получаем роль из ComboBox

            if (IsValidUser(username, password, role))
            {
                MainWindow mainWindow = new MainWindow(role); // Передаем роль в главное окно
                mainWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Неверные учетные данные!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик для кнопки "Зарегистрироваться"
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow registrationWindow = new RegistrationWindow();
            registrationWindow.Show();
            this.Close(); // Закрываем текущее окно
        }

        private bool IsValidUser(string username, string password, string role)
        {
            // Имитация базы данных пользователей
            if (role == "Администратор" && username == "admin" && password == "1234")
                return true;
            if (role == "Пользователь" && username == "user" && password == "1234")
                return true;

            return false;
        }
    }
}
