using Microsoft.AspNetCore.Mvc;
using SATURNO_V2.Services;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Functions;
using SATURNO_V2.Data.DTOs.ProfesionalDTO;

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

    [HttpGet("cuted/{n}")]
    public async Task<IEnumerable<ProfesionalDto>> GetFour(int n)
    {
        return await _service.GetFour(n);
    }

    [HttpGet("horarios/{username}/{fecha}")]
    public async Task<IDictionary<DateTime, IEnumerable<string>>> GetInicioCierre(string username, int id, DateTime fecha)
    {
        var profesional = await _service.GetByUsername(username);
        var servicio = await _service.GetServicio(id);
        var turnos = await _service.GetTurnos();
        TimeSpan? horaInicio = profesional.HorarioInicio;
        TimeSpan? horaFinal = profesional.HorarioFinal;
        var servicioIntervalo = servicio.Duracion;

        IDictionary<DateTime, IEnumerable<string>> horarios = new Dictionary<DateTime, IEnumerable<string>>();

        while (horaInicio <= horaFinal)
        {
            var fechaHoraActual = fecha.Add(horaInicio.Value);

            // Verificar si existe algún turno para la fecha actual y la hora actual
            var turnoExistente = turnos.FirstOrDefault(t =>
                t.FechaTurno.Date == fechaHoraActual.Date &&
                t.HoraTurno == fechaHoraActual.TimeOfDay);

            // Agregar el horario solo si no hay un turno existente para la fecha actual
            if (turnoExistente == null)
            {
                if (!horarios.ContainsKey(fechaHoraActual.Date))
                {
                    horarios[fechaHoraActual.Date] = new List<string>();
                }

                ((List<string>)horarios[fechaHoraActual.Date]).Add(fechaHoraActual.ToString("HH\\:mm")); // Utilizar "HH" para formato de 24 horas
            }

            horaInicio = horaInicio.Value.Add(TimeSpan.Parse(servicioIntervalo.ToString()));
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
