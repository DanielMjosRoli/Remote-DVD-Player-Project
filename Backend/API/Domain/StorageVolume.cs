public class StorageVolume
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }
    public string MountPath { get; private set; }
    public string Type { get; private set; }

    public long? CapacityBytes { get; private set; }
    public long? AvailableBytes { get; private set; }

    public DateTime CreatedAt { get; private set; }

    public ICollection<MediaFile> MediaFiles { get; private set; } = new List<MediaFile>();
}