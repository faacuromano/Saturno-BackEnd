using SATURNO_V2.Data;

namespace SATURNO_V2.Services;

public class ListaServices
{

    private readonly SaturnoV2Context _context;

    public ListaServices(SaturnoV2Context context)
    {
        _context = context;
    }

    public List<string> GetRubro()
    {

        List<string> servicios = new List<string>()
        {
            "Psicologo",
            "Docente",
            "Canchas de futbol",
            "Peluquero",
            "Medico",
            "Estilista",
            "Kinesiologo"
        };
        return servicios;
    }
    public List<string> GetUbicaciones()
    {

        List<string> ubicaciones = new List<string>()
        {
            "Rosario",
            "Arroyo seco",
            "Funes",
            "Villa gobernador Galvez",
            "Roldan"
        };
        return ubicaciones;
    }

}