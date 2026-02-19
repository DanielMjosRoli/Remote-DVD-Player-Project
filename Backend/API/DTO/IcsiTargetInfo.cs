public record IscsiTargetInfo(
    Iqn Iqn,
    bool IsActive,
    bool ReadOnly,
    int ConnectedSessions
);
