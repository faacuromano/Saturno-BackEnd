using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.DTOs.TurnoDTO;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("turno")]

public class TurnoController : ControllerBase
{
    private readonly TurnoService _service;
    public TurnoController(TurnoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<TurnoDtoOut>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("/turnosDe/{username}")]
    public async Task<ActionResult<IEnumerable<TurnoDtoOut?>>> GetByProfesional(string username, int estado)
    {
        var turno = await _service.GetByProfesional(username, estado);

        if (turno is not null)
        {
            if (turno.Count() > 0)
            {
                return Ok(turno);
            }
            else
            {
                return BadRequest(new { error = "No se encontraron turnos para el usuario especificado." });
            }
        }
        else
        {
            return BadRequest(new { error = "Turno es null." });

        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(TurnoDtoIn turno)
    {
        var turnoNuevo = await _service.Create(turno);

        if (turnoNuevo is not null)
        {
            return Ok(turnoNuevo);
        }
        else
        {
            return BadRequest("No se pudo crear el turno"); // O manejar el escenario de error de alguna otra forma
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var turnoDelete = await _service.GetByIdToFunction(id);

        if (turnoDelete is not null)
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
