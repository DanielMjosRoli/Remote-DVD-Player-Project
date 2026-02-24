public class User
{
    public Guid Id { get; private set; }

    public string Username { get; private set; }
    public string PasswordHash { get; private set; }
    public string Role { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? LastLoginAt { get; private set; }

    public ICollection<WatchHistory> WatchHistory { get; private set; } = new List<WatchHistory>();
    public ICollection<Rating> Ratings { get; private set; } = new List<Rating>();
}