import type { GenreDTO } from "./genreDTO";
import type { Guid } from "./Guid";
import type { MediaFilesDTO } from "./mediaFilesDTO";

export interface MoviePageDTO{
    id: Guid;
    title: string;
    originalTitle: string;
    description: string;
    releaseYear: number;
    durationMinutes: number;
    ageRating: string;
    posterPath: string;
    updatedAt: Date;
    mediaFiles: MediaFilesDTO[];
    genres: GenreDTO[];
}