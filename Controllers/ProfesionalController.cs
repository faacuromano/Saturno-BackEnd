using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Functions;
using SATURNO_V2.Data.DTOs.ProfesionalDTO;
using System.Globalization;

namespace SATURNO_V2.Controllers;

[ApiController]
[Route("profesional")]

public class ProfesionalController : ControllerBase
{
    private readonly ProfesionalService _service;
    public ProfesionalController(ProfesionalService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IEnumerable<ProfesionalDto>> Get()
    {
        return await _service.GetAll();
    }

    [HttpGet("horarios/{username}/{fecha}")]
    public async Task<IDictionary<DateTime, IEnumerable<string>>> GetInicioCierre(string username, int id, DateTime fecha)
    {
        var profesional = await _service.GetByUsername(username);
        var servicio = await _service.GetServicio(id);
        var turnos = await _service.GetTurnos();
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

    private IEnumerable<TimeSpan> ObtenerHorariosDisponibles(TimeSpan horaInicio, TimeSpan horaFinal, string servicioIntervalo)
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

    [HttpGet("{username}")]
    public async Task<ActionResult<ProfesionalDto>> GetByUsername(string username)
    {
        var profesional = await _service.GetByUsername(username);

        if (profesional is not null)
        {
            return profesional;
        }
        else
        {
            return NotFound();
        }

    }

    [HttpPost]
    public async Task<IActionResult> Create(Profesionale profesional)
    {
        bool verficacion = VF.validateProfessional(profesional);

        if (verficacion is true)
        {
            var profesionalNuevo = await _service.Create(profesional);

            if (profesionalNuevo is not null)
            {
                return CreatedAtAction(nameof(GetByUsername), new { username = profesionalNuevo.IdUsuariosNavigation.Username }, profesionalNuevo);
            }
            else
            {
                return BadRequest("El objeto profesional se recibio como null.");
            }
        }
        else
        {
            return BadRequest("Uno o mas campos invalidos ingresados en la request.");
        }

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ProfesionalDtoUpdate profesionalDtoIn)
    {
        if (id != profesionalDtoIn.IdUsuarios)
        {
            return BadRequest();
        }

        await _service.Update(id, profesionalDtoIn);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var profesionalDelete = await _service.GetByIdToFunction(id);

        if (profesionalDelete is not null)
        {
            await _service.Delete(id);
            return Ok();
        }
        else
        {
            return NotFound();
        }
    }

}
