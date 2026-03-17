public class UploadRequest
{
    public IFormFile File { get; set; } = null!;

    public Guid MovieId { get; set; }

    public Guid StorageVolumeId { get; set; }
}