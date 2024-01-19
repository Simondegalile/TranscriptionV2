using System.Windows;
using System.Windows.Controls;
using Transcription.Services;

namespace Transcription.View
{

    public partial class MP3 : UserControl
    {
        // Déclaration des instances de service.
        private SpeechToMP3 speechToMp3;
        private AWSTranslation _awsTranslation;

        public MP3()
        {
            InitializeComponent();

            speechToMp3 = new SpeechToMP3();
            _awsTranslation = new AWSTranslation();

            LoadAudioDevices();


            // Définir une action pour mettre à jour le TextBox avec le texte transcrit.
            speechToMp3.UpdateTextBoxAction = UpdateTranscriptionTextBox;
        }

        private void BTN_Lancer_Click(object sender, RoutedEventArgs e)
        {
            // Lancer l'enregistrement audio
            int selectedDevice = comboBoxDevices.SelectedIndex;
            if (selectedDevice >= 0)
            {
                speechToMp3.StartRecording(selectedDevice);
            }
            else
            {
                MessageBox.Show("No device is selected", "Error");
            }
        }

        private void LoadAudioDevices()
        {
            // Charger et afficher les périphériques audio dans le ComboBox.
            var devices = speechToMp3.GetAudioDevices();
            comboBoxDevices.ItemsSource = devices;
            if (comboBoxDevices.Items.Count > 0)
            {
                comboBoxDevices.SelectedIndex = 0;
            }
        }

        private void BTN_Retour_Click(object sender, RoutedEventArgs e)
        {
            // Retourner à la page précédente.
            Window_container.RowDefinitions.Clear();
            Window_container.Children.Clear();
            View.Page1 page1 = new Page1();
            Window_container.Children.Add(page1);
        }

        private void BTN_Arreter_Click(object sender, RoutedEventArgs e)
        {
            speechToMp3.StopRecording();
            MessageBox.Show($"Recording stopped.\nFile saved at: {speechToMp3.LastRecordedFilePath}", "MP3 Recording");
        }

        private void UpdateTranscriptionTextBox(string text)
        {
            // Mettre à jour le TextBox avec le texte transcrit.
            // Utiliser Dispatcher pour s'assurer que l'opération est effectuée sur le thread UI.
            if (!Dispatcher.CheckAccess())
            {
                Dispatcher.Invoke(() => TB_texte.Text = text);
            }
            else
            {
                TB_texte.Text = text;
            }
        }

        private async void BTN_Translate_Click(object sender, RoutedEventArgs e)
        {
            // Traduire le texte et l'afficher dans le TextBox.
            string textToTranslate = TB_texte.Text;
            if (!string.IsNullOrWhiteSpace(textToTranslate))
            {
                string translatedText = await _awsTranslation.TranslateTextAsync(textToTranslate);
                TB_texte.Text = translatedText;
            }
        }
    }
}
