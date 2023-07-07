using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SATURNO_V2.Data.DTOs;

public class UsuarioUpdateMailDTO
{
    public string Mail { get; set; } = null!;

}
public class UsuarioUpdatePasswordDTO
{
    public string OldPass { get; set; } = null!;
    public string NewPass { get; set; } = null!;
    public string SameNew { get; set; } = null!;

}
public class UsuarioRecoveryPasswordDTO
{
    public string NewPass { get; set; } = null!;
    public string SameNew { get; set; } = null!;

}
