public class MovieGenre
{
    public Guid MovieId { get; private set; }
    public Movie Movie { get; private set; } = null!;

    public Guid GenreId { get; private set; }
    public Genre Genre { get; private set; } = null!;
}