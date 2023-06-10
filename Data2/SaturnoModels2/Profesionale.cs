using System;
using System.Collections.Generic;

namespace SATURNO_V2.Data2.SaturnoModels2;

public partial class Profesionale
{
    public int IdUsuarios { get; set; }

    public string? Descripcion { get; set; }

    public TimeSpan HorarioInicio { get; set; }

    public TimeSpan HorarioFinal { get; set; }

    public string? FotoBanner { get; set; }

    public string Direccion { get; set; } = null!;

    public string? Profesion { get; set; }

    public bool? EstadoSub { get; set; }

    public virtual Usuario IdUsuariosNavigation { get; set; } = null!;

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
