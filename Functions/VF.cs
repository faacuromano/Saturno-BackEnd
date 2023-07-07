using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Services;

namespace SATURNO_V2.Functions
{
    public class VF
    {
        public static bool validateProfessional(Profesionale profesionalToValidate)
        {
            bool mail = VerificarCorreo(profesionalToValidate.IdUsuariosNavigation.Mail);
            bool ubicacion = VerificarUbicacion(profesionalToValidate.IdUsuariosNavigation.Ubicacion);
            bool profesion = VerificarProfesion(profesionalToValidate.Profesion);

            if (mail is true && ubicacion is true && profesion is true)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public static bool validateCliente(Cliente clienteToValidate)
        {
            bool mail = VerificarCorreo(clienteToValidate.IdUsuariosNavigation.Mail);
            bool ubicacion = VerificarUbicacion(clienteToValidate.IdUsuariosNavigation.Ubicacion);

            if (mail is true && ubicacion is true)
            {
                return true;
            }
            else
            {
                return false;
            }

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

            if (listaServicios.Contains(profesion))
            {
                return true;
            }
            else
            {
                return false;

            }
        }

        private static bool VerificarUbicacion(string ubicacion)
        {
            List<string> listaUbicacion = new List<string>()
            {
                "Rosario",
                "Arroyo Seco",
                "Funes",
                "Villa Gobernador Galvez",
                "Roldan"
            };

            if (listaUbicacion.Contains(ubicacion))
                return true;
            return false;
        }

        static bool VerificarCorreo(string correo)
        {
            // Expresión regular para verificar el correo electrónico
            string patron = @"^[a-zA-Z0-9._%+-]+@(gmail|hotmail|outlook|yahoo|aol)\.com$";

            // Verificar si el correo cumple5rt con el patrón
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

    }

}