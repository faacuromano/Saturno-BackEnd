using Microsoft.EntityFrameworkCore;
using SATURNO_V2.Data;
using SATURNO_V2.Data.SaturnoModels;
using SATURNO_V2.Data.DTOs;
using System.Security.Cryptography;
using System.Text;

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
            Ubicacion = t.IdUsuariosNavigation.Ubicacion,
            Descripcion = t.Descripcion,
            HorarioInicio = t.HorarioInicio,
            HorarioFinal = t.HorarioFinal,
            Direccion = t.Direccion,
            IdUsuarios = t.IdUsuarios,
            FotoBanner = t.FotoBanner,

        }).ToListAsync();

    }
    public async Task<IEnumerable<ProfesionalDto>> GetFour(int n)
    {
        var professionalsToCut = await _context.Profesionales.Select(t => new ProfesionalDto
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
            Ubicacion = t.IdUsuariosNavigation.Ubicacion,
            Descripcion = t.Descripcion,
            HorarioInicio = t.HorarioInicio,
            HorarioFinal = t.HorarioFinal,
            Direccion = t.Direccion,
            IdUsuarios = t.IdUsuarios,
            FotoBanner = t.FotoBanner,

        }).ToListAsync();

        return professionalsToCut.Take(n).ToArray();
    }
    public async Task<Profesionale?> GetByIdToFunction(int id)
    {
        return await _context.Profesionales.FindAsync(id);
    }

    public async Task<ProfesionalDto?> GetById(int id)
    {
        return await _context.Profesionales
            .Where(p => p.IdUsuariosNavigation.Id == id)
            .Select(t => new ProfesionalDto
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
                Descripcion = t.Descripcion,
                HorarioInicio = t.HorarioInicio,
                HorarioFinal = t.HorarioFinal,
                FotoBanner = t.FotoBanner,
                Direccion = t.Direccion,
                IdUsuarios = t.IdUsuarios
            })
            .FirstOrDefaultAsync();
    }

    public async Task<Profesionale?> Create(Profesionale profesionalNuevo)
    {
        profesionalNuevo.IdUsuariosNavigation.Pass = hashPassword(profesionalNuevo.IdUsuariosNavigation.Pass);
        _context.Profesionales.Add(profesionalNuevo);

        await _context.SaveChangesAsync();

        return profesionalNuevo;
    }

    public async Task Update(int id, ProfesionalDtoIn profesionalDto)
    {
        var profesionalExistente = await GetByIdToFunction(id);

        if (profesionalExistente is not null)
        {
            profesionalExistente.Descripcion = profesionalDto.Descripcion;
            profesionalExistente.HorarioInicio = profesionalDto.HorarioInicio;
            profesionalExistente.HorarioFinal = profesionalDto.HorarioFinal;
            profesionalExistente.FotoBanner = profesionalDto.FotoBanner;
            profesionalExistente.Direccion = profesionalDto.Direccion;
            profesionalExistente.IdUsuarios = profesionalDto.IdUsuarios;

            await _context.SaveChangesAsync();
        }
    }
    public async Task Delete(int id)
    {
        var profesionalDelete = await GetByIdToFunction(id);

        if (profesionalDelete is not null)
        {
            _context.Profesionales.Remove(profesionalDelete);
            await _context.SaveChangesAsync();
        }
    }

    string hashPassword(string password)
    {
        var sha = SHA256.Create();

        var asByteArray = Encoding.Default.GetBytes(password);
        var hashedPassword = sha.ComputeHash(asByteArray);

        return Convert.ToBase64String(hashedPassword);
    }

}