using System;
using System.Collections.Generic;

namespace SATURNO_V2.Data.SaturnoModels;

public partial class Administrador
{
    public int Id { get; set; }

    public string Semilla { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Passw { get; set; } = null!;

    public DateTime CreacionCuenta { get; set; }

    public string Tipo { get; set; } = null!;
}
