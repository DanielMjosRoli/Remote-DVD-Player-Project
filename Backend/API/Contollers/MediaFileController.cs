using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("media-files")]
public class MediaFilesController : ControllerBase
{
    private readonly MediaDBContext _context;

    public MediaFilesController(MediaDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<MediaFile>>> GetAll(
        int page = 1,
        int pageSize = 20)
    {
        pageSize = Math.Min(pageSize, 100);

        var query = _context.MediaFiles
            .Include(m => m.Movie)
            .AsNoTracking();

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new PagedResult<MediaFile>(items, totalCount, page, pageSize));
    }
}