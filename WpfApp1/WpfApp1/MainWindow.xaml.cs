using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp1.Services;
using WpfApp1.Models;
using static System.Net.Mime.MediaTypeNames;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            Register(sender, e);
        }
        private void Login(object sender, RoutedEventArgs e)
        {
            UserManager userManager = new UserManager();
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            // Input validation
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ForgotPasswordTextBlock.Text = "Please enter both username and password.";
                ForgotPasswordTextBlock.Foreground = Brushes.Red;
                ForgotPasswordTextBlock.Visibility = Visibility.Visible;

                return;
            }

            // Create credentials object
            var credentials = new WpfApp1.Models.Registration
            {
                Username = username,
                Password = password
            };

            // Attempt login
            bool loginSuccess = userManager.Trylogin(credentials);

            if (loginSuccess)
            {
                ForgotPasswordTextBlock.Text = "Login correct.";
                ForgotPasswordTextBlock.Foreground = Brushes.Green;
                ForgotPasswordTextBlock.Visibility = Visibility.Visible;
            }
            else
            {
                ForgotPasswordTextBlock.Text = "Invalid username or password.";
                ForgotPasswordTextBlock.Foreground = Brushes.Red;
                ForgotPasswordTextBlock.Visibility = Visibility.Visible;
                PasswordBox.Clear();
            }
        }
    }
}