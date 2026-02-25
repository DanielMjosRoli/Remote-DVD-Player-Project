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
        int page = 1,
        int pageSize = 20)
    {
        pageSize = Math.Min(pageSize, 100);

        var query = _context.Collections.AsNoTracking();
        var total = await query.CountAsync();

        var items = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new PagedResult<Collection>(items, total, page, pageSize));
    }
}