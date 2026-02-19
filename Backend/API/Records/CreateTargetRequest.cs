public record CreateTargetRequest(
    string BackingFilePath,
    bool ReadOnly,
    long? SizeInBytes = null
);
