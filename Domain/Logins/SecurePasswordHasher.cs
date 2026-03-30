using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Logins
{
    public static class SecurePasswordHasher
    {
        public static string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(16); // salt ngẫu nhiên
            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] hash = pbkdf2.GetBytes(32); // lấy 32 byte hash

            return Convert.ToBase64String(salt.Concat(hash).ToArray()); // lưu cả salt + hash
        }

        public static bool Verify(string password, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);
            byte[] salt = hashBytes.Take(16).ToArray();
            byte[] storedPasswordHash = hashBytes.Skip(16).ToArray();

            var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 100_000, HashAlgorithmName.SHA256);
            byte[] enteredPasswordHash = pbkdf2.GetBytes(32);

            return storedPasswordHash.SequenceEqual(enteredPasswordHash);
        }
    }
}
