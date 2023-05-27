using Microsoft.EntityFrameworkCore;
using SATURNO_V2.Data;
using SATURNO_V2.Data.DTOs;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Functions;

namespace SATURNO_V2.Services;

public class ClienteService
{

    private readonly SaturnoV2Context _context;

    public ClienteService(SaturnoV2Context context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ClienteDto>> GetAll()
    {
        return await _context.Clientes.Select(t => new ClienteDto
        {
            Nombre = t.IdUsuariosNavigation.Nombre,
            Apellido = t.IdUsuariosNavigation.Apellido,
            Username = t.IdUsuariosNavigation.Username,
            Mail = t.IdUsuariosNavigation.Mail,
            Pass = t.IdUsuariosNavigation.Pass,
            NumTelefono = t.IdUsuariosNavigation.NumTelefono,
            FechaNacimiento = t.IdUsuariosNavigation.FechaNacimiento,
            FotoPerfil = t.IdUsuariosNavigation.FotoPerfil,
            Verificado = t.IdUsuariosNavigation.Verificado,
            CreacionCuenta = t.IdUsuariosNavigation.CreacionCuenta,
            TipoCuenta = t.IdUsuariosNavigation.TipoCuenta,
            Ubicacion = t.IdUsuariosNavigation.Ubicacion
        }).ToListAsync();

    }

    public async Task<ClienteDto?> GetById(int id)
    {
        return await _context.Profesionales
            .Where(p => p.IdUsuariosNavigation.Id == id)
            .Select(t => new ClienteDto
            {
                IdUsuarios = t.IdUsuariosNavigation.Id,
                Nombre = t.IdUsuariosNavigation.Nombre,
                Apellido = t.IdUsuariosNavigation.Apellido,
                Username = t.IdUsuariosNavigation.Username,
                Mail = t.IdUsuariosNavigation.Mail,
                Pass = t.IdUsuariosNavigation.Pass,
                NumTelefono = t.IdUsuariosNavigation.NumTelefono,
                FechaNacimiento = t.IdUsuariosNavigation.FechaNacimiento,
                FotoPerfil = t.IdUsuariosNavigation.FotoPerfil,
                Verificado = t.IdUsuariosNavigation.Verificado,
                CreacionCuenta = t.IdUsuariosNavigation.CreacionCuenta,
                Ubicacion = t.IdUsuariosNavigation.Ubicacion,
                TipoCuenta = t.IdUsuariosNavigation.TipoCuenta
            })
            .FirstOrDefaultAsync();
    }
    public async Task<Cliente?> GetByIdToFunction(int id)
    {
        return await _context.Clientes.FindAsync(id);
    }
    public async Task<Usuario?> GetUsuarioToDelete(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }
    public async Task<ClienteDto?> GetByUsername(string username)
    {
        return await _context.Clientes
            .Where(p => p.IdUsuariosNavigation.Username == username)
            .Select(t => new ClienteDto
            {
                IdUsuarios = t.IdUsuariosNavigation.Id,
                Nombre = t.IdUsuariosNavigation.Nombre,
                Apellido = t.IdUsuariosNavigation.Apellido,
                Username = t.IdUsuariosNavigation.Username,
                Mail = t.IdUsuariosNavigation.Mail,
                Pass = t.IdUsuariosNavigation.Pass,
                NumTelefono = t.IdUsuariosNavigation.NumTelefono,
                FechaNacimiento = t.IdUsuariosNavigation.FechaNacimiento,
                FotoPerfil = t.IdUsuariosNavigation.FotoPerfil,
                Verificado = t.IdUsuariosNavigation.Verificado,
                CreacionCuenta = t.IdUsuariosNavigation.CreacionCuenta,
                Ubicacion = t.IdUsuariosNavigation.Ubicacion,
                TipoCuenta = t.IdUsuariosNavigation.TipoCuenta
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Cliente?> Create(Cliente clienteNuevo)
    {
        clienteNuevo.IdUsuariosNavigation.Pass = PH.hashPassword(clienteNuevo.IdUsuariosNavigation.Pass);
        clienteNuevo.IdUsuariosNavigation.Nombre = NN.ConvertirNombre(clienteNuevo.IdUsuariosNavigation.Nombre);
        clienteNuevo.IdUsuariosNavigation.Apellido = NN.ConvertirNombre(clienteNuevo.IdUsuariosNavigation.Apellido);
        clienteNuevo.IdUsuariosNavigation.CreacionCuenta = DateTime.Now;
        _context.Clientes.Add(clienteNuevo);

        await _context.SaveChangesAsync();

        return clienteNuevo;
    }

    public async Task Delete(int id)
    {
        var ClieteToDelete = await GetByIdToFunction(id);
        var usuarioDelete = await GetUsuarioToDelete(id);

        if (ClieteToDelete is not null)
        {
            _context.Clientes.Remove(ClieteToDelete);
            _context.Usuarios.Remove(usuarioDelete);

            await _context.SaveChangesAsync();
        }
    }

}