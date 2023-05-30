namespace SATURNO_V2.Data.DTOs.ProfesionalDTO;

public class ProfesionalCreateDtoIn
{
    public int IdUsuarios { get; set; }

    public string? Descripcion { get; set; }

    public TimeSpan? HorarioInicio { get; set; }

    public TimeSpan? HorarioFinal { get; set; }

    public string? FotoBanner { get; set; }

    public string? Direccion { get; set; }

    public string Ubicacion { get; set; } = null!;

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string NumTelefono { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string? FotoPerfil { get; set; }

    public string TipoCuenta { get; set; } = null!;

}
