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
    public async Task<IActionResult> GetForUser(Guid userId)
    {
        var ratings = await _context.Ratings
            .Where(r => r.UserId == userId)
            .Include(r => r.Movie)
            .ToListAsync();

        return Ok(ratings);
    }
}