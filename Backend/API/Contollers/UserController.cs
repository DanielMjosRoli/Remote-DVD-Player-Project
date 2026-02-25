using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("users")]
public class UsersController : ControllerBase
{
    private readonly MediaDBContext _context;

    public UsersController(MediaDBContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<PagedResult<User>>> GetAll(
        int page = 1,
        int pageSize = 20)
    {
        pageSize = Math.Min(pageSize, 100);

        var query = _context.Users.AsNoTracking();

        var totalCount = await query.CountAsync();

        var users = await query
            .OrderBy(u => u.Username)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return Ok(new PagedResult<User>(users, totalCount, page, pageSize));
    }
}