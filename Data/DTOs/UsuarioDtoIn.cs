using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SATURNO_V2.Data.DTOs;

public class UsuarioDtoIn
{
    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Username { get; set; } = null!;
    
    public string Mail { get; set; } = null!;

    public string Passw { get; set; } = null!;

    public string NumTelefono { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; } 

    public DateTime CreacionCuenta { get; set; } 

    public string? FotoPerfil { get; set; }

    public string TipoCuenta { get; set; } = null!;

}
