using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using SATURNO_V2.Data;
using SATURNO_V2.Data.DTOs;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Functions;

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
    public async Task<Usuario?> GetByUsername(string username)
    {
        return await _context.Usuarios
        .Where(p => p.Username == username)
        .FirstOrDefaultAsync();
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
           Username = t.Username,
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

        nuevoUsuario.Nombre = NN.ConvertirNombre(usuarioNuevoDto.Nombre);
        nuevoUsuario.Apellido = NN.ConvertirNombre(usuarioNuevoDto.Nombre);
        nuevoUsuario.Mail = usuarioNuevoDto.Mail;
        nuevoUsuario.FechaNacimiento = usuarioNuevoDto.FechaNacimiento;
        nuevoUsuario.CreacionCuenta = DateTime.Now;
        nuevoUsuario.Pass = PH.hashPassword(usuarioNuevoDto.Passw);
        nuevoUsuario.NumTelefono = usuarioNuevoDto.NumTelefono;
        nuevoUsuario.FotoPerfil = usuarioNuevoDto.FotoPerfil;
        nuevoUsuario.Ubicacion = usuarioNuevoDto.Ubicacion;
        nuevoUsuario.TipoCuenta = usuarioNuevoDto.TipoCuenta;
        nuevoUsuario.Username = usuarioNuevoDto.Username;



        _context.Usuarios.Add(nuevoUsuario);
        await _context.SaveChangesAsync();

        return nuevoUsuario;
    }

    public async Task Update(string username, UsuarioDtoOut usuario)
    {
        var usuarioExistente = await GetByUsername(username);

        if (usuarioExistente is not null)
        {
            usuarioExistente.Nombre = NN.ConvertirNombre(usuario.Nombre);
            usuarioExistente.Apellido = NN.ConvertirNombre(usuario.Apellido);
            usuarioExistente.Username = usuario.Username;
            usuarioExistente.Ubicacion = usuario.Ubicacion;
            usuarioExistente.NumTelefono = usuario.NumTelefono;
            usuarioExistente.FechaNacimiento = usuario.FechaNacimiento;


            await _context.SaveChangesAsync();
        }
    }
    public async Task UpdateMail(string username, UsuarioUpdateMailDTO usuario)
    {
        var usuarioExistente = await GetByUsername(username);

        if (usuarioExistente is not null)
        {
            usuarioExistente.Mail = VerificarCorreo(usuario.Mail);

            await _context.SaveChangesAsync();
        }
    }
    public async Task UpdatePassword(string username, UsuarioUpdatePasswordDTO usuario)
    {
        var usuarioExistente = await GetByUsername(username);

        if (usuarioExistente is not null)
        {
            usuarioExistente.Pass = PH.hashPassword(usuario.NewPass);

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
        var response = await _context.Usuarios
                            .FirstOrDefaultAsync(u => u.Username == username && u.Pass == PH.hashPassword(password));
        return response;
    }

    static string VerificarCorreo(string correo)
    {
        // Expresión regular para verificar el correo electrónico
        string patron = @"^[a-zA-Z0-9._%+-]+@(gmail|hotmail|outlook|yahoo|aol)\.com$";

        // Verificar si el correo cumple con el patrón
        bool esValido = Regex.IsMatch(correo, patron, RegexOptions.IgnoreCase);

        if (esValido is true)
        {
            return correo;
        }
        else
        {
            return "El mail ingresado no es valido";
        }
    }



}

