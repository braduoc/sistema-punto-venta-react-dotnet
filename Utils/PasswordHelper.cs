using System.Security.Cryptography;
using System.Text;

namespace ReactVentas.Utils
{
    public static class PasswordHelper
    {
        public static string Hash(string? password)
        {
            if (string.IsNullOrWhiteSpace(password))
            {
                return string.Empty;
            }

            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToHexString(bytes);
        }

        public static bool Verify(string? password, string? storedValue)
        {
            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(storedValue))
            {
                return false;
            }

            return string.Equals(Hash(password), storedValue, StringComparison.OrdinalIgnoreCase)
                || string.Equals(password, storedValue, StringComparison.Ordinal);
        }
    }
}
