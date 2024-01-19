using Amazon.Translate;
using Amazon.Translate.Model;
using System;
using System.Threading.Tasks;
using Amazon.Runtime; 
using Amazon;

namespace Transcription.Services
{
    internal class AWSTranslation
    {
        private readonly AmazonTranslateClient _translateClient;

        public AWSTranslation()
        {
            // Vérification et retour du premier transcript trouvé
            string awsAccessKeyId = "AKIA4MTWIJO2ZJJWWU7Q";
            string awsSecretAccessKey = "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";

            var awsCredentials = new BasicAWSCredentials(awsAccessKeyId, awsSecretAccessKey);
            var awsRegionEndpoint = RegionEndpoint.GetBySystemName("eu-west-2");


            _translateClient = new AmazonTranslateClient(awsCredentials, awsRegionEndpoint);
            
        }

        public async Task<string> TranslateTextAsync(string text)
        {
            // Création de la requête de traduction
            var request = new TranslateTextRequest
            {
                Text = text,
                SourceLanguageCode = "fr",
                TargetLanguageCode = "en"
            };

            try
            {
                // Envoi de la requête de traduction et récupération de la réponse
                var response = await _translateClient.TranslateTextAsync(request);
                return response.TranslatedText;
            }
            catch (Exception ex)
            {
                return $"Erreur lors de la traduction : {ex.Message}";
            }
        }
    }
}