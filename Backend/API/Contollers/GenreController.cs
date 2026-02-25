using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("genres")]
public class GenresController : ControllerBase
{
    private readonly MediaDBContext _context;

    public GenresController(MediaDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Genre>>> GetAll()
    {
        return Ok(await _context.Genres.AsNoTracking().ToListAsync());
    }
}