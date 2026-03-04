public record RatingDTO(
    Guid UserId,
    Guid MovieId,
    MovieDTO Movie,
    int RatingValue,
    DateTime RatedAt
);