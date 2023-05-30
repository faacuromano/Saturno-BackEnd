namespace SATURNO_V2.Data.DTOs.TurnoDTO;

public partial class TurnoDtoOut
{
    public string NombreCliente { get; set; } = null!;

    public string NombreProfesional { get; set; } = null!;

    public string NombreServicio { get; set; } = null!;

    public decimal Monto { get; set; }

    public string FechaTurno { get; set; } = null!;

    public TimeSpan? HoraTurno { get; set; }

    public string? Observaciones { get; set; }

}
