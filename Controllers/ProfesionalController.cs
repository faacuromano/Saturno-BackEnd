using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Functions;
using SATURNO_V2.Data.DTOs.ProfesionalDTO;
using System.Globalization;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("profesional")]

public class ProfesionalController : ControllerBase
{
    private readonly ProfesionalService _service;

    private IConfiguration config;

    public ProfesionalController(ProfesionalService service, IConfiguration config)
    {
        _service = service;
        this.config = config;
    }

    [HttpGet]
    public async Task<IEnumerable<ProfesionalDto>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("horarios/{username}/{fecha}")]
    public async Task<IDictionary<DateTime, IEnumerable<string>>> GetInicioCierre(string username, int id, DateTime fecha, [FromServices] ProfesionalService profesionalService)
    {
        return await profesionalService.GetHorariosDisponibles(username, id, fecha);
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

    [HttpPost]
    public async Task<IActionResult> Create(Profesionale profesional)
    {
        bool verficacion = VF.validateProfessional(profesional);

        if (verficacion is true)
        {
            var profesionalNuevo = await _service.Create(profesional);

            if (profesionalNuevo is null)
            {
                return BadRequest("El objeto profesional se recibio como null.");
            }
            else
            {
                return CreatedAtAction(nameof(GetByUsername), new { username = profesionalNuevo.IdUsuariosNavigation.Username }, profesionalNuevo);
            }
        }
        else
        {
            return BadRequest("Uno o mas campos invalidos ingresados en la request.");
        }

    }

    [HttpPut("{username}")]
    [Authorize]
    public async Task<IActionResult> Update(string username, ProfesionalDtoUpdate profesionalDtoIn)
    {
        // Verificar que el usuario actual coincide con el usuario del token
        var currentUser = HttpContext?.User?.Identity?.Name;
        var profesionalUpdate = await _service.GetByUsernameToFunction(username);

        if (profesionalUpdate is null)
        {
            return NotFound("El usuario no existe");
        }

        if (currentUser == username || currentUser == "admin")
        {
            await _service.Update(username, profesionalDtoIn);
            return Ok("Los cambios se han aplicado");
        }
        else
        {
            return Unauthorized("No podes realizar cambios sobre este usuario.");
        }

    }

    [HttpPut("/activarSubscripcion/{username}")]
    public async Task<IActionResult> UpdateStatus(string username)
    {
        var usuarioUpdate = await _service.GetByUsername(username);

        if (usuarioUpdate is null)
        {
            return NotFound("El usuario no existe");
        }
        else
        {
            await _service.UpdateStatus(username);
            return Ok("Cuenta activada exitosamente!");
        }
    }


    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> Delete(string username)
    {
        var profesionalDelete = await _service.GetByUsernameToFunction(username);
        var currentUser = HttpContext?.User?.Identity?.Name;
        bool isAdmin = HttpContext?.User?.FindFirst(ClaimTypes.Actor)?.Value == "A";


        if (currentUser == username || isAdmin)
        {
            if (profesionalDelete is not null)
            {
                await _service.Delete(username);
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
        else
        {
            return Unauthorized("No posees los permisos para ejecutar la acción");
        }
    }

}
