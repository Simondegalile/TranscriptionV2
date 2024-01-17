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
using Transcription.Services;

namespace Transcription.View
{
    /// <summary>
    /// Logique d'interaction pour Page1.xaml
    /// </summary>
    public partial class Page1 : UserControl
    {
        public Page1()
        {
            InitializeComponent();
            BTN_Texte.Click += BTN_Texte_Click;
            BTN_MP3.Click += BTN_MP3_Click;
        }

        private void BTN_Texte_Click(object sender, RoutedEventArgs e)
        {
            // Clear the Grid
            Window_container.Children.Clear();

            // Create a new instance of Text.xaml
            View.Text textPage = new View.Text();

            // Add Text.xaml to the center of the Grid
            Grid.SetRow(textPage, 1);
            Window_container.Children.Add(textPage);
        }
        private void BTN_MP3_Click(object sender, RoutedEventArgs e)
        {
            // Clear the Grid
            Window_container.Children.Clear();

            // Create a new instance of MP3.xaml
            MP3 mp3Page = new MP3();

            // Add MP3.xaml to the center of the Grid
            Grid.SetRow(mp3Page, 1);
            Window_container.Children.Add(mp3Page);
        }

    }
}
