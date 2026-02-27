public record MovieDTO
(
    Guid Id,

    string Title,
    
    string? OriginalTitle,
    string? Description,

    int? ReleaseYear,
    int? DurationMinutes,
    string? AgeRating,

    string? PosterPath,

    DateTime UpdatedAt,

    IReadOnlyList<GenreDTO> Genres
);