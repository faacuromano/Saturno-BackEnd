using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("lista")]

public class ListaController : ControllerBase
{
    private readonly ListaServices _service;
    public ListaController(ListaServices service)
    {
        _service = service;
    }

    [HttpGet("/rubros")]
    public List<string> GetRubro()
    {
        var servicio = _service.GetRubro();

        return servicio;
    }

    [HttpGet("/ubicaciones")]
    public List<string> GetUbicaciones()
    {
        var servicio = _service.GetUbicaciones();

        return servicio;
    }
}
