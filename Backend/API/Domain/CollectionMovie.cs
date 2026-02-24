public class CollectionMovie
{
    public Guid CollectionId { get; private set; }
    public Collection Collection { get; private set; } = null!;

    public Guid MovieId { get; private set; }
    public Movie Movie { get; private set; } = null!;
}