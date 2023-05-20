using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Data.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("[controller]")]

public class UsuarioController : ControllerBase
{
    private readonly UsuarioService _service;
    private IConfiguration config;
    public UsuarioController(UsuarioService service, IConfiguration config)
    {
        _service = service;
        this.config = config;
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
        var user = await _service.Login(username, password);

        if (user is not null)
        {
            string jwtToken = GenerateToken(user);
            return Ok(new { token = jwtToken, user });
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

        return CreatedAtAction(nameof(GetById), new { id = usuarioNuevo.Id }, usuarioNuevo);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, UsuarioDtoOut usuario)
    {

        var usuarioUpdate = await _service.GetByIdToFunction(id);

        if (usuarioUpdate is not null)
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

        if (usuarioDelete is not null)
        {
            await _service.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

    private string GenerateToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario.Username),
            new Claim(ClaimTypes.Email, usuario.Mail)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var securityToken = new JwtSecurityToken(
                            claims: claims,
                            expires: DateTime.Now.AddMinutes(60),
                            signingCredentials: creds);

        string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }
}
