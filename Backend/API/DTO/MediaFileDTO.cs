public record MediaFilesDTO(
    Guid Id,
    Guid StorageVolumeId,
    string FilePath,
    long FileSizeBytes,
    string? Checksum,
    string? ContainerFormat,
    string? Resolution,
    string? AudioFormat,
    string? SubtitleLanguages,
    DateTime CreatedAt
);