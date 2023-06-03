using System.Security.Cryptography;
using System.Text;

namespace SATURNO_V2.Functions
{
    public class FP
    {
        public static string FechaParse(DateTime fecha)
        {
            string toParse = fecha.ToString();
            string fechaParseada = toParse.Substring(0, toParse.Length - 8);

            return fechaParseada;
        }
    }
}