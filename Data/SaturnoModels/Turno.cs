using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SATURNO_V2.Data.SaturnoModels;

public partial class Turno
{
    public int Id { get; set; }

    public int IdClientes { get; set; }

    public int IdProfesionales { get; set; }

    public int IdServicios { get; set; }

    public DateTime? FechaTurno { get; set; }

    public TimeSpan? HoraTurno { get; set; }

    public string? Observaciones { get; set; }

    [JsonIgnore]
    public virtual Cliente IdClientesNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual Profesionale IdProfesionalesNavigation { get; set; } = null!;
    
    [JsonIgnore]
    public virtual Servicio IdServiciosNavigation { get; set; } = null!;
}
