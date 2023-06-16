namespace SATURNO_V2.Data.DTOs.TurnoDTO;

public partial class TurnoDtoOut
{

    public int Id { get; set; }

    public string NombreCliente { get; set; } = null!;

    public string NombreProfesional { get; set; } = null!;

    public string NombreServicio { get; set; } = null!;

    public decimal Monto { get; set; }

    public DateTime FechaTurno { get; set; }

    public TimeSpan? HoraTurno { get; set; }

    public string? Observaciones { get; set; }

}
