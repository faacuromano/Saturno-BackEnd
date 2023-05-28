using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Data.DTOs;
using SATURNO_V2.Functions;

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

    [HttpGet("{username}")]
    public async Task<ActionResult<ClienteDto>> GetByUsername(string username)
    {
        var cliente = await _service.GetByUsername(username);

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
        bool clientIsValid = VC.validateCliente(cliente);


        if (clientIsValid)
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
        else
        {
            return BadRequest("Uno o mas campos invalidos en la requiest");
        }

    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var clienteDelete = await _service.GetById(id);

        if (clienteDelete is not null)
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
