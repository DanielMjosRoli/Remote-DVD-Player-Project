public class Genre
{
    public Guid Id { get; private set; }
    public string Name { get; private set; }

    public ICollection<MovieGenre> Movies { get; private set; } = new List<MovieGenre>();
}