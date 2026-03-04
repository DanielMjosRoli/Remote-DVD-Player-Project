public record UserRatingDTO(
    Guid Id,
    string Username,
    PagedResult<RatingDTO> Ratings
);