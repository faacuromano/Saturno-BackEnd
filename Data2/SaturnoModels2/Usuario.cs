using System;
using System.Collections.Generic;

namespace SATURNO_V2.Data2.SaturnoModels2;

public partial class Usuario
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Mail { get; set; } = null!;

    public string? Ubicacion { get; set; }

    public string NumTelefono { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string? FotoPerfil { get; set; }

    public string Pass { get; set; } = null!;

    public DateTime CreacionCuenta { get; set; }

    public bool Verificado { get; set; }

    public string TipoCuenta { get; set; } = null!;

    public virtual Cliente? Cliente { get; set; }

    public virtual Profesionale? Profesionale { get; set; }
}
