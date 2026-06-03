using System.Security.Cryptography;
using System.Text;

namespace SkyVisionStore.BusinessLogic.Core.Auth
{
    public static class PasswordHasher
    {
        public static string HashPassword(string password)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var hashBytes = SHA256.HashData(passwordBytes);

            return Convert.ToHexString(hashBytes).ToLowerInvariant();
        }

        public static bool VerifyPassword(string password, string storedPassword)
        {
            if (string.IsNullOrWhiteSpace(password) ||
                string.IsNullOrWhiteSpace(storedPassword))
            {
                return false;
            }

            var enteredPasswordHash = HashPassword(password);

            return string.Equals(
                enteredPasswordHash,
                storedPassword,
                StringComparison.OrdinalIgnoreCase
            );
        }

        public static bool IsHashedPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            if (password.Length != 64)
            {
                return false;
            }

            return password.All(IsHexCharacter);
        }

        private static bool IsHexCharacter(char symbol)
        {
            return symbol is >= '0' and <= '9'
                or >= 'a' and <= 'f'
                or >= 'A' and <= 'F';
        }
    }
}