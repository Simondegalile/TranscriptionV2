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
using Transcription.Models;
using System.IO;
using System.Text.Json;

namespace Transcription.View
{
    /// <summary>
    /// Logique d'interaction pour Login.xaml
    /// </summary>
    public partial class Login : UserControl
    {
        private List<Userdata> users;
        private string filePath = @"..\..\Ressources\Userdata.json";


        public Login()
        {
            InitializeComponent();
            LoadUsersFromDatabase();
        }

        private void LoadUsersFromDatabase()
        {
            string fullPath = Path.GetFullPath(filePath);
            users = JsonSerializer.Deserialize<List<Userdata>>(File.ReadAllText(fullPath));
        }

        private void SaveUsersToDatabase()
        {
            string fullPath = Path.GetFullPath(filePath);
            File.WriteAllText(fullPath, JsonSerializer.Serialize(users));
        }

        private void TB_Login_TextChanged(object sender, TextChangedEventArgs e)
        {
            BP_Ajouter.IsEnabled = !string.IsNullOrEmpty(TB_Login.Text);
        }


        private void TB_Mail(object sender, TextChangedEventArgs e)
        {
            BP_Ajouter.IsEnabled = !string.IsNullOrEmpty(TB_Email.Text);
        }

        private void BP_Ajouter_Click(object sender, RoutedEventArgs e)
        {
            // Vérifier si les champs d'utilisateur et de mot de passe ne sont pas vides
            if (string.IsNullOrWhiteSpace(TB_Login.Text) || string.IsNullOrWhiteSpace(Pwd_Password.Password))
            {
                MessageBox.Show("Veuillez spécifier un nom d'utilisateur et un mot de passe.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Arrêter le traitement car les champs ne sont pas valides
            }

            // Ajouter un nouvel utilisateur à la liste
            Userdata newUser = new Userdata
            {
                UserName = TB_Login.Text,
                PassWord = Pwd_Password.Password,
                Email = TB_Email.Text
            };

            users.Add(newUser);

            // Sauvegarder les modifications dans la base de données
            SaveUsersToDatabase();

            // Afficher un message ou effectuer d'autres actions si nécessaire
            MessageBox.Show("Utilisateur ajouté avec succès!", "Ajout d'utilisateur", MessageBoxButton.OK, MessageBoxImage.Information);
        }



        private void BP_Supprimer_Click(object sender, RoutedEventArgs e)
        {
            // Vérifier si les champs d'utilisateur et de mot de passe ne sont pas vides
            if (string.IsNullOrWhiteSpace(TB_Login.Text) || string.IsNullOrWhiteSpace(Pwd_Password.Password))
            {
                MessageBox.Show("Veuillez spécifier un nom d'utilisateur et un mot de passe.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return; // Arrêter le traitement car les champs ne sont pas valides
            }

            // Vérifier si l'utilisateur à supprimer existe dans la liste
            Userdata userToRemove = users.FirstOrDefault(user => user.UserName == TB_Login.Text);

            if (userToRemove != null)
            {
                // Supprimer l'utilisateur de la liste
                users.Remove(userToRemove);

                // Sauvegarder les modifications dans la base de données
                SaveUsersToDatabase();

                // Afficher un message ou effectuer d'autres actions si nécessaire
                MessageBox.Show("Utilisateur supprimé avec succès!", "Suppression d'utilisateur", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                // L'utilisateur n'a pas été trouvé dans la liste
                MessageBox.Show("Utilisateur non trouvé!", "Suppression d'utilisateur", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void BP_Retour_Click(object sender, RoutedEventArgs e)
        {
            Window_container.RowDefinitions.Clear();
            Window_container.Children.Clear();
            View.Page1 page1 = new Page1();
            Window_container.Children.Add(page1);
        }
    }
}
