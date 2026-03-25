public interface IDvdDriveService
{
    Task LoadMediaAsync(Guid mediaFileId);
    Task EjectAsync();

    Guid? GetCurrentMedia();
    bool HasMedia();
}