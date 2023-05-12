using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("[controller]")]

public class ClienteController : ControllerBase
{
    private readonly ProfesionalService _service;
    public ClienteController(ProfesionalService service)
    {
        _service = service;
    }

}
