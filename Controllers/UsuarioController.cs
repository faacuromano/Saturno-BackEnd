using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Data.DTOs;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using SATURNO_V2.Functions;
using Microsoft.AspNetCore.Authorization;

namespace SATURNO_V2.Controllers
{
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

        [HttpGet("{username}")]
        [Authorize]
        public async Task<ActionResult<UsuarioDtoOut>> GetByUsername(string username)
        {
            var currentUser = HttpContext.User.Identity.Name;
            var usuario = await _service.GetByUsername(username);


            if (usuario is null)
            {
                return NotFound();
            }
            else
            {
                if (currentUser == username)
                {
                    return usuario;
                }
                else
                {
                    return Unauthorized("No puedes ver la informacion de este usuario.");
                }
            }
        }

        [HttpGet("login")]
        public async Task<ActionResult<UsuarioLoginDto>> Login(string username, string password)
        {
            var user = await _service.Login(username, password);

            if (user is null)
            {
                return NotFound("Credenciales Incorrectas");
            }
            else
            {
                string jwtToken = GenerateToken(user);
                return Ok(new { token = (jwtToken), user });
            }
        }

        [HttpPut("{username}")]
        [Authorize]
        public async Task<IActionResult> Update(string username, UsuarioDtoOut usuario)
        {
            var currentUser = HttpContext.User.Identity.Name;
            var usuarioUpdate = await _service.GetByUsernameToFunction(username);

            if (usuarioUpdate is null)
            {
                return NotFound("El usuario no existe");
            }

            if (currentUser == username)
            {
                await _service.Update(username, usuario);
                return Ok("Los cambios se han aplicado");
            }
            else
            {
                return Unauthorized("No puedes realizar cambios sobre este usuario.");
            }

        }

        [HttpPut("updateMail/{username}")]
        [Authorize]
        public async Task<IActionResult> UpdateMail(string username, UsuarioUpdateMailDTO usuario)
        {
            var currentUser = HttpContext.User.Identity.Name;
            var usuarioUpdate = await _service.GetByUsernameToFunction(username);
            var isValid = UsuarioService.VerificarCorreo(usuario.Mail);

            if (usuarioUpdate is null)
            {
                return NotFound("El usuario no existe.");
            }

            if (currentUser != username)
            {
                return Unauthorized("No puedes realizar cambios sobre este usuario.");
            }
            else
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
        }

        [HttpPut("updateVerficado/{username}")]
        [Authorize]
        public async Task<IActionResult> UpdateVerficado(string username)
        {
            var currentUser = HttpContext.User.Identity.Name;
            var usuarioUpdate = await _service.GetByUsernameToFunction(username);

            if (usuarioUpdate is null)
            {
                return BadRequest("La verificacion no ha podido completarse");
            }

            if (currentUser == username)
            {
                await _service.UpdateVerificado(username);
                return Ok("Verficacion correcta");
            }
            else
            {
                return Unauthorized("No puedes realizar cambios sobre este usuario");
            }

        }

        [HttpPut("updatePassword/{username}")]
        [Authorize]
        public async Task<IActionResult> UpdatePassword(string username, UsuarioUpdatePasswordDTO usuario)
        {
            var currentUser = HttpContext.User.Identity.Name;
            var usuarioUpdate = await _service.GetByUsernameToFunction(username);
            var oldPassword = PH.hashPassword(usuario.OldPass);

            if (usuarioUpdate is null)
            {
                return NotFound("El usuario no existe");
            }

            if (currentUser != username)
            {
                return Unauthorized("No puedes realizar cambios sobre este usuario.");
            }
            else if (usuarioUpdate.Pass == oldPassword && usuario.NewPass == usuario.SameNew)
            {
                await _service.UpdatePassword(username, usuario);
                return Ok("Contraseña cambiada con exito.");
            }
            else
            {
                return BadRequest("La contraseña anterior no es correcta o las nuevas no coinciden");
            }
        }

        private string GenerateToken(UsuarioLoginDto usuario)
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
                expires: DateTime.Now.AddMinutes(4),
                signingCredentials: creds);

            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);

            return token;
        }


    }
}
