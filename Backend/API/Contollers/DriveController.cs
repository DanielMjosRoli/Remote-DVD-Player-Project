using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[ApiController]
[Route("api/drive")]
public class DriveController : ControllerBase
{
    private readonly IDvdDriveService _dvdDrive;
    private readonly MediaDBContext _context;

    public DriveController(IDvdDriveService dvdDrive, MediaDBContext context)
    {
        _dvdDrive = dvdDrive;
        _context = context;
    }

    [HttpPost("play/{mediaFileId:guid}")]
    public async Task<IActionResult> Play(Guid mediaFileId)
    {
        await _dvdDrive.LoadMediaAsync(mediaFileId);
        Console.WriteLine(mediaFileId);
        return Ok();
    }

    [HttpPost("eject")]
    public async Task<IActionResult> Eject()
    {
        await _dvdDrive.EjectAsync();
        return Ok();
    }

   [HttpGet("status")]
    public async Task<ActionResult<CurrentMovieDTO>> Status()
    {
        var mediaId = _dvdDrive.GetCurrentMedia();

        if (mediaId == null)
        {
            return Ok(new {
                currentMediaFileId = (Guid?)null,
                movieTitle = (string?)null,
                posterPath = (string?)null
            });
        }
        Console.WriteLine("Success");
        var media = await _context.MediaFiles
            .Where(mf => mf.Id == mediaId)
            .Select(mf => new CurrentMovieDTO(
                mf.Movie.Id,
                mf.Movie.Title,
                mf.Movie.PosterPath
            ))
            .FirstOrDefaultAsync();

        return Ok(media);
    }
}