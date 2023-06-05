using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SATURNO_V2.Data.DTOs.TurnoDTO;

public class TurnoDtoIn
{
    public DateTime FechaTurno { get; set; }
    public TimeSpan? HoraTurno { get; set; }
    public string Observaciones { get; set; }
    public string UsernameProfesional { get; set; }
    public string UsernameCliente { get; set; }
    public int IdServicios { get; set; }
}
