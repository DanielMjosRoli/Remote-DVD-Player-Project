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
    public async Task<ActionResult<IEnumerable<GenreDTO>>> GetAll()
    {
        var query = _context.Genres.AsNoTracking();
        var items = await query
            .OrderBy(g => g.Name)
            .Select( g => new GenreDTO(
                g.Id,
                g.Name
            ))
            .ToListAsync();
        return Ok(items);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<GenreViewDTO>> Get(Guid id)
    {
        var genre = await _context.Genres
            .Where(g => g.Id == id)
            .Select(g => new GenreViewDTO(
                g.Id,
                g.Name,
                g.Movies
                    .Select(gm => new MovieDTO(
                        gm.Movie.Id,
                        gm.Movie.Title,
                        gm.Movie.OriginalTitle,
                        gm.Movie.Description,
                        gm.Movie.ReleaseYear,
                        gm.Movie.DurationMinutes,
                        gm.Movie.AgeRating,
                        gm.Movie.PosterPath,
                        gm.Movie.UpdatedAt,
                        gm.Movie.Genres
                        .Select(mg => new GenreDTO(
                            mg.GenreId,
                            mg.Genre.Name
                        ))
                        .ToList()
                    )).ToList()
            )).FirstOrDefaultAsync();
        if (genre is null)
        {
            return NotFound();
        }
        return Ok(genre);
    }
}