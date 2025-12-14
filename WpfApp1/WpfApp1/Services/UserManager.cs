using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WpfApp1.Models;

namespace WpfApp1.Services
{
    internal class UserManager
    {
        // Statische lijst om geregistreerde gebruikers bij te houden (blijft bestaan tussen method calls)
        private static List<Registration> _users = new List<Registration>();

        public bool Register(string username, string password)
        {
            // Check of username al bestaat
            if (_users.Any(u => u.Username == username))
            {
                return false; // Gebruiker bestaat al
            }

            Registration newUser = new Registration
            {
                Username = username,
                Password = HashPassword(password)
            };

            _users.Add(newUser);  // Sla de nieuwe gebruiker op
            return true;
        }

        public bool Trylogin(Registration credentials)
        {
            string hashedPassword = HashPassword(credentials.Password);

            // Zoek in de lijst van geregistreerde gebruikers
            var user = _users.FirstOrDefault(u =>
                u.Username == credentials.Username &&
                u.Password == hashedPassword);

            // Als geen user gevonden, check ook hardcoded admin account
            if (user == null)
            {
                if (credentials.Username == "admin" && hashedPassword == HashPassword("password"))
                {
                    return true;
                }
                return false;
            }

            return true;
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));
                byte[] hash = sha256Hash.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }
    }
}