using Microsoft.EntityFrameworkCore;
using SATURNO_V2.Data;
using SATURNO_V2.Data.SaturnoModels;

namespace SATURNO_V2.Services;

public class PagoService
{

    private readonly SaturnoV2Context _context;

    public PagoService(SaturnoV2Context context)
    {
        _context = context;
    }

    public async Task<Profesionale?> GetByIdToFunction(int id)
    {
        return await _context.Profesionales.FindAsync(id);
    }

    public async Task<Profesionale?> Create(Profesionale profesionalNuevo)
    {
        _context.Profesionales.Add(profesionalNuevo);

        await _context.SaveChangesAsync();

        return profesionalNuevo;
    }

}