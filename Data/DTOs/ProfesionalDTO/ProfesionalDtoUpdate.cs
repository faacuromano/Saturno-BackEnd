using System.Text.Json.Serialization;

namespace SATURNO_V2.Data.DTOs.ProfesionalDTO;

public class ProfesionalDtoUpdate
{
    public int IdUsuarios { get; set; }
    public string? Descripcion { get; set; }

    public TimeSpan HorarioInicio { get; set; }

    public TimeSpan HorarioFinal { get; set; }

    public string? FotoBanner { get; set; }

    public string Direccion { get; set; } = null!;

    public string? Profesion { get; set; }

}