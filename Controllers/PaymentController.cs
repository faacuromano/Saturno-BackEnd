using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("[controller]")]

public class PaymentController : ControllerBase
{
    private readonly ControllerService _service;
    public PaymentController(ControllerService service)
    {
        _service = service;
    }

}