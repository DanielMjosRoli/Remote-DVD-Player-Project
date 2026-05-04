import axios from "axios";
import type { PagedResult } from "../Types/pagedResult";
import type { MovieDTO } from "../Types/movieDTO";
import type { MoviePageDTO } from "../Types/MoviePageDTO";
import type { GenreDTO } from "../Types/genreDTO";

const api = axios.create({
  baseURL: "http://localhost:5280",
  validateStatus: (status) => status < 500
});

api.interceptors.request.use((config) => {
  const profile = JSON.parse(localStorage.getItem("profile") || "null");
  if (profile) {
    config.headers["X-Profile-Id"] = profile.id;
  }
  return config;
});

export async function getMovies(
  page: number,
  query?: string,
  genre?: string
): Promise<PagedResult<MovieDTO>> {
    let url = `/movies?page=${page}`;

    if (query && query.trim() !== "") {
        url += `&query=${encodeURIComponent(query)}`;
    }

    if (genre && genre.trim() !== "") {
        url += `&genre=${encodeURIComponent(genre)}`;
    }

    const response = await api.get<PagedResult<MovieDTO>>(url);
    return response.data;
}

export async function getMovieById(id: string): Promise<MoviePageDTO> {
  const res = await api.get<MoviePageDTO>(`/movies/${id}`);
  return res.data;
}

export async function addMovie(movie: MovieDTO) : Promise<MovieDTO> {
    const result = await api.post<MovieDTO>("/movies", movie);
    return result.data;
}

export async function getGenres(): Promise<GenreDTO[]> {
    const response = await api.get("/genres");
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

  const res = await api.post(`/movies/${movieId}/upload`, formData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });

  return res.data;
}