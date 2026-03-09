import axios from "axios";
import type { PagedResult } from "../Types/pagedResult";
import type { MovieDTO } from "../Types/movieDTO";

const api = axios.create({
    baseURL: "http://localhost:5280"
});

export async function getMovies(page: number): Promise<PagedResult<MovieDTO>> {
    const response = await api.get(`/movies?page=${page}`);
    return response.data;
}