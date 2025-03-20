using System.Windows;

namespace Click_Snup
{
    public partial class RegistrationWindow : Window
    {
        private UserDatabaseService dbService;

        public RegistrationWindow()
        {
            InitializeComponent();
            dbService = new UserDatabaseService(); // Создаем экземпляр класса для работы с БД

            // Обработчики для проверки ввода текста
            txtUsername.TextChanged += TxtUsername_TextChanged;
            txtEmail.TextChanged += TxtEmail_TextChanged;
            txtPassword.PasswordChanged += TxtPassword_PasswordChanged;
        }

        // Обработчик для TextBox (Имя пользователя)
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

        // Обработчик для TextBox (Почта)
        private void TxtEmail_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text))
            {
                EmailPlaceholder.Visibility = Visibility.Collapsed; // Скрываем подсказку
            }
            else
            {
                EmailPlaceholder.Visibility = Visibility.Visible; // Показываем подсказку
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

        // Обработчик для кнопки "Зарегистрироваться"
        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUsername.Text;
            string email = txtEmail.Text;
            string password = txtPassword.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            bool isRegistered = dbService.RegisterUser(username, email, password);

            if (isRegistered)
            {
                MessageBox.Show("Регистрация прошла успешно!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            else
            {
                MessageBox.Show("Пользователь с такой почтой уже существует.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик для кнопки "Уже есть аккаунт? Войти"
        private void GoToLogin_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close(); // Закрываем окно регистрации
        }
    }
}
