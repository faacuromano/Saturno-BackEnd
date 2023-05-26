using System.Security.Cryptography;
using System.Text;

namespace SATURNO_V2.Functions
{
    public class PH
    {
        public static string hashPassword(string password)
        {
            var sha = SHA256.Create();

            var asByteArray = Encoding.Default.GetBytes(password);
            var hashedPassword = sha.ComputeHash(asByteArray);

            return Convert.ToBase64String(hashedPassword);
        }
    }

}