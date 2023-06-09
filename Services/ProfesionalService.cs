using Microsoft.EntityFrameworkCore;
using SATURNO_V2.Data;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Functions;
using SATURNO_V2.Data.DTOs.ProfesionalDTO;
using SATURNO_V2.Data.DTOs.TurnoDTO;
using System.Globalization;

namespace SATURNO_V2.Services;

public class ProfesionalService
{

    private readonly SaturnoV2Context _context;

    public ProfesionalService(SaturnoV2Context context)
    {
        _context = context;
    }

    #region GetAll Y GetAll CUTED
    public async Task<IEnumerable<ProfesionalDto>> GetAll()
    {
        return await _context.Profesionales
        .Where(t => t.EstadoSub == true)
        .Select(t => new ProfesionalDto
        {
            IdUsuarios = t.IdUsuariosNavigation.Id,
            Nombre = t.IdUsuariosNavigation.Nombre,
            Apellido = t.IdUsuariosNavigation.Apellido,
            Username = t.IdUsuariosNavigation.Username,
            Mail = t.IdUsuariosNavigation.Mail,
            NumTelefono = t.IdUsuariosNavigation.NumTelefono,
            FechaNacimiento = t.IdUsuariosNavigation.FechaNacimiento,
            FotoPerfil = t.IdUsuariosNavigation.FotoPerfil,
            Verificado = t.IdUsuariosNavigation.Verificado,
            CreacionCuenta = t.IdUsuariosNavigation.CreacionCuenta,
            TipoCuenta = t.IdUsuariosNavigation.TipoCuenta,
            Ubicacion = t.IdUsuariosNavigation.Ubicacion,
            Descripcion = t.Descripcion,
            EstadoSub = t.EstadoSub,
            Profesion = t.Profesion,
            HorarioInicio = t.HorarioInicio,
            HorarioFinal = t.HorarioFinal,
            Direccion = t.Direccion,
            FotoBanner = t.FotoBanner,

        }).ToListAsync();
    }
    public async Task<IEnumerable<ProfesionalDto>> GetFour(int n)
    {
        var professionalsToCut = await _context.Profesionales
        .Where(t => t.EstadoSub == true)
        .Select(t => new ProfesionalDto
        {
            IdUsuarios = t.IdUsuariosNavigation.Id,
            Nombre = t.IdUsuariosNavigation.Nombre,
            Apellido = t.IdUsuariosNavigation.Apellido,
            Username = t.IdUsuariosNavigation.Username,
            Mail = t.IdUsuariosNavigation.Mail,
            NumTelefono = t.IdUsuariosNavigation.NumTelefono,
            FechaNacimiento = t.IdUsuariosNavigation.FechaNacimiento,
            FotoPerfil = t.IdUsuariosNavigation.FotoPerfil,
            Verificado = t.IdUsuariosNavigation.Verificado,
            CreacionCuenta = t.IdUsuariosNavigation.CreacionCuenta,
            TipoCuenta = t.IdUsuariosNavigation.TipoCuenta,
            Ubicacion = t.IdUsuariosNavigation.Ubicacion,
            Descripcion = t.Descripcion,
            Profesion = t.Profesion,
            HorarioInicio = t.HorarioInicio,
            HorarioFinal = t.HorarioFinal,
            Direccion = t.Direccion,
            FotoBanner = t.FotoBanner,

        }).ToListAsync();

        return professionalsToCut.Take(n).ToArray();
    }
    #endregion

    #region Get by ID
    public async Task<Profesionale?> GetByIdToFunction(int id)
    {
        return await _context.Profesionales.FindAsync(id);
    }
    public async Task<Profesionale?> GetByUsernameToFunction(string username)
    {
        return await _context.Profesionales
        .Where(p => p.IdUsuariosNavigation.Username == username)
        .FirstOrDefaultAsync();
    }
    #endregion

    #region Get by USERNAME
    public async Task<ProfesionalDto?> GetByUsername(string username)
    {
        return await _context.Profesionales
            .Where(p => p.IdUsuariosNavigation.Username == username)
            .Select(t => new ProfesionalDto
            {
                IdUsuarios = t.IdUsuariosNavigation.Id,
                Nombre = t.IdUsuariosNavigation.Nombre,
                Apellido = t.IdUsuariosNavigation.Apellido,
                Username = t.IdUsuariosNavigation.Username,
                Mail = t.IdUsuariosNavigation.Mail,
                Ubicacion = t.IdUsuariosNavigation.Ubicacion,
                NumTelefono = t.IdUsuariosNavigation.NumTelefono,
                FechaNacimiento = t.IdUsuariosNavigation.FechaNacimiento,
                FotoPerfil = t.IdUsuariosNavigation.FotoPerfil,
                Verificado = t.IdUsuariosNavigation.Verificado,
                CreacionCuenta = t.IdUsuariosNavigation.CreacionCuenta,
                TipoCuenta = t.IdUsuariosNavigation.TipoCuenta,
                Descripcion = t.Descripcion,
                EstadoSub = t.EstadoSub,
                Profesion = t.Profesion,
                HorarioInicio = t.HorarioInicio,
                HorarioFinal = t.HorarioFinal,
                FotoBanner = t.FotoBanner,
                Direccion = t.Direccion,
            })
            .FirstOrDefaultAsync();
    }

    #endregion

    #region Create
    public async Task<Profesionale?> Create(Profesionale profesionalNuevo)
    {
        var validPassword = PH.verifyPassword(profesionalNuevo.IdUsuariosNavigation.Pass);
        if (validPassword is false)
        {
            throw new Exception("La contaseña debe contener un numero y una mayuscula");
        }
        else
        {
            profesionalNuevo.IdUsuariosNavigation.Pass = PH.hashPassword(profesionalNuevo.IdUsuariosNavigation.Pass);
            profesionalNuevo.IdUsuariosNavigation.Nombre = NN.ConvertirNombre(profesionalNuevo.IdUsuariosNavigation.Nombre);
            profesionalNuevo.IdUsuariosNavigation.Apellido = NN.ConvertirNombre(profesionalNuevo.IdUsuariosNavigation.Apellido);
            profesionalNuevo.EstadoSub = false;
            profesionalNuevo.IdUsuariosNavigation.TipoCuenta = "P";
            profesionalNuevo.IdUsuariosNavigation.CreacionCuenta = DateTime.Today;
            _context.Profesionales.Add(profesionalNuevo);

            await _context.SaveChangesAsync();

            return profesionalNuevo;
        }
    }
    #endregion

    #region Update
    public async Task Update(string username, ProfesionalDtoUpdate profesionalDto)
    {
        var profesionalExistente = await GetByUsernameToFunction(username);


        if (profesionalExistente is not null)
        {
            profesionalExistente.Descripcion = profesionalDto.Descripcion;
            profesionalExistente.HorarioInicio = profesionalDto.HorarioInicio;
            profesionalExistente.HorarioFinal = profesionalDto.HorarioFinal;
            profesionalExistente.FotoBanner = profesionalDto.FotoBanner;
            profesionalExistente.Direccion = profesionalDto.Direccion;
            profesionalExistente.Profesion = profesionalDto.Profesion;

            await _context.SaveChangesAsync();
        }
    }

    public async Task UpdateStatus(string username)
    {
        var profesionalToGet = await GetByUsernameToFunction(username);


        if (profesionalToGet is not null)
        {
            var id = profesionalToGet.IdUsuarios;
            profesionalToGet.EstadoSub = true;

            await _context.SaveChangesAsync();
        }
    }

    #endregion

    #region Delete
    public async Task Delete(string username)
    {
        var serviciosDelete = await GetServiceToDelete(username);
        var profesionalToDelete = await GetByUsernameToFunction(username);
        var usuarioDelete = await GetUsuarioToDelete(username);

        if (profesionalToDelete is not null && usuarioDelete is not null)
        {
            _context.Servicios.RemoveRange(serviciosDelete);
            _context.Profesionales.Remove(profesionalToDelete);
            _context.Usuarios.Remove(usuarioDelete);

            await _context.SaveChangesAsync();
        }
    }

    public async Task<Usuario?> GetUsuarioToDelete(string username)
    {
        return await _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
    }
    public async Task<IEnumerable<Servicio>> GetServiceToDelete(string username)
    {
        return await _context.Servicios.Where(t => t.IdProfesionalNavigation != null &&
        t.IdProfesionalNavigation.IdUsuariosNavigation.Username == username).ToListAsync();

    }

    #endregion

    #region Generar Horarios
#nullable disable

    public async Task<IDictionary<DateTime, IEnumerable<string>>> GetHorariosDisponibles(string username, int id, DateTime fecha)
    {
        var profesional = await GetByUsername(username);
        var servicio = await GetServicio(id);
        var turnos = await GetTurnos();
        TimeSpan? horaInicio = profesional.HorarioInicio;
        TimeSpan? horaFinal = profesional.HorarioFinal;
        string servicioIntervalo = servicio?.Duracion.ToString() ?? string.Empty;

        IDictionary<DateTime, IEnumerable<string>> horarios = new Dictionary<DateTime, IEnumerable<string>>();

        // Generar una lista completa de horarios disponibles
        var horariosDisponibles = GenerarHorariosDisponibles(horaInicio.Value, horaFinal.Value, servicioIntervalo);

        // Eliminar los horarios ocupados según los turnos existentes
        foreach (var turno in turnos)
        {
            var fechaTurno = turno.FechaTurno.Date;
            var horaTurno = turno.HoraTurno;

            // Verificar si el turno es para la fecha y el servicio específicos
            if (fechaTurno == fecha && horaTurno.HasValue)
            {
                var duracionTurno = turno.Duracion ?? TimeSpan.Zero;

                // Calcular el rango de horarios ocupados por el turno
                var horaInicioTurno = horaTurno.Value;
                var horaFinalTurno = horaInicioTurno.Add(duracionTurno);

                // Eliminar los horarios ocupados de la lista de horarios disponibles
                horariosDisponibles.RemoveAll(h => h >= horaInicioTurno && h < horaFinalTurno);

                // Verificar si el turno es en el día actual y el horario es anterior al horario actual
                if (fechaTurno == DateTime.Today && horaInicioTurno < DateTime.Now.TimeOfDay)
                {
                    horariosDisponibles.RemoveAll(h => h < DateTime.Now.TimeOfDay);
                }
            }
        }

        // Agregar los horarios disponibles restantes a la lista final de horarios
        foreach (var horarioDisponible in horariosDisponibles)
        {
            var fechaHoraActual = fecha.Add(horarioDisponible);

            if (!horarios.ContainsKey(fechaHoraActual.Date))
            {
                horarios[fechaHoraActual.Date] = new List<string>();
            }

            ((List<string>)horarios[fechaHoraActual.Date]).Add(horarioDisponible.ToString(@"hh\:mm")); // Utilizar "HH" para formato de 24 horas
        }

        return horarios;
    }
    private List<TimeSpan> GenerarHorariosDisponibles(TimeSpan horaInicio, TimeSpan horaFinal, string servicioIntervalo)
    {
        var horarios = new List<TimeSpan>();
        var intervalo = TimeSpan.ParseExact(servicioIntervalo, @"hh\:mm\:ss", CultureInfo.InvariantCulture);

        while (horaInicio <= horaFinal)
        {
            horarios.Add(horaInicio);
            horaInicio = horaInicio.Add(intervalo);
        }

        return horarios;
    }
#nullable restore


    public async Task<Servicio?> GetServicio(int id)
    {
        return await _context.Servicios.FindAsync(id);
    }

    public async Task<IEnumerable<ListaTurnosDTO?>> GetTurnos()
    {
        return await _context.Turnos.Select(t => new ListaTurnosDTO
        {
            HoraTurno = t.HoraTurno,
            FechaTurno = t.FechaTurno,
            Duracion = t.IdServiciosNavigation.Duracion
        }).ToListAsync();
    }


    #endregion
}

