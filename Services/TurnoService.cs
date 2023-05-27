using Microsoft.EntityFrameworkCore;
using SATURNO_V2.Data;
using SATURNO_V2.Data.DTOs;
using SATURNO_V2.Data.SaturnoModels;

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
            NombreCliente = t.IdClientesNavigation.IdUsuariosNavigation.Nombre + " " + t.IdClientesNavigation.IdUsuariosNavigation.Apellido,
            NombreProfesional = t.IdProfesionalesNavigation.IdUsuariosNavigation.Nombre + " " + t.IdProfesionalesNavigation.IdUsuariosNavigation.Apellido,
            NombreServicio = t.IdServiciosNavigation.Nombre,
            Monto = t.IdServiciosNavigation.Precio,
            Observaciones = t.Observaciones,
            HoraTurno = t.HoraTurno,
            FechaTurno = CutFecha(t.FechaTurno)
        }).ToListAsync();

    }

    public async Task<IEnumerable<Turno>> GetFour(int n)
    {
        var professionalsToCut = await _context.Turnos.Select(t => new Turno
        {
            IdClientes = t.IdClientes,
            IdProfesionales = t.IdProfesionales,
            IdServicios = t.IdServicios,
            Observaciones = t.Observaciones,
            HoraTurno = t.HoraTurno,
            FechaTurno = t.FechaTurno
        }).ToListAsync();

        return professionalsToCut.Take(n).ToArray();
    }

    public async Task<Turno?> GetByIdToFunction(int id)
    {
        return await _context.Turnos.FindAsync(id);
    }

    public async Task<Turno?> GetById(int id)
    {
        return await _context.Turnos
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
    }
    public async Task<Turno?> GetByProfesional(string username)
    {
        return await _context.Turnos
            .Where(p => p.IdProfesionalesNavigation.IdUsuariosNavigation.Username == username)
            .FirstOrDefaultAsync();
    }

    public async Task<Turno?> Create(Turno turnoNuevo)
    {
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
    public static string CutFecha(DateTime fecha)
    {
        string toParse = fecha.ToString();
        string fechaParseada = toParse.Substring(0, toParse.Length - 12);

        return fechaParseada;
    }

}