using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class DvdDriveService : IDvdDriveService
{
    private readonly IServiceScopeFactory _scopeFactory;

    private Guid? _currentMediaFileId;

    public DvdDriveService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public async Task LoadMediaAsync(Guid mediaFileId)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MediaDBContext>();

        var media = await context.MediaFiles
            .Include(m => m.StorageVolume)
            .FirstOrDefaultAsync(m => m.Id == mediaFileId);

        if (media == null)
            throw new Exception("Media not found");
        Console.WriteLine(media);
        _currentMediaFileId = mediaFileId;

        var fullPath = Path.Combine(
            media.StorageVolume.MountPath,
            media.FilePath
        );

        // Call SCST / emulator here
    }

    public Task EjectAsync()
    {
        _currentMediaFileId = null;

        // emulator eject

        return Task.CompletedTask;
    }

    public Guid? GetCurrentMedia() => _currentMediaFileId;

    public bool HasMedia() => _currentMediaFileId != null;
}