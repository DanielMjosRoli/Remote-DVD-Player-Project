public class Movie
{
    public Guid Id { get; private set; }

    public string Title { get; private set; }
    public string? OriginalTitle { get; private set; }
    public string? Description { get; private set; }

    public int? ReleaseYear { get; private set; }
    public int? DurationMinutes { get; private set; }
    public string? AgeRating { get; private set; }

    public string? PosterPath { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime UpdatedAt { get; private set; }

    public ICollection<MediaFile> MediaFiles { get; private set; } = new List<MediaFile>();
    public ICollection<MovieGenre> Genres { get; private set; } = new List<MovieGenre>();
    public ICollection<CollectionMovie> Collections { get; private set; } = new List<CollectionMovie>();
    public ICollection<Rating> Ratings { get; private set; } = new List<Rating>();
}