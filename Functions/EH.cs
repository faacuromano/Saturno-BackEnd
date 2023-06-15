using System.Security.Cryptography;
using System.Text;

namespace SATURNO_V2.Functions
{
    public class EH
    {
        public static string EncryptToken(string token)
        {
            // Invertir el string
            char[] charArray = token.ToCharArray();
            Array.Reverse(charArray);
            string invertedToken = new string(charArray);

            // Agregar 10 caracteres extra en puntos específicos
            string encryptedToken = "";
            for (int i = 0; i < invertedToken.Length; i++)
            {
                encryptedToken += invertedToken[i];
                if (i % 5 == 0) // Agregar un punto cada 3 caracteres
                {
                    encryptedToken += "º";
                }
            }

            return encryptedToken;
        }
    }

}