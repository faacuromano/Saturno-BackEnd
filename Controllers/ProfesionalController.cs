using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Data.DTOs;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("[controller]")]

public class ProfesionalController : ControllerBase
{
    private readonly ProfesionalService _service;
    public ProfesionalController(ProfesionalService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<ProfesionalDto>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("cuted/{n}")]
    public async Task<IEnumerable<ProfesionalDto>> GetFour(int n)
    {
        return await _service.GetFour(n);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProfesionalDto>> GetById(int id)
    {
        var profesional = await _service.GetById(id);

        if (profesional is not null)
        {
            return profesional;
        }
        else
        {
            return NotFound();
        }

    }

    [HttpPost]
    public async Task<IActionResult> Create(Profesionale profesional)
    {
        var profesionalNuevo = await _service.Create(profesional);

        if (profesionalNuevo is not null)
        {
            return CreatedAtAction(nameof(GetById), new { id = profesionalNuevo.IdUsuarios }, profesionalNuevo);
        }
        else
        {
            return BadRequest();
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProfesionalDtoIn profesionalDtoIn)
    {
        if (id != profesionalDtoIn.IdUsuarios)
        {
            return BadRequest();
        }

        await _service.Update(id, profesionalDtoIn);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var profesionalDelete = await _service.GetById(id);

        if (profesionalDelete is not null)
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
