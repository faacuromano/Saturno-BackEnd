using Microsoft.EntityFrameworkCore;
using SATURNO_V2.Data;
using SATURNO_V2.Data.SaturnoModels;

namespace SATURNO_V2.Services;

public class ServicioService{

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
            Duracion = t.Duracion
        }).ToListAsync();
        
    }
    public async Task<IEnumerable<Servicio>> GetFour(int n)
    {
        var professionalsToCut = await _context.Servicios.Select(t => new Servicio
        {
            Nombre = t.Nombre,
            Descripcion = t.Descripcion,
            Precio = t.Precio,
            Duracion = t.Duracion
        }).ToListAsync();

        return professionalsToCut.Take(n).ToArray();
    }
    public async Task<Servicio?> GetByIdToFunction(int id)
    {
         return  await _context.Servicios.FindAsync(id);
    }
    public async Task<Servicio?> GetById(int id)
    {
    return await _context.Servicios
        .Where(p => p.Id == id)
        .FirstOrDefaultAsync();
    }
    public async Task<Servicio?> Create(Servicio servicioNuevo)
    {
        _context.Servicios.Add(servicioNuevo);

        await _context.SaveChangesAsync();
        
        return servicioNuevo;
    }

    public async Task Update(int id, Servicio servicioDto) 
    {
        var profesionalExistente = await GetByIdToFunction(id);
        
        if (profesionalExistente is not null)
            {
            profesionalExistente.Descripcion = servicioDto.Descripcion;
            profesionalExistente.Nombre = servicioDto.Nombre;
            profesionalExistente.Precio = servicioDto.Precio;
            profesionalExistente.Duracion = servicioDto.Duracion;
            
             await _context.SaveChangesAsync();
        }
    }    
    public async Task Delete(int id)
    {
         var ususarioDelete = await GetByIdToFunction(id);

         if(ususarioDelete is not null)
         {
             _context.Servicios.Remove(ususarioDelete);
             await _context.SaveChangesAsync();
         }
     }

}