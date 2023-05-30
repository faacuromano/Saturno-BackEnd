using System.Text.Json.Serialization;

namespace SATURNO_V2.Data.DTOs.ClientDto;

public class ClientePerfilDto
{
    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string? FotoPerfil { get; set; }

    public string? Ubicacion { get; set; }

}
