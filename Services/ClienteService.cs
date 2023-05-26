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
                TipoCuenta = t.IdUsuariosNavigation.TipoCuenta
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Cliente?> Create(Cliente clienteNuevo)
    {
        clienteNuevo.IdUsuariosNavigation.Pass = PH.hashPassword(clienteNuevo.IdUsuariosNavigation.Pass);
        clienteNuevo.IdUsuariosNavigation.Nombre = NN.ConvertirNombre(clienteNuevo.IdUsuariosNavigation.Nombre);
        clienteNuevo.IdUsuariosNavigation.Apellido = NN.ConvertirNombre(clienteNuevo.IdUsuariosNavigation.Apellido);
        _context.Clientes.Add(clienteNuevo);

        await _context.SaveChangesAsync();

        return clienteNuevo;
    }

}