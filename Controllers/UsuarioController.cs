using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Data.DTOs;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("[controller]")]

public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _service;
    public UsuarioController(UsuarioService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<Usuario>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Usuario>> GetById(int id)
    {
        var usuario = await _service.GetById(id);
       
        if (usuario is not null) 
        { 
            return usuario; 
        }
        else
        { 
            return NotFound();
        }

    }

    [HttpGet("2/{id}")]
    public async Task<ActionResult<UsuarioDtoOut>> GetByIdToFunction(int id)
    {
        var usuario = await _service.GetByIdToFunction(id);
       
        if (usuario is not null) 
        { 
            return usuario; 
        }
        else
        { 
            return NotFound();
        }

    }

    [HttpGet("login")]
    public async Task<ActionResult<Usuario>> Login(string username, string password)
    {
        var loginStatus = await _service.Login(username, password);
       
        if (loginStatus is not null) 
        { 
            return loginStatus; 
        }
        else
        { 
            return NotFound("Credenciales Incorrectas");
        }
        }


    [HttpPost]    
    public async Task<IActionResult> Create(UsuarioDtoIn usuario)
    {
        var usuarioNuevo = await _service.Create(usuario);

        return CreatedAtAction(nameof(GetById), new {id = usuarioNuevo.Id}, usuarioNuevo);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UsuarioDtoOut usuario)
    {

        var usuarioUpdate = await _service.GetByIdToFunction(id);

        if (usuarioUpdate is not null )
        {
            await _service.Update(id, usuario);
            return Ok("Los cambios se han aplicado");
        }
        else
        {
            return NotFound("Hubo un error al realizar los cambios");
        }
    }


    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var usuarioDelete = await _service.GetById(id);

        if (usuarioDelete is not null )
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
