using Microsoft.EntityFrameworkCore;
using SATURNO_V2.Data;
using SATURNO_V2.Data.SaturnoModels;

namespace SATURNO_V2.Services;

public class ClienteService{

    private readonly SaturnoV2Context _context;

    public ClienteService(SaturnoV2Context context)
    {
        _context = context;
    }
}