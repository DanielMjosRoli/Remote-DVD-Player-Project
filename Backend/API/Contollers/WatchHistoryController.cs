using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("users/{userId:guid}/watch-history")]
public class WatchHistoryController : ControllerBase
{
    private readonly MediaDBContext _context;

    public WatchHistoryController(MediaDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetForUser(Guid userId)
    {
        var history = await _context.WatchHistories
            .Where(w => w.UserId == userId)
            .Include(w => w.Movie)
            .ToListAsync();

        return Ok(history);
    }
}