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

namespace Transcription.View
{
    /// <summary>
    /// Logique d'interaction pour MP3.xaml
    /// </summary>
    public partial class MP3 : UserControl
    {
        public MP3()
        {
            InitializeComponent();
            BTN_Retour.Click += BTN_Retour_Click; 
        }

        private void BTN_Lancer_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BTN_Retour_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var contentGrid = mainWindow.FindName("Window_container") as Grid;

            if (contentGrid != null)
            {
                // Supprimez tout le contenu actuel de 'contentGrid'
                contentGrid.Children.Clear();

                // Créez une nouvelle instance de Page1 et ajoutez-la à 'contentGrid'
                var page1 = new Page1();
                Grid.SetRow(page1, 1); // Assurez-vous de définir la bonne ligne si nécessaire
                contentGrid.Children.Add(page1);
            }
        }

        private void BTN_Arreter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
