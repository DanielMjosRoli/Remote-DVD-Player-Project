public record GenreViewDTO(
    Guid Id,
    string Name,
    IReadOnlyList<MovieDTO> Movies
);