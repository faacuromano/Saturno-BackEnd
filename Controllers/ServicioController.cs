using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("[controller]")]

public class ServicioController : ControllerBase
{
    private readonly ProfesionalService _service;
    public ServicioController(ProfesionalService service)
    {
        _service = service;
    }

}
