using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SATURNO_V2.Data.DTOs.TurnoDTO;

public class TurnoDtoIn
{
    public DateTime FechaTurno { get; set; }
    public TimeSpan? HoraTurno { get; set; }
    public string Observaciones { get; set; } = null!;
    public string UsernameProfesional { get; set; } = null!;
    public string UsernameCliente { get; set; } = null!;
    public int IdServicios { get; set; }
}

public class GenerarHorariosRequest
{
    public int IdProfesional { get; set; }
    public DateTime Fecha { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFinal { get; set; }
    public TimeSpan Intervalo { get; set; }
}