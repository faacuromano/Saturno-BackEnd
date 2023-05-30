using System.Text.Json.Serialization;
using SATURNO_V2.Data.SaturnoModels;

namespace SATURNO_V2.Data.DTOs.ProfesionalDTO;

public class Profesional_Servicios_DTO
{
    public int IdUsuarios { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Username { get; set; } = null!;

    public decimal? Precio { get; set; }

    public TimeSpan? Duracion { get; set; }

    public string? NombreServicio { get; set; }

    [JsonIgnore]
    public virtual UsuarioDtoIn IdUsuariosNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Servicio? IdServiciosNavigation { get; set; } = null!;
}
