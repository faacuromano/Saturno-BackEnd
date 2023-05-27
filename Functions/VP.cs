using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Services;

namespace SATURNO_V2.Functions
{
    public class VP
    {
        public static bool validateProfessional(Profesionale profesioalToValidate)
        {
            bool mail = VerificarCorreo(profesioalToValidate.IdUsuariosNavigation.Mail);
            bool telefono = VerificarTelefono(profesioalToValidate.IdUsuariosNavigation.NumTelefono);
            bool ubicacion = VerificarProfesion(profesioalToValidate.IdUsuariosNavigation.Ubicacion);
            bool profesion = VerificarProfesion(profesioalToValidate.Profesion);

            return true;
        }

        private static bool VerificarProfesion(string profesion)
        {
            List<string> listaServicios = new List<string>()
            {
                "Psicologo",
                "Docente",
                "Canchas de futbol",
                "Peluquero",
                "Medico",
                "Estilista",
                "Kinesiologo"
            };

            return listaServicios.Contains(profesion);
        }

        private static bool VerificarUbicacion(string ubicacion)
        {
            List<string> listaUbicacion = new List<string>()
            {
                "Rosario",
                "Arroyo seco",
                "Funes",
                "Villa gobernador Galvez",
                "Roldan"
            };

            return listaUbicacion.Contains(ubicacion);
        }

        static bool VerificarCorreo(string correo)
        {
            // Expresión regular para verificar el correo electrónico
            string patron = @"^[a-zA-Z0-9._%+-]+@(gmail|hotmail|outlook|yahoo|aol)\.com$";

            // Verificar si el correo cumple con el patrón
            bool esValido = Regex.IsMatch(correo, patron, RegexOptions.IgnoreCase);

            if (esValido is true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static bool VerificarTelefono(string telefono)
        {
            // Expresión regular para verificar el formato del número de teléfono argentino
            string patron = @"^\+?54(?:11|[2368]\d)(?:(?=\d{0,2}15)\d{2})??\d{8}$";

            Regex regex = new Regex(patron);

            return regex.IsMatch(telefono);
        }

    }

}