using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SATURNO_V2.Data.SaturnoModels;

public partial class Profesionale
{
    [JsonIgnore]
    public int IdUsuarios { get; set; }

    public string? Descripcion { get; set; }

    public TimeSpan HorarioInicio { get; set; }

    public TimeSpan HorarioFinal { get; set; }

    public string? FotoBanner { get; set; }

    public string Direccion { get; set; } = null!;

    public string? EstadoSubscripcion { get; set; }

    public string? Profesion { get; set; }

    [JsonIgnore]
    public virtual Usuario IdUsuariosNavigation { get; set; } = null!;

    public virtual ICollection<Servicio> Servicios { get; set; } = new List<Servicio>();

    [JsonIgnore]
    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
