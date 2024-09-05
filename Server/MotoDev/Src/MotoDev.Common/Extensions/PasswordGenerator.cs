using System.Security.Cryptography;
using System.Text;

namespace MotoDev.Common.Extensions
{
    public static class PasswordGenerator
    {
        public static string GenerateHash(this string inputString)
        {
            var stringBuilder = new StringBuilder();
            foreach (byte b in ComputerHash(inputString))
                stringBuilder.Append(b.ToString("X2"));

            return stringBuilder.ToString().ToLowerInvariant();
        }

        private static byte[] ComputerHash(string inputString)
        {
            using var algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }
    }
}