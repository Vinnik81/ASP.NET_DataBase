using System.Security.Cryptography;
using System.Text;

namespace Authentication.Models
{
    public static class SHA256Encryptor
    {
        public static string Encrypt(string data)
        {
            StringBuilder stringBuilder = new StringBuilder();
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
               

                for (int i = 0; i < bytes.Length; i++)
                {
                    stringBuilder.Append(bytes[i].ToString("x2"));
                }
            }
            return stringBuilder.ToString();
        }
    }
}
