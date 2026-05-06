import axios from "axios";

const api = axios.create({
  baseURL: "http://localhost:5280/ratings",
  headers: {
    "Content-Type": "application/json",
  },
  validateStatus: (status) => status < 500
});

export async function postRating(
  profileId: string,
  movieId: string,
  ratingValue: number
) {
    await api.post("/ratings", {
    profileId,
    movieId,
    ratingValue,
  });
}