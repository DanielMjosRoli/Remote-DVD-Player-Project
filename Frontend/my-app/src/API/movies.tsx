import axios from "axios";
import type { PagedResult } from "../Types/pagedResult";
import type { MovieDTO } from "../Types/movieDTO";
import type { MoviePageDTO } from "../Types/MoviePageDTO";

const api = axios.create({
    baseURL: "http://localhost:5280"
});

export async function getMovies(page: number): Promise<PagedResult<MovieDTO>> {
    const response = await api.get(`/movies?page=${page}`);
    return response.data;
}

export async function getMovieById(id: string): Promise<MoviePageDTO> {
  const res = await fetch(`http://localhost:5280/movies/${id}`);
  if (!res.ok) throw new Error("Movie not found");
  return res.json();
}