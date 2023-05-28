using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Data.DTOs;
using SATURNO_V2.Functions;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("profesional")]

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

    [HttpGet("{username}")]
    public async Task<ActionResult<ProfesionalDto>> GetByUsername(string username)
    {
        var profesional = await _service.GetByUsername(username);

        if (profesional is not null)
        {
            return profesional;
        }
        else
        {
            return NotFound();
        }

    }
    [HttpGet("id/{id}")]
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
        bool verficacion = VP.validateProfessional(profesional);

        if (verficacion is true)
        {
            var profesionalNuevo = await _service.Create(profesional);

            if (profesionalNuevo is not null)
            {
                return CreatedAtAction(nameof(GetById), new { id = profesionalNuevo.IdUsuarios }, profesionalNuevo);
            }
            else
            {
                return BadRequest("El objeto profesional se recibio como null.");
            }
        }
        else
        {
            return BadRequest("Uno o mas campos invalidos ingresados en la request.");
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProfesionalDtoUpdate profesionalDtoIn)
    {
        if (id != profesionalDtoIn.IdUsuarios)
        {
            return BadRequest();
        }

        await _service.Update(id, profesionalDtoIn);
        return Ok();
    }

    [HttpGet("/profesionalServices/{id}")]
    public async Task<IEnumerable<Servicio>> GetServiceToDelete(int id)
    {
        var servicio = await _service.GetServiceToDelete(id);

        return servicio;
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
