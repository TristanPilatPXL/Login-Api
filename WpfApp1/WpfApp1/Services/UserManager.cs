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
        public bool Register(string username, string password)
        {
            Registration newUser = new Registration
            {
                Username = username,
                Password = HashPassword(password)
            };

            return true;
        }

        public bool Trylogin(Registration credentials)
        {
            // Hash het ingevoerde wachtwoord EERST, dan vergelijken
            string hashedInputPassword = HashPassword(credentials.Password);

            if (credentials.Username == "admin" && hashedInputPassword == HashPassword("password"))
            {
                return true;
            }
            return false;
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
