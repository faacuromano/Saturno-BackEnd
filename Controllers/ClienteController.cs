using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Data.DTOs;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("cliente")]

public class ClienteController : ControllerBase
{
    private readonly ClienteService _service;
    public ClienteController(ClienteService service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IEnumerable<ClienteDto>> Get()
    {
        return await _service.GetAll();
    }


    [HttpGet("id/{id}")]
    public async Task<ActionResult<ClienteDto>> GetById(int id)
    {
        var cliente = await _service.GetById(id);

        if (cliente is not null)
        {
            return cliente;
        }
        else
        {
            return NotFound();
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(Cliente cliente)
    {
        var clienteNuevo = await _service.Create(cliente);

        if (clienteNuevo is not null)
        {
            return CreatedAtAction(nameof(GetById), new { id = clienteNuevo.IdUsuarios }, clienteNuevo);
        }
        else
        {
            return BadRequest();
        }
    }

}
