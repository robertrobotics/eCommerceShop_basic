using System;
using System.Security.Cryptography;
using Identity.API.Interfaces;
using Identity.API.Models;

namespace Identity.API.Helpers
{
    public class PasswordHasher
    {
        private const int HashingIterationsNumber = 10000;
        private byte[] _salt;

        public PasswordHasher()
        {
        }   

        public IEncryptedUser GetEncryptedUserInfo(string password)
        {
            if (string.IsNullOrEmpty(password))
                throw new ArgumentNullException(nameof(password), "Provided password cannot be equal to null!");
            
            // Generate 16-byte salt
            new RNGCryptoServiceProvider().GetBytes(_salt = new byte[16]);

            // Hash salted password using PBKDF2
            var key = new Rfc2898DeriveBytes(password, _salt, HashingIterationsNumber);

            // Get first 20 bits of key to be a hash 
            byte[] hash = key.GetBytes(20);

            // Reserve 36-bytes for hashed password (salt (16B) + hashedPassword (20B))
            byte[] hashedPasswordBytes = new byte[36];

            // Add salt at the beginning
            Array.Copy(_salt, 0, hashedPasswordBytes, 0, 16);
            Array.Copy(hash, 0, hashedPasswordBytes, 16, 20);

            return new EncryptedUser() { Password = password, HashedPassword = Convert.ToBase64String(hashedPasswordBytes) };
        }
    }
}