using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Mvc;

using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;

using MercadoPago.Config;
using MercadoPago.Client.Payment;
using MercadoPago.Resource.Payment;


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

    //[HttpPost]
   
}
