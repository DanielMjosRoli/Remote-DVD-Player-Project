import axios from "axios";
import type { PagedResult } from "../Types/pagedResult";
import type { MovieDTO } from "../Types/movieDTO";
import type { MoviePageDTO } from "../Types/MoviePageDTO";
import type { GenreDTO } from "../Types/genreDTO";

const api = axios.create({
    baseURL: "http://localhost:5280"
});

export async function getMovies(page: number): Promise<PagedResult<MovieDTO>> {
    const response = await api.get<PagedResult<MovieDTO>>(`/movies?page=${page}`, { validateStatus: function (status) {
    return status < 500;
  }});
    return response.data;
}

export async function getMovieById(id: string): Promise<MoviePageDTO> {
  const res = await api.get<MoviePageDTO>(`http://localhost:5280/movies/${id}`, { validateStatus: function (status) {
    return status < 500;
  }});
  return res.data;
}

export async function addMovie(movie: MovieDTO) : Promise<MovieDTO> {
    const result = await api.post<MovieDTO>("/movies", movie, { validateStatus: function (status) {
    return status < 500;
  }});
    return result.data;
}

export async function getGenres(): Promise<GenreDTO[]> {
    const response = await api.get("/genres", { validateStatus: function (status) {
    return status < 500;
  }});
    return response.data;
}

export async function uploadMedia(
  file: File,
  movieId: string,
  storageVolumeId: string
) {
  const formData = new FormData();

  formData.append("file", file);
  formData.append("movieId", movieId);
  formData.append("storageVolumeId", storageVolumeId);

  const res = await api.post("/movies/upload", formData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });

  return res.data;
}