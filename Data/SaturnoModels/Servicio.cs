﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SATURNO_V2.Data.SaturnoModels;

public partial class Servicio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public TimeSpan Duracion { get; set; }

    public int? IdProfesional { get; set; }

    [JsonIgnore]
    public virtual Profesionale? IdProfesionalNavigation { get; set; }

    [JsonIgnore]
    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
