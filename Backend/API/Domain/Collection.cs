public class Collection
{
    public Guid Id { get; private set; }

    public string Name { get; private set; }
    public string? Description { get; private set; }

    public ICollection<CollectionMovie> Movies { get; private set; } = new List<CollectionMovie>();
}