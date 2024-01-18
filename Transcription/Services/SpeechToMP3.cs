using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NAudio.Wave;


namespace Transcription.Services
{
    internal class SpeechToMP3
    {
        private WaveInEvent waveIn;
        private WaveFileWriter writer;

        private string outputDirectory;

        public SpeechToMP3()
        {
            outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "enregistrements");
        }

        public string LastRecordedFilePath { get; private set; } //pour stocker le chemin d'enregistrement

        public List<string> GetAudioDevices()
        {
            List<string> deviceList = new List<string>();
            for (int n = 0; n < WaveIn.DeviceCount; n++)
            {
                var deviceInfo = WaveIn.GetCapabilities(n);
                deviceList.Add(deviceInfo.ProductName);
            }
            return deviceList;
        }


        public void StartRecording(int deviceNumber)
        {
            try
            {
                // Créez le dossier d'enregistrement s'il n'existe pas
                Directory.CreateDirectory(outputDirectory);

                // Générez un chemin de fichier unique
                string outputFilePath = GenerateUniqueFilePath();
                LastRecordedFilePath = outputFilePath;

                // Initialisez waveIn avant de créer le writer
                waveIn = new WaveInEvent
                {
                    DeviceNumber = deviceNumber,
                    WaveFormat = new WaveFormat(44100, 1)
                };
                waveIn.DataAvailable += OnDataAvailable;
                waveIn.RecordingStopped += OnRecordingStopped;

                // Maintenant, créez le WaveFileWriter
                writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);

                // Commencez l'enregistrement
                waveIn.StartRecording();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement : {ex.Message}");
                // Gérez l'exception ici (par exemple, nettoyez les ressources si nécessaire)
            }
        }

        public void StopRecording()
        {
            waveIn.StopRecording();
            DisposeRecordingResources();
        }

        private string GenerateUniqueFilePath()
        {
            int fileNumber = 1;
            string filePath;
            do
            {
                filePath = Path.Combine(outputDirectory, $"enregistrement{fileNumber}.wav");
                fileNumber++;
            } while (File.Exists(filePath));

            return filePath;
        }

        private void OnDataAvailable(object sender, WaveInEventArgs args)
        {
            if (writer == null)
            {
                Console.WriteLine("Erreur : 'writer' est null dans OnDataAvailable.");
                return;
            }

            writer.Write(args.Buffer, 0, args.BytesRecorded);
        }

        private void OnRecordingStopped(object sender, StoppedEventArgs args)
        {
            DisposeRecordingResources();
        }

        private void DisposeRecordingResources()
        {
            writer?.Dispose();
            writer = null;
            waveIn?.Dispose();
            waveIn = null;
        }
    }
}
