using System;
using System.Collections.Generic;

namespace SATURNO_V2.Data2.SaturnoModels2;

public partial class Turno
{
    public int Id { get; set; }

    public int IdClientes { get; set; }

    public int IdProfesionales { get; set; }

    public int IdServicios { get; set; }

    public DateTime? FechaTurno { get; set; }

    public TimeSpan? HoraTurno { get; set; }

    public string? Observaciones { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Cliente IdClientesNavigation { get; set; } = null!;

    public virtual Profesionale IdProfesionalesNavigation { get; set; } = null!;

    public virtual Servicio IdServiciosNavigation { get; set; } = null!;
}
