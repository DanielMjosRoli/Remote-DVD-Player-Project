public class MediaFile
{
    public Guid Id { get; private set; }

    public Guid MovieId { get; private set; }
    public Movie Movie { get; private set; } = null!;

    public Guid StorageVolumeId { get; private set; }
    public StorageVolume StorageVolume { get; private set; } = null!;

    public string FilePath { get; private set; }
    public long FileSizeBytes { get; private set; }

    public string? Checksum { get; private set; }
    public string? ContainerFormat { get; private set; }
    public string? Resolution { get; private set; }
    public string? AudioFormat { get; private set; }
    public string? SubtitleLanguages { get; private set; }

    public DateTime CreatedAt { get; private set; }
}