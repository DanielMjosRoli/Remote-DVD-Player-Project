using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("storage-volumes")]
public class StorageVolumesController : ControllerBase
{
    private readonly MediaDBContext _context;

    public StorageVolumesController(MediaDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<StorageVolume>>> GetAll()
    {
        return Ok(await _context.StorageVolumes.AsNoTracking().ToListAsync());
    }
}