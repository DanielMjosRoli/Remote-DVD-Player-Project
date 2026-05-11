using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("rating")]
public class RatingsController : ControllerBase
{
    private readonly MediaDBContext _context;

    public RatingsController(MediaDBContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrUpdateRating(CreateRatingDto dto)
    {
        if (dto.RatingValue < 1 || dto.RatingValue > 5)
            return BadRequest("Rating must be between 1 and 5");

        var existing = await _context.Ratings
            .FirstOrDefaultAsync(r =>
                r.ProfileId == dto.ProfileId &&
                r.MovieId == dto.MovieId);

        if (existing != null)
        {
            // Update existing rating
            existing.RatingValue = dto.RatingValue;
            existing.RatedAt = DateTime.UtcNow;
        }
        else
        {
            // Create new rating
            var rating = new Rating
            {
                ProfileId = dto.ProfileId,
                MovieId = dto.MovieId,
                RatingValue = dto.RatingValue,
                RatedAt = DateTime.UtcNow
            };

            _context.Ratings.Add(rating);
        }

        await _context.SaveChangesAsync();
        return Ok(dto);
    }
}