import { useState, useEffect } from "react";
import type { MovieDTO } from "../Types/movieDTO";
import { addMovie, getGenres } from "../API/movies";
import type { GenreDTO } from "../Types/genreDTO";
import { useNavigate } from "react-router-dom";

export function AddMoviePage() {
  const [movie, setMovie] = useState<MovieDTO>({
    title: "",
    originalTitle: "",
    description: "",
    releaseYear: undefined,
    durationMinutes: undefined,
    ageRating: "",
    posterPath: "",
    genres: [],
  });

  const [availableGenres, setAvailableGenres] = useState<GenreDTO[]>([]);
  const [selectedGenres, setSelectedGenres] = useState<string[]>([]);

  // Load genres from API
  useEffect(() => {
    async function loadGenres() {
      const res = await getGenres();
      setAvailableGenres(res);
    }
    loadGenres();
  }, []);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    setMovie({
        ...movie,
        [e.target.name]: e.target.value,
    });
  };

  const handleNumberChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    setMovie({
        ...movie,
        [e.target.name]: Number(e.target.value),
    });
  };

  const handleGenreToggle = (genreId: string) => {
    setSelectedGenres((prev) =>
      prev.includes(genreId) ? prev.filter((id) => id !== genreId) : [...prev, genreId]
    );
  };

    const handleSubmit = async (e: React.SyntheticEvent<HTMLFormElement>) => {
        e.preventDefault();

        const payload: MovieDTO = {
            ...movie,
            genres: availableGenres.filter((g) => selectedGenres.includes(g.id)),
        };

        try {
            const createdMovie = await addMovie(payload);

            navigate(`/movies/${createdMovie.id}`);

        } catch (err) {
            console.error(err);
            alert("Failed to create movie");
        }
    };

    const navigate = useNavigate();

  return (
    <div className="max-w-3xl mx-auto p-6">
      <h1 className="text-2xl font-bold mb-4">Add New Movie</h1>

      <form onSubmit={handleSubmit} className="space-y-4">

        <div>
          <label className="block text-sm font-medium text-white">Title</label>
          <input
            type="text"
            name="title"
            value={movie.title}
            onChange={handleChange}
            required
            className="mt-1 block w-full rounded border-gray-300 shadow-sm p-2 bg-zinc-900 text-white"
          />
        </div>

        <div>
          <label className="block text-sm font-medium text-white">Original Title</label>
          <input
            type="text"
            name="originalTitle"
            value={movie.originalTitle}
            onChange={handleChange}
            className="mt-1 block w-full rounded border-gray-300 shadow-sm p-2 bg-zinc-900 text-white"
          />
        </div>

        <div>
          <label className="block text-sm font-medium text-white">Description</label>
          <textarea
            name="description"
            value={movie.description}
            onChange={handleChange}
            className="mt-1 block w-full rounded border-gray-300 shadow-sm p-2 bg-zinc-900 text-white"
          />
        </div>

        <div className="grid grid-cols-2 gap-4">
          <div>
            <label className="block text-sm font-medium text-white">Release Year</label>
            <input
              type="number"
              name="releaseYear"
              value={movie.releaseYear || ""}
              onChange={handleNumberChange}
              className="mt-1 block w-full rounded border-gray-300 shadow-sm p-2 bg-zinc-900 text-white"
            />
          </div>

          <div>
            <label className="block text-sm font-medium text-white">Duration (min)</label>
            <input
              type="number"
              name="durationMinutes"
              value={movie.durationMinutes || ""}
              onChange={handleNumberChange}
              className="mt-1 block w-full rounded border-gray-300 shadow-sm p-2 bg-zinc-900 text-white"
            />
          </div>
        </div>

        <div>
          <label className="block text-sm font-medium text-white">Age Rating</label>
          <input
            type="text"
            name="ageRating"
            value={movie.ageRating}
            onChange={handleChange}
            className="mt-1 block w-full rounded border-gray-300 shadow-sm p-2 bg-zinc-900 text-white"
          />
        </div>

        <div>
          <label className="block text-sm font-medium text-white">Poster URL</label>
          <input
            type="text"
            name="posterPath"
            value={movie.posterPath}
            onChange={handleChange}
            className="mt-1 block w-full rounded border-gray-300 shadow-sm p-2 bg-zinc-900 text-white"
          />
        </div>

        <div>
          <label className="block text-sm font-medium text-white">Genres</label>
          <div className="flex flex-wrap gap-2 mt-1">
            {availableGenres.map((genre) => (
              <button
                key={genre.id}
                type="button"
                className={`px-3 py-1 rounded-full border ${
                  selectedGenres.includes(genre.id) ? "bg-indigo-600 text-white" : "bg-zinc-700 text-white"
                }`}
                onClick={() => handleGenreToggle(genre.id)}
              >
                {genre.name}
              </button>
            ))}
          </div>
        </div>

        <div>
          <button
            type="submit"
            className="bg-indigo-600 text-white px-4 py-2 rounded hover:bg-indigo-700 transition"
          >
            Add Movie
          </button>
        </div>

      </form>
    </div>
  );
}