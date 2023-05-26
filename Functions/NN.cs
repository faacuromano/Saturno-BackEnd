using System.Security.Cryptography;
using System.Text;

namespace SATURNO_V2.Functions
{
    public class NN
    {
        public static string ConvertirNombre(string nombre)
        {
            string[] palabras = nombre.Split(' ');
            for (int i = 0; i < palabras.Length; i++)
            {
                if (!string.IsNullOrEmpty(palabras[i]))
                {
                    char[] letras = palabras[i].ToLower().ToCharArray();
                    letras[0] = char.ToUpper(letras[0]);
                    palabras[i] = new string(letras);
                }
            }
            return string.Join(" ", palabras);
        }
    }
}