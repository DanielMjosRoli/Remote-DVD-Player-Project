public class WatchHistory
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public Guid MovieId { get; private set; }
    public Movie Movie { get; private set; } = null!;

    public int LastPositionSeconds { get; private set; }
    public bool Completed { get; private set; }
    public DateTime LastWatchedAt { get; private set; }
}