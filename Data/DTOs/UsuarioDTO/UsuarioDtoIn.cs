namespace SATURNO_V2.Data.DTOs;

public class UsuarioDtoIn
{
    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? Ubicacion { get; set; }

    public string NumTelefono { get; set; } = null!;

    public string FechaNacimiento { get; set; }

    public string? FotoPerfil { get; set; }

}
