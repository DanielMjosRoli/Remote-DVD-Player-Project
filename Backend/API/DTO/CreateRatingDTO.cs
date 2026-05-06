public class CreateRatingDto
{
    public Guid ProfileId { get; set; }
    public Guid MovieId { get; set; }
    public int RatingValue { get; set; } // 1–5
}