﻿using System;
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

        // Initialisation des composants pour l'enregistrement audio
        private WaveInEvent waveIn;
        private WaveFileWriter writer;
        private string outputDirectory;
        private MP3ToText mp3ToTextConverter;

        public SpeechToMP3()
        {
            // Définition du répertoire de sortie et initialisation du convertisseur MP3 en texte
            outputDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "enregistrements");
            mp3ToTextConverter = new MP3ToText();
        }

        public string LastRecordedFilePath { get; private set; }
        public Action<string> UpdateTextBoxAction { get; set; }


        // Récupération des périphériques audio disponibles
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

        // Démarrage de l'enregistrement audio
        public void StartRecording(int deviceNumber)
        {
            try
            {
                Directory.CreateDirectory(outputDirectory);
                string outputFilePath = GenerateUniqueFilePath();
                LastRecordedFilePath = outputFilePath;

                waveIn = new WaveInEvent
                {
                    DeviceNumber = deviceNumber,
                    WaveFormat = new WaveFormat(44100, 1)
                };
                waveIn.DataAvailable += OnDataAvailable;
                waveIn.RecordingStopped += OnRecordingStopped;

                writer = new WaveFileWriter(outputFilePath, waveIn.WaveFormat);
                waveIn.StartRecording();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement : {ex.Message}");
            }
        }

        public void StopRecording()
        {
            waveIn.StopRecording();
            DisposeRecordingResources();
        }

        // Génération d'un chemin de fichier unique pour chaque enregistrement
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
            // Délai avant la conversion audio en texte
            Task.Delay(500).ContinueWith(_ =>
            {
                DisposeRecordingResources();
                ConvertRecordedAudioToText();
            });
        }

        // Conversion de l'audio enregistré en texte
        private async void ConvertRecordedAudioToText()
        {
            try
            {
                if (!string.IsNullOrEmpty(LastRecordedFilePath))
                {
                    string textResult = await mp3ToTextConverter.ConvertAudioToText(LastRecordedFilePath);
                    UpdateTextBoxAction?.Invoke(textResult);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la conversion audio en texte : {ex.Message}");
                UpdateTextBoxAction?.Invoke($"Erreur lors de la conversion audio en texte : {ex.Message}");
            }
        }

        private void DisposeRecordingResources()
        {
            writer?.Close();
            writer?.Dispose();
            writer = null;
            waveIn?.Dispose();
            waveIn = null;
        }
    }
}
