using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public class DvdDriveService : IDvdDriveService
{
    private readonly IServiceScopeFactory _scopeFactory;

    private readonly IDvdHardwareController _hardware;

    private Guid? _currentMediaFileId;

    public DvdDriveService(IServiceScopeFactory scopeFactory, IDvdHardwareController hardware)
    {
        _scopeFactory = scopeFactory;
        _hardware = hardware;
    }

    public async Task LoadMediaAsync(Guid mediaFileId)
    {
        using var scope = _scopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MediaDBContext>();

        var media = await context.MediaFiles.FindAsync(mediaFileId);

        if (media == null)
            throw new Exception("Media not found");
        _currentMediaFileId = mediaFileId;

        var fullPath = Path.Combine(
            media.StorageVolume.MountPath,
            media.FilePath
        );
        await _hardware.EjectAsync();        
        await Task.Delay(500);                // simulate hardware delay
        await _hardware.InsertAsync(fullPath);

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