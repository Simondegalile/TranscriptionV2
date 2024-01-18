using NAudio.Wave;
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
    /// Logique d'interaction pour MP3.xaml
    /// </summary>
    public partial class MP3 : UserControl
    {
        private SpeechToMP3 speechToMp3;
        public MP3()
        {
            InitializeComponent();
            speechToMp3 = new SpeechToMP3();
            LoadAudioDevices();
        }

        private void BTN_Lancer_Click(object sender, RoutedEventArgs e)
        {
            speechToMp3.StartRecording(0);

            int selectedDevice = comboBoxDevices.SelectedIndex;
            if (selectedDevice >= 0)
            {
                speechToMp3.StartRecording(selectedDevice);
            }
            else
            {
                MessageBox.Show("Aucun Péripherique n'est séléctionné");
            }
        }

        private void LoadAudioDevices()
        {
            var devices = speechToMp3.GetAudioDevices();
            foreach (var device in devices)
            {
                comboBoxDevices.Items.Add(device);
            }

            if (comboBoxDevices.Items.Count > 0)
            {
                comboBoxDevices.SelectedIndex = 0;
            }
        }

        private void BTN_Retour_Click(object sender, RoutedEventArgs e)
        {
            Window_container.RowDefinitions.Clear();
            Window_container.Children.Clear();
            View.Page1 page1 = new Page1();
            Window_container.Children.Add(page1);
        }

        private void BTN_Arreter_Click(object sender, RoutedEventArgs e)
        {
            speechToMp3.StopRecording();

            MessageBox.Show($"Enregistrement terminé.\nFichier enregistré à : {speechToMp3.LastRecordedFilePath}", "Enregistrement MP3");
        }
    }
}
