using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Click_Snup
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string userRole;

        public MainWindow(string role)
        {
            InitializeComponent();
            userRole = role;
            WelcomeLabel.Content = $"Добро пожаловать, {role}";
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void OpenCatalog_Click(object sender, RoutedEventArgs e)
        {
            MainFrame.Navigate(new CatalogPage());
        }
        private void SetupUI()
        {
            if (userRole == "Пользователь")
            {
                // Отключаем админские кнопки (например, управление пользователями)
                Button adminPanelButton = FindName("btnAdminPanel") as Button;
                if (adminPanelButton != null)
                    adminPanelButton.Visibility = Visibility.Collapsed;
            }
        }
    }

}

