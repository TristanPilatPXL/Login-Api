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
        private int count = 3; // Move counter to class level

        public MainWindow()
        {
            InitializeComponent();
        }
        private void Cancel(object sender, RoutedEventArgs e)
        {
            Close();
        }

        
        private void Login(object sender, RoutedEventArgs e)
        {

            if (count==0)
            {
                ForgotPasswordTextBlock.Text = "No more attempts left.";
                ForgotPasswordTextBlock.Foreground = Brushes.Red;
                ForgotPasswordTextBlock.Visibility = Visibility.Visible;
                return;
            }

            UserManager userManager = new UserManager();
            var username = UsernameTextBox.Text;
            var password = PasswordBox.Password;

            // Input validation
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                count--;
                ForgotPasswordTextBlock.Text = $"Please enter both username and password. {count}";
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
                int count = 0; 
            }
            else
            {
                count--;
                ForgotPasswordTextBlock.Text = $"Invalid username or password. Attempts: {count}";
                ForgotPasswordTextBlock.Foreground = Brushes.Red;
                ForgotPasswordTextBlock.Visibility = Visibility.Visible;

                PasswordBox.Clear();

            }
        }

        

      

        private void naam(object sender, MouseEventArgs e)
        {
            
            ForgotPasswordTextBlock.Text = "Enter Username.";
            ForgotPasswordTextBlock.Foreground = Brushes.Gray;
            ForgotPasswordTextBlock.Visibility = Visibility.Visible;
        }

        private void wachtwoord(object sender, MouseEventArgs e)
        {

            ForgotPasswordTextBlock.Text = "Enter Password.";
            ForgotPasswordTextBlock.Foreground = Brushes.Gray;
            ForgotPasswordTextBlock.Visibility = Visibility.Visible;
        }

        private void register(object sender, RoutedEventArgs e)
        {
            //gebruik klasse Registration om nieuwe gebruiker te registreren
            UserManager userManager = new UserManager();
        }
    }
}