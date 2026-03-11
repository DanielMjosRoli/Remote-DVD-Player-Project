import type { GenreDTO } from "./genreDTO";
import type { Guid } from "./Guid";

export interface MovieDTO{
    id?: Guid;
    title: string;
    originalTitle?: string;
    description?: string;
    releaseYear?: number;
    durationMinutes?: number;
    ageRating?: string;
    posterPath?: string;
    updatedAt?: Date;
    genres: GenreDTO[];
}