using Microsoft.EntityFrameworkCore;
using SATURNO_V2.Data;
using SATURNO_V2.Data.DTOs.ServicioDTO;
using SATURNO_V2.Data.SaturnoModels;

namespace SATURNO_V2.Services;

public class ServicioService
{

    private readonly SaturnoV2Context _context;

    public ServicioService(SaturnoV2Context context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ServicioDTO_getAll>> GetAll()
    {
        return await _context.Servicios.Select(t => new ServicioDTO_getAll
        {
            Id = t.Id,
            Nombre = t.Nombre,
            Descripcion = t.Descripcion,
            Precio = t.Precio,
            Duracion = t.Duracion,
            IdProfesional = t.IdProfesional
        }).ToListAsync();

    }

    public async Task<Servicio?> GetByIdToFunction(int id)
    {
        return await _context.Servicios.FindAsync(id);
    }

    public async Task<Servicio?> GetById(int id)
    {
        return await _context.Servicios
            .Where(p => p.Id == id)
            .FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<ServicioDto>> GetByProfesional(string username)
    {
        return await _context.Servicios.Where(a => a.IdProfesionalNavigation != null
        && a.IdProfesionalNavigation.IdUsuariosNavigation.Username == username).Select(a => new ServicioDto
        {
            Id = a.Id,
            Nombre = a.Nombre,
            Precio = a.Precio,
            Duracion = a.Duracion,
            Descripcion = a.Descripcion,
        }).ToListAsync();
    }

    public async Task<object?> Create(Servicio servicioNuevo)
    {
        if (servicioNuevo.Precio > 0)
        {
            _context.Servicios.Add(servicioNuevo);
            await _context.SaveChangesAsync();
            return servicioNuevo;
        }
        else
        {
            return new Exception("EL PRECIO DEBE SER MAYOR A CERO").Message;
        }
    }

    public async Task Update(int id, Servicio servicioDto)
    {
        var servicioExistente = await GetByIdToFunction(id);

        if (servicioExistente is not null)
        {
            servicioExistente.Nombre = servicioDto.Nombre;
            servicioExistente.Descripcion = servicioDto.Descripcion;
            servicioExistente.Precio = servicioDto.Precio;
            servicioExistente.Duracion = servicioDto.Duracion;

            await _context.SaveChangesAsync();
        }
    }

    public async Task Delete(int id)
    {
        var ususarioDelete = await GetByIdToFunction(id);

        if (ususarioDelete is not null)
        {
            _context.Servicios.Remove(ususarioDelete);
            await _context.SaveChangesAsync();
        }
    }



}