using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SATURNO_V2.Data;
using SATURNO_V2.Data.DTOs;
using SATURNO_V2.Data.SaturnoModels;

namespace SATURNO_V2.Services;

public class UsuarioService
{

    private readonly SaturnoV2Context _context;

    public UsuarioService(SaturnoV2Context context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Usuario>> GetAll()
    {
        return await _context.Usuarios.ToListAsync();
    }

    public async Task<Usuario?> GetById(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<UsuarioDtoOut?> GetByIdToFunction(int id)
    {
        return await _context.Usuarios
       .Where(p => p.Id == id)
       .Select(t => new UsuarioDtoOut
       {
           Nombre = t.Nombre,
           Apellido = t.Apellido,
           Mail = t.Mail,
           NumTelefono = t.NumTelefono,
           FechaNacimiento = t.FechaNacimiento,
           Ubicacion = t.Ubicacion,
           FotoPerfil = t.FotoPerfil,
       })
       .FirstOrDefaultAsync();
    }


    public async Task<Usuario?> Create(UsuarioDtoIn usuarioNuevoDto)
    {
        var nuevoUsuario = new Usuario();

        nuevoUsuario.Nombre = usuarioNuevoDto.Nombre;
        nuevoUsuario.Apellido = usuarioNuevoDto.Apellido;
        nuevoUsuario.Mail = usuarioNuevoDto.Mail;
        nuevoUsuario.FechaNacimiento = usuarioNuevoDto.FechaNacimiento;
        nuevoUsuario.CreacionCuenta = DateTime.Now;
        nuevoUsuario.Pass = hashPassword(usuarioNuevoDto.Passw);
        nuevoUsuario.NumTelefono = usuarioNuevoDto.NumTelefono;
        nuevoUsuario.FotoPerfil = usuarioNuevoDto.FotoPerfil;
        nuevoUsuario.Ubicacion = usuarioNuevoDto.Ubicacion;
        nuevoUsuario.TipoCuenta = usuarioNuevoDto.TipoCuenta;
        nuevoUsuario.Username = usuarioNuevoDto.Username;

        _context.Usuarios.Add(nuevoUsuario);
        await _context.SaveChangesAsync();

        return nuevoUsuario;
    }

    public async Task Update(int id, UsuarioDtoOut usuario)
    {
        var usuarioExistente = await GetById(id);

        if (usuarioExistente is not null)
        {
            usuarioExistente.Id = usuario.Id;
            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Username = usuario.Username;
            usuarioExistente.Apellido = usuario.Apellido;
            usuarioExistente.Mail = usuario.Mail;
            usuarioExistente.Ubicacion = usuario.Ubicacion;
            usuarioExistente.NumTelefono = usuario.NumTelefono;
            usuarioExistente.FechaNacimiento = usuario.FechaNacimiento;


            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var usuarioDelete = await GetById(id);

        if (usuarioDelete is not null)
        {
            _context.Usuarios.Remove(usuarioDelete);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<Usuario?> Login(string username, string password)
    {
        var response = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username && u.Pass == hashPassword(password));
        return response;
    }

    string hashPassword(string password)
    {
        var sha = SHA256.Create();

        var asByteArray = Encoding.Default.GetBytes(password);
        var hashedPassword = sha.ComputeHash(asByteArray);

        return Convert.ToBase64String(hashedPassword);
    }


}

