using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SATURNO_V2.Data.SaturnoModels;

public partial class Usuario
{
    [JsonIgnore]
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string Ubicacion { get; set; } = null!;

    public string NumTelefono { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string? FotoPerfil { get; set; }

    public string Pass { get; set; } = null!;

    [JsonIgnore]
    public DateTime CreacionCuenta { get; set; }

    [JsonIgnore]
    public bool Verificado { get; set; }

    [JsonIgnore]
    public string? TipoCuenta { get; set; }

    [JsonIgnore]
    public virtual Cliente? Cliente { get; set; }

    [JsonIgnore]
    public virtual Profesionale? Profesionale { get; set; }
}
