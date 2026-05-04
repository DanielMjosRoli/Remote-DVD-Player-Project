using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("/profile")]
public class ProfilesController : ControllerBase
{
    private readonly MediaDBContext _context;

    public ProfilesController(MediaDBContext context)
    {
        _context = context;
    }

    // GET: api/profiles?userId=...
    [HttpGet]
    public async Task<IActionResult> GetProfiles([FromQuery] Guid userId)
    {
        var profiles = await _context.Profiles
            .Where(p => p.UserId == userId)
            .ToListAsync();

        return Ok(profiles);
    }

    [HttpPost]
public async Task<IActionResult> CreateProfile([FromBody] CreateProfileDto dto)
{
    var profile = new Profile
    {
        Name = dto.Name,
        Avatar = dto.Avatar,
        IsKids = dto.IsKids,
        UserId = dto.UserId
    };

    _context.Profiles.Add(profile);
    await _context.SaveChangesAsync();

    return Ok(profile);
}

    // DELETE: api/profiles/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProfile(Guid id)
    {
        var profile = await _context.Profiles.FindAsync(id);
        if (profile == null)
            return NotFound();

        _context.Profiles.Remove(profile);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}