
namespace SATURNO_V2.Data.DTOs.ProfesionalDTO;

public class ProfesionalStatusDto_In
{
    public string Username { get; set; } = null!;
    public bool EstadoSub { get; set; }
}

public class ProfesionalStatusDto_Out
{
    public string Username { get; set; } = null!;

    public bool EstadoSub { get; set; }

    public bool? Verificado { get; set; }

}
