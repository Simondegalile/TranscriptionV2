TranscriptionV2
Description
TranscriptionV2 est une application Windows avancée développée en C# pour la transcription audio, la traduction et l'analyse de texte. Elle intègre plusieurs fonctionnalités puissantes, notamment :

Transcription Audio-Texte : Utilisation de l'API Google Speech-to-Text pour convertir l'audio enregistré en texte.

Traduction Multilingue : Traduction du texte dans différentes langues en utilisant AWS Translate.

Analyse de Texte : Utilisation d'AWS Comprehend pour l'analyse approfondie du texte traduit.
Synthèse Vocale : Capacité à vocaliser du texte avec des voix naturelles via Amazon Polly.
Base de Données Locale en JSON : Gestion des utilisateurs avec login et mot de passe stockés dans une base de données JSON.
Interface Utilisateur Intuitive : Facilite la gestion des enregistrements, des traductions, et des analyses.


Prérequis
Windows 7/8/10.
.NET Framework 4.7.2 ou supérieur.
Clés d'accès AWS pour AWS Translate, AWS Comprehend et Amazon Polly.
Clé API Google Cloud pour Google Speech-to-Text.


Installation
Clonez le dépôt GitHub :
bash


Copy code
git clone https://github.com/Simondegalile/TranscriptionV2.git
Ouvrez la solution dans Visual Studio.
Restaurez les packages NuGet.
Configurez vos clés d'accès AWS et votre clé API Google Cloud dans les fichiers appropriés.
Compilez et exécutez l'application.


Utilisation
Lancez l'application et connectez-vous avec vos identifiants.
Sélectionnez un périphérique d'enregistrement pour capturer l'audio.
Utilisez l'API Google pour transcrire l'audio en texte.
Traduisez le texte dans la langue de votre choix à l'aide d'AWS Translate.
Analysez le texte traduit pour en extraire des informations utiles avec AWS Comprehend.
Utilisez Amazon Polly pour convertir du texte en parole naturelle.
Gérez facilement tous les processus via une interface utilisateur conviviale.
Contribution
Les contributions à ce projet sont les bienvenues. Pour contribuer, veuillez forker le dépôt, créer une branche pour vos modifications et soumettre une pull request.
