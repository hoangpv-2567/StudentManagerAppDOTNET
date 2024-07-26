using System.Security.Cryptography;
using System.Text;

public class PasswordHelper
{
    public static (string hash, string salt) CreatePasswordHash(string password)
    {
        // Tạo giá trị muối ngẫu nhiên
        byte[] saltBytes = new byte[16];
        RandomNumberGenerator.Fill(saltBytes);
        string salt = Convert.ToBase64String(saltBytes);

        // Kết hợp mật khẩu và muối để tạo băm
        string saltedPassword = password + salt;
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            string hash = Convert.ToBase64String(hashBytes);
            return (hash, salt);
        }
    }

    public static bool VerifyPassword(string enteredPassword, string storedHash, string storedSalt)
    {
        // Kết hợp mật khẩu được nhập và muối đã lưu để tạo băm
        string saltedPassword = enteredPassword + storedSalt;
        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(saltedPassword));
            string hash = Convert.ToBase64String(hashBytes);
            return hash == storedHash;
        }
    }
}
