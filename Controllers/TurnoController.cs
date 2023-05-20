using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("[controller]")]

public class TurnoController : ControllerBase
{
    private readonly TurnoService _service;
    public TurnoController(TurnoService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<Turno>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Turno>> GetById(int id)
    {
        var turno = await _service.GetById(id);

        if (turno is not null)
        {
            return turno;
        }
        else
        {
            return NotFound();
        }

    }

    [HttpPost]
    public async Task<IActionResult> Create(Turno turno)
    {
        var turnoNuevo = await _service.Create(turno);

        if (turnoNuevo is not null)
        {
            return CreatedAtAction(nameof(GetById), new { id = turnoNuevo.Id }, turnoNuevo);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, Turno turno)
    {
        if (id != turno.Id)
        {
            return BadRequest();
        }

        await _service.Update(id, turno);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var turnoDelete = await _service.GetById(id);

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
