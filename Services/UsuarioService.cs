using Microsoft.EntityFrameworkCore;
using SATURNO_V2.Data;
using SATURNO_V2.Data.DTOs;
using SATURNO_V2.Data.SaturnoModels;

namespace SATURNO_V2.Services;

public class UsuarioService{

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
        return  await _context.Usuarios.FindAsync(id);
    }

    public async Task<Usuario?> Login(string username, string password)
    {
        var response = await _context.Usuarios.FirstOrDefaultAsync(u => u.Nombre == username && u.Pass == password);
        return response;
    }

    public async Task<Usuario?> Create(UsuarioDtoIn usuarioNuevoDto)
    {
        var nuevoUsuario = new Usuario();

        nuevoUsuario.Nombre = usuarioNuevoDto.Nombre;
        nuevoUsuario.Apellido = usuarioNuevoDto.Apellido;
        nuevoUsuario.Mail = usuarioNuevoDto.NumTelefono;
        nuevoUsuario.FechaNacimiento = usuarioNuevoDto.FechaNacimiento;
        nuevoUsuario.CreacionCuenta = usuarioNuevoDto.CreacionCuenta;
        nuevoUsuario.Pass = usuarioNuevoDto.Passw;
        nuevoUsuario.NumTelefono = usuarioNuevoDto.NumTelefono;
        nuevoUsuario.FotoPerfil = usuarioNuevoDto.FotoPerfil;
        nuevoUsuario.TipoCuenta = usuarioNuevoDto.TipoCuenta;
        nuevoUsuario.Username = usuarioNuevoDto.Username;

        _context.Usuarios.Add(nuevoUsuario);
        await _context.SaveChangesAsync();

        return nuevoUsuario;
    }

    public async Task Update(int id, Usuario usuario) 
    {
        var usuarioExistente = await GetById(id);

        if (usuarioExistente is not null)
        {
            usuarioExistente.Id = usuario.Id;
            usuarioExistente.Nombre = usuario.Nombre;
            usuarioExistente.Apellido = usuario.Apellido;
            usuarioExistente.Mail = usuario.Mail;
            usuarioExistente.Pass = usuario.Pass;
            usuarioExistente.NumTelefono = usuario.NumTelefono;
            usuarioExistente.FechaNacimiento = usuario.FechaNacimiento;
            usuarioExistente.FotoPerfil = usuario.FotoPerfil;
            usuarioExistente.TipoCuenta = usuario.TipoCuenta;

            await _context.SaveChangesAsync();
        }
    }    

    public async Task Delete(int id)
    {
        var usuarioDelete = await GetById(id);

        if(usuarioDelete is not null)
        {
            _context.Usuarios.Remove(usuarioDelete);
            await _context.SaveChangesAsync();
        }
    }
}