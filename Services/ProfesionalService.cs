using Microsoft.EntityFrameworkCore;
using SATURNO_V2.Data;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Functions;
using SATURNO_V2.Data.DTOs.ProfesionalDTO;

namespace SATURNO_V2.Services;

public class ProfesionalService
{

    private readonly SaturnoV2Context _context;

    public ProfesionalService(SaturnoV2Context context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ProfesionalDto>> GetAll()
    {
        return await _context.Profesionales.Select(t => new ProfesionalDto
        {
            IdUsuarios = t.IdUsuariosNavigation.Id,
            Nombre = t.IdUsuariosNavigation.Nombre,
            Apellido = t.IdUsuariosNavigation.Apellido,
            Username = t.IdUsuariosNavigation.Username,
            Mail = t.IdUsuariosNavigation.Mail,
            Pass = t.IdUsuariosNavigation.Pass,
            NumTelefono = t.IdUsuariosNavigation.NumTelefono,
            FechaNacimiento = FP.FechaParse(t.IdUsuariosNavigation.FechaNacimiento),
            FotoPerfil = t.IdUsuariosNavigation.FotoPerfil,
            Verificado = t.IdUsuariosNavigation.Verificado,
            CreacionCuenta = FP.FechaParse(t.IdUsuariosNavigation.CreacionCuenta),
            TipoCuenta = t.IdUsuariosNavigation.TipoCuenta,
            Ubicacion = t.IdUsuariosNavigation.Ubicacion,
            Descripcion = t.Descripcion,
            Profesion = NN.ConvertirNombre(t.Profesion!),
            HorarioInicio = t.HorarioInicio,
            HorarioFinal = t.HorarioFinal,
            Direccion = t.Direccion,
            FotoBanner = t.FotoBanner,

        }).ToListAsync();

    }
    public async Task<IEnumerable<ProfesionalDto>> GetFour(int n)
    {
        var professionalsToCut = await _context.Profesionales.Select(t => new ProfesionalDto
        {
            IdUsuarios = t.IdUsuariosNavigation.Id,
            Nombre = t.IdUsuariosNavigation.Nombre,
            Apellido = t.IdUsuariosNavigation.Apellido,
            Username = t.IdUsuariosNavigation.Username,
            Mail = t.IdUsuariosNavigation.Mail,
            Pass = t.IdUsuariosNavigation.Pass,
            NumTelefono = t.IdUsuariosNavigation.NumTelefono,
            FechaNacimiento = FP.FechaParse(t.IdUsuariosNavigation.FechaNacimiento),
            FotoPerfil = t.IdUsuariosNavigation.FotoPerfil,
            Verificado = t.IdUsuariosNavigation.Verificado,
            CreacionCuenta = FP.FechaParse(t.IdUsuariosNavigation.CreacionCuenta),
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
    public async Task<Profesionale?> GetByIdToFunction(int id)
    {
        return await _context.Profesionales.FindAsync(id);
    }
    public async Task<Usuario?> GetUsuarioToDelete(int id)
    {
        return await _context.Usuarios.FindAsync(id);
    }
    public async Task<IEnumerable<Servicio>> GetServiceToDelete(int id)
    {
        return await _context.Servicios.Where(t => t.IdProfesionalNavigation != null &&
        t.IdProfesionalNavigation.IdUsuariosNavigation.Id == id).ToListAsync();

    }
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
                Pass = t.IdUsuariosNavigation.Pass,
                NumTelefono = t.IdUsuariosNavigation.NumTelefono,
                FechaNacimiento = FP.FechaParse(t.IdUsuariosNavigation.FechaNacimiento),
                FotoPerfil = t.IdUsuariosNavigation.FotoPerfil,
                Verificado = t.IdUsuariosNavigation.Verificado,
                CreacionCuenta = FP.FechaParse(t.IdUsuariosNavigation.CreacionCuenta),
                TipoCuenta = t.IdUsuariosNavigation.TipoCuenta,
                Descripcion = t.Descripcion,
                Profesion = t.Profesion,
                HorarioInicio = t.HorarioInicio,
                HorarioFinal = t.HorarioFinal,
                FotoBanner = t.FotoBanner,
                Direccion = t.Direccion,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Profesionale?> Create(Profesionale profesionalNuevo)
    {
        profesionalNuevo.IdUsuariosNavigation.Pass = PH.hashPassword(profesionalNuevo.IdUsuariosNavigation.Pass);
        profesionalNuevo.IdUsuariosNavigation.Nombre = NN.ConvertirNombre(profesionalNuevo.IdUsuariosNavigation.Nombre);
        profesionalNuevo.IdUsuariosNavigation.Apellido = NN.ConvertirNombre(profesionalNuevo.IdUsuariosNavigation.Apellido);
        profesionalNuevo.IdUsuariosNavigation.TipoCuenta = "P";
        profesionalNuevo.IdUsuariosNavigation.CreacionCuenta = DateTime.Today;
        _context.Profesionales.Add(profesionalNuevo);

        await _context.SaveChangesAsync();

        return profesionalNuevo;
    }

    public async Task Update(int id, ProfesionalDtoUpdate profesionalDto)
    {
        var profesionalExistente = await GetByIdToFunction(id);

        if (profesionalExistente is not null)
        {
            profesionalExistente.Descripcion = profesionalDto.Descripcion;
            profesionalExistente.HorarioInicio = profesionalDto.HorarioInicio;
            profesionalExistente.HorarioFinal = profesionalDto.HorarioFinal;
            profesionalExistente.FotoBanner = profesionalDto.FotoBanner;
            profesionalExistente.Direccion = profesionalDto.Direccion;
            profesionalExistente.Profesion = profesionalDto.Profesion;
            profesionalExistente.IdUsuarios = profesionalDto.IdUsuarios;

            await _context.SaveChangesAsync();
        }
    }
    public async Task Delete(int id)
    {
        var serviciosDelete = await GetServiceToDelete(id);
        var profesionalToDelete = await GetByIdToFunction(id);
        var usuarioDelete = await GetUsuarioToDelete(id);

        if (profesionalToDelete is not null && usuarioDelete is not null)
        {
            _context.Servicios.RemoveRange(serviciosDelete);
            _context.Profesionales.Remove(profesionalToDelete);
            _context.Usuarios.Remove(usuarioDelete);

            await _context.SaveChangesAsync();
        }
    }

}