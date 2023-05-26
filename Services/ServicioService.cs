using Microsoft.EntityFrameworkCore;
using SATURNO_V2.Data;
using SATURNO_V2.Data.SaturnoModels;

namespace SATURNO_V2.Services;

public class ServicioService
{

    private readonly SaturnoV2Context _context;

    public ServicioService(SaturnoV2Context context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Servicio>> GetAll()
    {
        return await _context.Servicios.Select(t => new Servicio
        {
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

    public async Task<IEnumerable<Servicio>> GetByProfesional(string username)
    {
        return await _context.Servicios.Where(a => a.IdProfesionalNavigation.IdUsuariosNavigation.Username == username).Select(a => new Servicio
        {
            IdProfesional = a.IdProfesional,
            Nombre = a.Nombre,
            Precio = a.Precio,
            Duracion = a.Duracion,
            Descripcion = a.Descripcion,
        }).ToListAsync();
    }

    public async Task<Servicio?> Create(Servicio servicioNuevo)
    {
        _context.Servicios.Add(servicioNuevo);

        await _context.SaveChangesAsync();

        return servicioNuevo;
    }

    public async Task Update(int id, Servicio servicioDto)
    {
        var servicioExistente = await GetByIdToFunction(id);

        if (servicioExistente is not null)
        {
            servicioExistente.Descripcion = servicioDto.Descripcion;
            servicioExistente.Nombre = servicioDto.Nombre;
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