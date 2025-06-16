using System.Security.Cryptography;
using System.Text;

namespace ERPAPI.Services
{
    public class PasswordService
    {
        private const string Salt = "ERPAPI"; // 在实际应用中，这个盐值应该存储在配置文件中

        public string HashPassword(string password)
        {
            using (var md5 = MD5.Create())
            {
                // 将密码和盐值组合
                var saltedPassword = password + Salt;
                // 将字符串转换为字节数组
                var bytes = Encoding.UTF8.GetBytes(saltedPassword);
                // 计算哈希值
                var hash = md5.ComputeHash(bytes);
                // 将字节数组转换为十六进制字符串
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }

        public bool VerifyPassword(string password, string hashedPassword)
        {
            var computedHash = HashPassword(password);
            return computedHash.Equals(hashedPassword, StringComparison.OrdinalIgnoreCase);
        }
    }
} 