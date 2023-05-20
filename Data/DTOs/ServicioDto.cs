using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using SATURNO_V2.Data.SaturnoModels;

namespace SATURNO_V2.Data.DTOs;

public class ServicioDto
{

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public string Username { get; set; } = null!;

    public decimal Precio { get; set; }

    public TimeSpan Duracion { get; set; }

    [JsonIgnore]
    public virtual ICollection<Profesionale> Profesionales { get; set; } = new List<Profesionale>();

}
