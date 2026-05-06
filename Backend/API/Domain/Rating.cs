public class Rating
{
    public Guid ProfileId { get; set; }
    public Profile Profile { get; set; } = null!;

    public Guid MovieId { get; set; }
    public Movie Movie { get; set; } = null!;

    public int RatingValue { get; set; }
    public DateTime RatedAt { get; set; }
}