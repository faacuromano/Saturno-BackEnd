using SATURNO_V2.Data;

namespace SATURNO_V2.Services;

public class TurnoService{

    private readonly SaturnoV2Context _context;

    public TurnoService(SaturnoV2Context context)
    {
        _context = context;
    }

}