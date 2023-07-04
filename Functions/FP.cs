using System.Globalization;

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


        public static DateTime ConvertirFecha(string fecha)
        {
            DateTime fechaConvertida;
            if (DateTime.TryParseExact(fecha, "yyyy-MM-ddy", CultureInfo.InvariantCulture, DateTimeStyles.None, out fechaConvertida))
            {
                return fechaConvertida;
            }
            else
            {
                // La cadena de entrada no tiene el formato esperado
                // Puedes manejar este caso según tus necesidades, por ejemplo, lanzando una excepción o devolviendo un valor predeterminado.
                throw new ArgumentException("El formato de fecha no es válido.");
            }
        }
    }
}