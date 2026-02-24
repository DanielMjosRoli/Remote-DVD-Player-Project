public class Rating
{
    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public Guid MovieId { get; private set; }
    public Movie Movie { get; private set; } = null!;

    public int RatingValue { get; private set; }
    public DateTime RatedAt { get; private set; }
}