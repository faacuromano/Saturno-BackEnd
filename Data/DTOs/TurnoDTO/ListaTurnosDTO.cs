using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SATURNO_V2.Data.DTOs.TurnoDTO;

public class ListaTurnosDTO
{
    public DateTime FechaTurno { get; set; }
    public TimeSpan? HoraTurno { get; set; }

}