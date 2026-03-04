using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

[ApiController]
[Route("movies")]
public class MoviesController : ControllerBase
{
    private readonly MediaDBContext _context;

    public MoviesController(MediaDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<MovieDTO>>> GetAll(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        pageSize = Math.Min(pageSize, 100);

        var query = _context.Movies.AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(m => m.Title)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .Select(m => new MovieDTO(
                m.Id,
                m.Title,
                m.OriginalTitle,
                m.Description,
                m.ReleaseYear,
                m.DurationMinutes,
                m.AgeRating,
                m.PosterPath,
                m.UpdatedAt,
                m.Genres
                    .Select(mg => new GenreDTO(
                        mg.GenreId,
                        mg.Genre.Name
                    ))
                    .ToList()
            ))
            .ToListAsync();

        return Ok(new PagedResult<MovieDTO>(items, totalCount, page, pageSize));
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<MoviePageDTO>> Get(Guid id)
    {
        var movie = await _context.Movies
            .Where(m => m.Id == id)
            .Select(m => new MoviePageDTO(
                m.Id,
                m.Title,
                m.OriginalTitle,
                m.Description,
                m.ReleaseYear,
                m.DurationMinutes,
                m.AgeRating,
                m.PosterPath,
                m.UpdatedAt,
                m.MediaFiles
                    .Select(mf => new MediaFilesDTO(
                        mf.Id,
                        mf.StorageVolumeId,
                        mf.FilePath,
                        mf.FileSizeBytes,
                        mf.Checksum,
                        mf.ContainerFormat,
                        mf.Resolution,
                        mf.AudioFormat,
                        mf.SubtitleLanguages,
                        mf.CreatedAt
                    )).ToList(),
                m.Genres
                    .Select(mg => new GenreDTO(
                        mg.GenreId,
                        mg.Genre.Name
                    ))
                    .ToList()
            ))
            .FirstOrDefaultAsync();

        if (movie is null)
            return NotFound();

        return Ok(movie);
    }

    [HttpPost]
    public async Task<ActionResult<MovieDTO>> Create(MovieDTO moviedto)
    {
        var movie = new Movie
        (
            moviedto.Title,
            moviedto.OriginalTitle,
            moviedto.Description,
            moviedto.ReleaseYear,
            moviedto.DurationMinutes,
            moviedto.AgeRating,
            moviedto.PosterPath
        );
        _context.Movies.Add(movie);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(Get), new { id = movie.Id }, movie);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<MovieDTO>> Delete(Guid id)
    {
        var movie = await _context.Movies.FindAsync(id);
        if (movie is null)
            return NotFound();

        _context.Movies.Remove(movie);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    [HttpGet("{movieId:guid}/rating-summary")]
    public async Task<IActionResult> GetRatingSummary(Guid movieId)
    {
        var exists = await _context.Movies
            .AnyAsync(m => m.Id == movieId);

        if (!exists)
            return NotFound();

        var ratingData = await _context.Ratings
            .Where(r => r.MovieId == movieId)
            .GroupBy(r => r.MovieId)
            .Select(g => new
            {
                AverageRating = g.Average(r => r.RatingValue),
                RatingCount = g.Count()
            })
            .FirstOrDefaultAsync();

        if (ratingData == null)
        {
            return Ok(new
            {
                AverageRating = 0.0,
                RatingCount = 0
            });
        }

        return Ok(ratingData);
    }
}