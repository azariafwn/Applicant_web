using System.Security.Cryptography;
using System.Text;

namespace LearningApplicantWeb
{
    public class Helpers
    {
        public static string GeneratePasswordHash(string username, string password)
        {
            // Gabungkan username dan password (bisa kamu ganti separatornya)
            var combined = $"{username}:{password}";

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(combined);
                var hashBytes = sha256.ComputeHash(bytes);

                // Konversi byte array ke format HEX string
                var sb = new StringBuilder();
                foreach (var b in hashBytes)
                {
                    sb.Append(b.ToString("x2")); // hex lowercase
                }

                return sb.ToString();
            }
        }

        public static string GenerateAlphanumericCode()
        {
            int length = 5;
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Range(0, length)
                .Select(_ => chars[random.Next(chars.Length)])
                .ToArray());
        }

    }
}
