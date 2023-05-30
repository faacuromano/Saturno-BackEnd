using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Data.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using SATURNO_V2.Functions;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("usuario")]

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

    [HttpGet("/getId/{id}")]
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

    [HttpGet("login")]
    public async Task<ActionResult<UsuarioDtoOut>> Login(string username, string password)
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

    [HttpPut("{username}")]
    public async Task<IActionResult> Update(string username, UsuarioDtoOut usuario)
    {

        var usuarioUpdate = await _service.GetByUsername(username);

        if (usuarioUpdate is not null)
        {
            await _service.Update(username, usuario);
            return Ok("Los cambios se han aplicado");
        }
        else
        {
            return NotFound("Hubo un error al realizar los cambios");
        }
    }

    [HttpPut("updateMail/{username}")]
    public async Task<IActionResult> UpdateMail(string username, UsuarioUpdateMailDTO usuario)
    {
        var usuarioUpdate = await _service.GetByUsername(username);
        var isValid = UsuarioService.VerificarCorreo(usuario.Mail);

        if (usuarioUpdate is not null)
        {
            if (isValid)
            {
                await _service.UpdateMail(username, usuario);
                return Ok("Mail cambiado con exito.");
            }
            else
            {
                return BadRequest("El proveedor de correo no es valido u esta omitiendo uno de los siguientes elementos: [@] [.com]");
            }

        }
        else
        {
            return NotFound("Hubo un error al realizar los cambios.");
        }
    }

    [HttpPut("updatePassword/{username}")]
    public async Task<IActionResult> UpdatePassword(string username, UsuarioUpdatePasswordDTO usuario)
    {
        var usuarioUpdate = await _service.GetByUsername(username);
        var oldPassword = PH.hashPassword(usuario.OldPass);

        if (usuarioUpdate is not null
            && usuarioUpdate.Pass == oldPassword
            && usuario.NewPass == usuario.SameNew)
        {
            await _service.UpdatePassword(username, usuario);
            return Ok("Contraseña cambiada con exito.");
        }
        else
        {
            return NotFound("Hubo un error al realizar los cambios. Revise el campo email");
        }
    }

    private string GenerateToken(Usuario usuario)
    {
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, usuario.Username),
            new Claim(ClaimTypes.Email, usuario.Mail),
            new Claim("TipoCuenta", usuario.TipoCuenta),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config.GetSection("JWT:Key").Value));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var securityToken = new JwtSecurityToken(
                            claims: claims,
                            expires: DateTime.Now.AddHours(4),
                            signingCredentials: creds);

        string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }
}
