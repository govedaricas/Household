using System.Security.Cryptography;
using System.Text;
using Application.Interfaces;
using Infrastructure.Entities;

namespace Application.Services
{
    public class PasswordService : IPasswordService
    {
        const int SALT_SIZE = 16;
        private const int HASH_SIZE = 32;
        private const int ITERATIONS = 100000;
        private readonly HashAlgorithmName ALGORITHM = HashAlgorithmName.SHA512;

        public byte[] HashPassword(string password, out byte[] salt)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be null or empty.", nameof(password));

            salt = RandomNumberGenerator.GetBytes(SALT_SIZE);

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                ITERATIONS,
                ALGORITHM,
                HASH_SIZE);

            return hash;
        }

        public bool VerifyPassword(User user, string password)
        {
            if (user == null || string.IsNullOrWhiteSpace(password))
                return false;

            if (user.PasswordHash == null || user.PasswordSalt == null)
                return false;

            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                user.PasswordSalt,
                ITERATIONS,
                ALGORITHM,
                HASH_SIZE);

            return CryptographicOperations.FixedTimeEquals(user.PasswordHash, hash);
        }
    }
}
