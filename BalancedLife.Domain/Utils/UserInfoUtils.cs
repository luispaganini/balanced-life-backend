using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BalancedLife.Domain.Utils {
    public static class UserInfoUtils {
        public static bool VerifyPassword(string password, string hashedPassword) {
            return HashPassword(password) == hashedPassword;
        }
        public static bool isValidPassword(string password) {
            string pattern = @"^(?=.*[A-Za-z])(?=.*\d)[A-Za-z\d]{8,}$";

            return Regex.IsMatch(password, pattern);
        }

        public static string HashPassword(string password) {
            using ( var sha256 = SHA256.Create() ) {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }
    }
}
