using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("users/{userId:guid}/ratings")]
public class RatingsController : ControllerBase
{
    private readonly MediaDBContext _context;

    public RatingsController(MediaDBContext context)
    {
        _context = context;
    }

    [HttpGet]
public async Task<ActionResult<UserRatingDTO>> GetForUser(
    Guid userId,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 20)
{
    pageSize = Math.Min(pageSize, 100);

    var user = await _context.Users
        .AsNoTracking()
        .Where(u => u.Id == userId)
        .Select(u => new
        {
            u.Id,
            u.Username
        })
        .FirstOrDefaultAsync();

    if (user == null)
        return NotFound();

    var ratingsQuery = _context.Ratings
        .AsNoTracking()
        .Where(r => r.UserId == userId);

    var totalCount = await ratingsQuery.CountAsync();

    var ratings = await ratingsQuery
        .OrderByDescending(r => r.RatedAt)
        .Skip((page - 1) * pageSize)
        .Take(pageSize)
        .Select(r => new RatingDTO(
            r.UserId,
            r.MovieId,
            new MovieDTO(
                r.Movie.Id,
                r.Movie.Title,
                r.Movie.OriginalTitle,
                r.Movie.Description,
                r.Movie.ReleaseYear,
                r.Movie.DurationMinutes,
                r.Movie.AgeRating,
                r.Movie.PosterPath,
                r.Movie.UpdatedAt,
                r.Movie.Genres
                    .Select(mg => new GenreDTO(
                        mg.GenreId,
                        mg.Genre.Name
                    ))
                    .ToList()
            ),
            r.RatingValue,
            r.RatedAt
        ))
        .ToListAsync();

    var result = new UserRatingDTO(
        user.Id,
        user.Username,
        new PagedResult<RatingDTO>(
            ratings,
            totalCount,
            page,
            pageSize
        )
    );

    return Ok(result);
}
}