using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Data.DTOs.ServicioDTO;
using SATURNO_V2.Data.DTOs.TurnoDTO;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("servicio")]

public class ServicioController : ControllerBase
{
    private readonly ServicioService _service;
    public ServicioController(ServicioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<ServicioDTO_getAll>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("/serviciosDe/{username}")]
    public async Task<IEnumerable<ServicioDto>> GetByProfesional(string username)
    {
        var servicio = await _service.GetByProfesional(username);

        return servicio;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Servicio servicio)
    {
        var servicioNuevo = await _service.Create(servicio);

        if (servicioNuevo is not null)
        {
            return Ok(servicioNuevo);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Servicio servicio)
    {
        if (id != servicio.Id)
        {
            return BadRequest();
        }

        await _service.Update(id, servicio);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var servicioDelete = await _service.GetById(id);

        if (servicioDelete is not null)
        {
            await _service.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }


}
