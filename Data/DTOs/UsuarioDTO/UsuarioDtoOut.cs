using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SATURNO_V2.Data.DTOs;

public class UsuarioDtoOut
{
    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string? Ubicacion { get; set; }

    public string NumTelefono { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string? FotoPerfil { get; set; }

}

