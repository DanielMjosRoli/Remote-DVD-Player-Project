using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/movie")]
public class MoviesController : ControllerBase
{
    private readonly MediaDBContext _context;

    public MoviesController(MediaDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var movies = await _context.Movies
            .AsNoTracking()
            .ToListAsync();

        return Ok(movies);
    }
}