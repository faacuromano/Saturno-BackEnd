using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SATURNO_V2.Data.SaturnoModels;

public partial class Cliente
{
    public int IdUsuarios { get; set; }

    public virtual Usuario IdUsuariosNavigation { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<Turno> Turnos { get; set; } = new List<Turno>();
}
