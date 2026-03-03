using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("collections")]
public class CollectionsController : ControllerBase
{
    private readonly MediaDBContext _context;

    public CollectionsController(MediaDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<Collection>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        pageSize = Math.Min(pageSize, 100);

        var query = _context.Collections.AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(c => c.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(c => new CollectionDTO(
                c.Id,
                c.Name,
                c.Description,
                c.Movies
                    .Select(cm => new MovieDTO(
                        cm.Movie.Id,
                        cm.Movie.Title,
                        cm.Movie.OriginalTitle,
                        cm.Movie.Description,
                        cm.Movie.ReleaseYear,
                        cm.Movie.DurationMinutes,
                        cm.Movie.AgeRating,
                        cm.Movie.PosterPath,
                        cm.Movie.UpdatedAt,
                        cm.Movie.Genres
                            .Select(mg => new GenreDTO(
                                mg.GenreId,
                                mg.Genre.Name
                            ))
                            .ToList()
                    )).ToList()
            ))
            .ToListAsync();

        return Ok(new PagedResult<CollectionDTO>(items, totalCount, page, pageSize));
    }
}