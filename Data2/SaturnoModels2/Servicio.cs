using System;
using System.Collections.Generic;

namespace SATURNO_V2.Data2.SaturnoModels2;

public partial class Servicio
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public decimal Precio { get; set; }

    public TimeSpan Duracion { get; set; }

    public int? IdProfesional { get; set; }

    public virtual Profesionale? IdProfesionalNavigation { get; set; }

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
