using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;

namespace Transcription.Models
{
    public class Userdata
    {
        public string PassWord { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
    }
    public class AuthenticationManager
    {
        private List<Userdata> users;

        public AuthenticationManager()
        {
            // Chemin relatif depuis le répertoire de travail de l'application
            string relativePath = @"..\..\Ressources\Userdata.json";
            string fullPath = Path.GetFullPath(relativePath);

            users = JsonSerializer.Deserialize<List<Userdata>>(File.ReadAllText(fullPath));
        }

        public bool AuthentificateUser(string username, string password)
        {
            return users.Any(user => user.UserName == username && user.PassWord == password);
        }
    }
}
