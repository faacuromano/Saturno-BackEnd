using System.Text.Json.Serialization;

namespace SATURNO_V2.Data.DTOs;

public class ProfesionalDto
{

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string? Ubicacion { get; set; }

    public string Pass { get; set; } = null!;

    public string NumTelefono { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string? FotoPerfil { get; set; }

    public bool? Verificado { get; set; }

    public DateTime CreacionCuenta { get; set; }

    public string? TipoCuenta { get; set; }

    public string? Descripcion { get; set; }

    public TimeSpan? HorarioInicio { get; set; }

    public TimeSpan? HorarioFinal { get; set; }

    public string? FotoBanner { get; set; }

    public string? Direccion { get; set; }

    [JsonIgnore]
    public virtual UsuarioDtoIn IdUsuariosNavigation { get; set; } = null!;
}
