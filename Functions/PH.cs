using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

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
        public static bool verifyPassword(string input)
        {
            // Verificar si contiene al menos una letra mayúscula
            bool hasUppercase = input.Any(char.IsUpper);

            // Verificar si contiene al menos un número utilizando una expresión regular
            bool hasNumber = Regex.IsMatch(input, @"\d");

            // Retornar true si cumple ambas condiciones, de lo contrario, retornar false
            return hasUppercase && hasNumber;
        }
    }

}