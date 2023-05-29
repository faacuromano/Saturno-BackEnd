using System.Text.Json.Serialization;

namespace SATURNO_V2.Data.DTOs;

public class ClienteDto
{
    public int IdUsuarios { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string Pass { get; set; } = null!;

    public string NumTelefono { get; set; } = null!;

    public string FechaNacimiento { get; set; }

    public string? FotoPerfil { get; set; }

    public bool? Verificado { get; set; }

    public string CreacionCuenta { get; set; }

    public string? TipoCuenta { get; set; }

    public string? Ubicacion { get; set; }

}
