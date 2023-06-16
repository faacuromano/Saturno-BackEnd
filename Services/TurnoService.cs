using Microsoft.EntityFrameworkCore;
using SATURNO_V2.Data;
using SATURNO_V2.Data.DTOs;
using SATURNO_V2.Data.DTOs.TurnoDTO;
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
            FechaTurno = t.FechaTurno
        }).ToListAsync();

    }

    public async Task<Turno?> GetByIdToFunction(int id)
    {
        return await _context.Turnos.FindAsync(id);
    }

    public async Task<IEnumerable<object?>> GetByProfesional(string username, int estado)
    {
        // Consulta base para todos los casos
        var query = _context.Turnos
            .Where(p => p.IdProfesionalesNavigation.IdUsuariosNavigation.Username == username || p.IdClientesNavigation.IdUsuariosNavigation.Username == username)
            .Select(t => new TurnoDtoOut
            {
                NombreCliente = t.IdClientesNavigation.IdUsuariosNavigation.Nombre + " " + t.IdClientesNavigation.IdUsuariosNavigation.Apellido,
                NombreProfesional = t.IdProfesionalesNavigation.IdUsuariosNavigation.Nombre + " " + t.IdProfesionalesNavigation.IdUsuariosNavigation.Apellido,
                NombreServicio = t.IdServiciosNavigation.Nombre,
                Monto = t.IdServiciosNavigation.Precio,
                Observaciones = t.Observaciones,
                HoraTurno = t.HoraTurno,
                FechaTurno = t.FechaTurno
            });

        switch (estado)
        {
            case 0:
                // Caso 0: Todos los turnos sin filtrar
                return await query.ToListAsync();

            case 1:
                // Caso 1: Turnos pasados (anterior a la fecha actual)
                query = query.Where(t => t.FechaTurno < DateTime.Now);
                return await query.ToListAsync();

            case 2:
                // Caso 2: Turnos futuros (posterior o igual a la fecha actual)
                query = query.Where(t => t.FechaTurno >= DateTime.Now);
                return await query.ToListAsync();

            case 3:
                // Caso 3: Turnos del día actual (exactamente igual a la fecha actual)
                query = query.Where(t => t.FechaTurno == DateTime.Now);
                return await query.ToListAsync();

            default:
                // Caso por defecto: Valor de estado inválido, se devuelve un mensaje de error
                return new List<object>
            {
                new ErrorMessage("El parámetro 'estado' (" + estado + ") es inválido.")
            };
        };

    }
    public class ErrorMessage
    {
        public string Message { get; set; }

        public ErrorMessage(string message)
        {
            Message = message;
        }
    }


    public async Task<Turno?> Create(TurnoDtoIn turnoNuevoDTO)
    {
        var profesional = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == turnoNuevoDTO.UsernameProfesional);
        var cliente = await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == turnoNuevoDTO.UsernameCliente);

        if (profesional is null || cliente is null)
        {
            return null; // O manejar el escenario de error de alguna otra forma
        }

        var turnoNuevo = new Turno();
        turnoNuevo.FechaTurno = turnoNuevoDTO.FechaTurno;
        turnoNuevo.HoraTurno = turnoNuevoDTO.HoraTurno;
        turnoNuevo.Observaciones = turnoNuevoDTO.Observaciones;
        turnoNuevo.IdProfesionales = profesional.Id;
        turnoNuevo.IdClientes = cliente.Id;
        turnoNuevo.IdServicios = turnoNuevoDTO.IdServicios;

        _context.Turnos.Add(turnoNuevo);
        await _context.SaveChangesAsync();

        return turnoNuevo;
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