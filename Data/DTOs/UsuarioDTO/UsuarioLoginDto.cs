using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SATURNO_V2.Data.SaturnoModels;

public partial class UsuarioLoginDto
{
    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string? Ubicacion { get; set; }

    public string NumTelefono { get; set; } = null!;

    public DateTime? FechaNacimiento { get; set; }

    public string? FotoPerfil { get; set; }

    public string TipoCuenta { get; set; } = null!;
}
