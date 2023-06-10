using System;
using System.Collections.Generic;

namespace SATURNO_V2.Data2.SaturnoModels2;

public partial class Cliente
{
    public int IdUsuarios { get; set; }

    public virtual Usuario IdUsuariosNavigation { get; set; } = null!;

    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
