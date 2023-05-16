using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("[controller]")]

public class PagoController : ControllerBase
{
    private readonly PagoService _service;
    public PagoController(PagoService service)
    {
        _service = service;
    }

}