using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SATURNO_V2.Data.DTOs.TurnoDTO;

public partial class TurnoDtoIn
{
    public int IdClientes { get; set; }

    public int IdProfesionales { get; set; }

    public int IdServicios { get; set; }

    public DateTime FechaTurno { get; set; }

    public TimeSpan? HoraTurno { get; set; }

    public string? Observaciones { get; set; }
}
