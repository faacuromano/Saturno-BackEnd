using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Data.DTOs;
using SATURNO_V2.Functions;
using SATURNO_V2.Data.DTOs.ClientDto;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("cliente")]

public class ClienteController : ControllerBase
{
    private readonly ClienteService _service;
    private IConfiguration config;
    public ClienteController(ClienteService service, IConfiguration config)
    {
        _service = service;
        this.config = config;
    }


    [HttpGet]
    public async Task<IEnumerable<ClienteDto>> Get()
    {
        return await _service.GetAll();
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

    [HttpGet("/perfilDe/{username}")]
    public async Task<ActionResult<ClientePerfilDto>> GetPerfil(string username)
    {
        var cliente = await _service.GetPerfilCliente(username);

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
        bool clientIsValid = VF.validateCliente(cliente);


        if (clientIsValid)
        {
            var clienteNuevo = await _service.Create(cliente);

            if (clienteNuevo is not null)
            {
                return CreatedAtAction(nameof(GetByUsername), new { username = clienteNuevo.IdUsuariosNavigation.Username }, clienteNuevo);
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

    [Authorize]
    [HttpDelete]
    public async Task<IActionResult> Delete(string username)
    {
        var clienteDelete = await _service.GetByUsernameToFunction(username);
        var currentUser = HttpContext?.User?.Identity?.Name;
        bool isAdmin = HttpContext?.User?.FindFirst(ClaimTypes.Actor)?.Value == "A";

        if (currentUser == username || isAdmin)
        {
            if (clienteDelete is not null)
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
            return Unauthorized("No posees permisos para realizar la acción");
        }
    }

}
