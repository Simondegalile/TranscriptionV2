using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using Transcription.Models;
using System.IO;
using System.Text.Json;
using Transcription.View;
using System.Collections.Generic;

namespace Transcription
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Userdata user;
        private AuthenticationManager authManager;

        public MainWindow()
        {
            InitializeComponent();
            AnimerFond();
            authManager = new AuthenticationManager();
            string relativePath = @"..\..\Ressources\Userdata.json";
            string fullPath = Path.GetFullPath(relativePath);

            try
            {
                List<Userdata> users = JsonSerializer.Deserialize<List<Userdata>>(File.ReadAllText(fullPath));
                foreach (var u in users)
                {
                    Console.WriteLine($"UserName: {u.UserName}, Email: {u.Email}, Password: {u.PassWord}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AnimerFond()
        {
            ColorAnimation animation = new ColorAnimation();
            animation.From = Colors.Blue;
            animation.To = Colors.Red;
            animation.Duration = TimeSpan.FromSeconds(3);
            animation.AutoReverse = true;
            animation.RepeatBehavior = RepeatBehavior.Forever;

        }

        private void BP_Login_Click(object sender, RoutedEventArgs e)
        {
            string username = TB_Login.Text;
            string password = Pwd_Password.Password;

            if (authManager.AuthentificateUser(username, password))
            {
                Window_container.RowDefinitions.Clear();
                Window_container.Children.Clear();
                View.Page1 page1 = new Page1();
                Window_container.Children.Add(page1);
            }
            else
            {
                MessageBox.Show("Invalid username or password", "Login Failed", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void TB_Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Assurez-vous que user est initialisé avant de l'utiliser
            if (user != null && TB_Login.Text == user.UserName && Pwd_Password.Password.ToString() == user.PassWord)
            {
                BP_Login.IsEnabled = true;
            }
        }
    }
}
