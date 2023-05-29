using Microsoft.EntityFrameworkCore;
using SATURNO_V2.Data;
using SATURNO_V2.Data.DTOs;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Functions;

namespace SATURNO_V2.Services;

public class TurnoService
{

    private readonly SaturnoV2Context _context;

    public TurnoService(SaturnoV2Context context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TurnoDtoOut>> GetAll()
    {
        return await _context.Turnos.Select(t => new TurnoDtoOut
        {
            NombreCliente = t.IdClientesNavigation.IdUsuariosNavigation.Nombre
                            + " "
                            + t.IdClientesNavigation.IdUsuariosNavigation.Apellido,

            NombreProfesional = t.IdProfesionalesNavigation.IdUsuariosNavigation.Nombre
                                + " "
                                + t.IdProfesionalesNavigation.IdUsuariosNavigation.Apellido,

            NombreServicio = t.IdServiciosNavigation.Nombre,
            Monto = t.IdServiciosNavigation.Precio,
            Observaciones = t.Observaciones,
            HoraTurno = t.HoraTurno,
            FechaTurno = FP.FechaParse(t.FechaTurno)
        }).ToListAsync();

    }

    public async Task<Turno?> GetByIdToFunction(int id)
    {
        return await _context.Turnos.FindAsync(id);
    }

    public async Task<IEnumerable<TurnoDtoOut?>> GetByProfesional(string username)
    {
        return await _context.Turnos
            .Where(p => p.IdProfesionalesNavigation != null && p.IdProfesionalesNavigation.IdUsuariosNavigation.Username == username ||
                         p.IdClientesNavigation != null && p.IdClientesNavigation.IdUsuariosNavigation.Username == username)
            .Select(t => new TurnoDtoOut
            {
                NombreCliente = t.IdClientesNavigation.IdUsuariosNavigation.Nombre
                                + " "
                                + t.IdClientesNavigation.IdUsuariosNavigation.Apellido,

                NombreProfesional = t.IdProfesionalesNavigation.IdUsuariosNavigation.Nombre
                                    + " "
                                    + t.IdProfesionalesNavigation.IdUsuariosNavigation.Apellido,

                NombreServicio = t.IdServiciosNavigation.Nombre,
                Monto = t.IdServiciosNavigation.Precio,
                Observaciones = t.Observaciones,
                HoraTurno = t.HoraTurno,
                FechaTurno = FP.FechaParse(t.FechaTurno)
            })
            .ToListAsync();
    }

    public async Task<Turno?> Create(TurnoDtoIn turnoNuevoDTO)
    {
        var turnoNuevo = new Turno();
        turnoNuevo.FechaTurno = turnoNuevoDTO.FechaTurno;
        turnoNuevo.HoraTurno = turnoNuevoDTO.HoraTurno;
        turnoNuevo.Observaciones = turnoNuevoDTO.Observaciones;
        turnoNuevo.IdProfesionales = turnoNuevoDTO.IdProfesionales;
        turnoNuevo.IdClientes = turnoNuevoDTO.IdClientes;
        turnoNuevo.IdServicios = turnoNuevoDTO.IdServicios;
        _context.Turnos.Add(turnoNuevo);

        await _context.SaveChangesAsync();

        return turnoNuevo;
    }

    public async Task Update(int id, Turno turnoDto)
    {
        var profesionalExistente = await GetByIdToFunction(id);

        if (profesionalExistente is not null)
        {
            profesionalExistente.IdClientes = turnoDto.IdClientes;
            profesionalExistente.IdProfesionales = turnoDto.IdProfesionales;
            profesionalExistente.IdServicios = turnoDto.IdServicios;
            profesionalExistente.Observaciones = turnoDto.Observaciones;
            profesionalExistente.HoraTurno = turnoDto.HoraTurno;
            profesionalExistente.FechaTurno = turnoDto.FechaTurno;

            await _context.SaveChangesAsync();
        }
    }
    public async Task Delete(int id)
    {
        var ususarioDelete = await GetByIdToFunction(id);

        if (ususarioDelete is not null)
        {
            _context.Turnos.Remove(ususarioDelete);
            await _context.SaveChangesAsync();
        }
    }
}