import { Link, useNavigate } from "react-router-dom";
import { useEffect, useState } from "react";
import { getGenres } from "../API/movies";
import type { GenreDTO } from "../Types/genreDTO";

export function NavBar() {
  const [menuOpen, setMenuOpen] = useState(false);
  const [search, setSearch] = useState("");
  const [genre, setGenre] = useState("");
  const [genres, setGenres] = useState<GenreDTO[]>([]);
  const navigate = useNavigate();

  function handleSearch(e: React.FormEvent) {
    e.preventDefault();
    const params = new URLSearchParams();
    if (search) params.set("query", search);
    if (genre) params.set("genre", genre);

    navigate(`/?${params.toString()}`);
  }

    useEffect(() => {
    async function loadGenres() {
      const data = await getGenres();
      setGenres(data);
    }

    loadGenres();
  }, []);

  return (
    <nav className="bg-gray-900 text-white shadow sticky top-0 z-50">
      <div className="max-w-7xl mx-auto px-4 py-3 flex items-center justify-between">

        {/* Logo */}
        <Link to="/" className="text-lg font-bold">
          Remote DVD
        </Link>

        {/* Desktop Navigation */}
        <div className="hidden md:flex items-center gap-6">
          <Link to="/" className="hover:text-indigo-400">
            All Movies
          </Link>

          <Link to="/movies/add" className="hover:text-indigo-400">
            Add Movie
          </Link>

          <form onSubmit={handleSearch} className="flex gap-2">
            <input
              type="text"
              placeholder="Search..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              className="px-2 py-1 rounded bg-gray-800 border border-gray-700"
            />
            {/* 🎭 Genre dropdown */}
            <select
              value={genre}
              onChange={(e) => setGenre(e.target.value)}
              className="px-2 py-1 rounded bg-gray-800 border border-gray-700"
            >
              <option value="">All Genres</option>
              {genres.map((g) => (
                <option key={g.id} value={g.name}>
                  {g.name}
                </option>
              ))}
            </select>
            <button className="bg-indigo-600 px-3 py-1 rounded hover:bg-indigo-700">
              Search
            </button>
          </form>
        </div>

        {/* Mobile menu button */}
        <button
          className="md:hidden"
          onClick={() => setMenuOpen(!menuOpen)}
        >
          ☰
        </button>
      </div>

      {/* Mobile Menu */}
      {menuOpen && (
        <div className="md:hidden bg-gray-800 px-4 pb-4 flex flex-col gap-3">

          <Link
            to="/"
            onClick={() => setMenuOpen(false)}
            className="hover:text-indigo-400"
          >
            All Movies
          </Link>

          <Link
            to="/movies/add"
            onClick={() => setMenuOpen(false)}
            className="hover:text-indigo-400"
          >
            Add Movie
          </Link>

          <form onSubmit={handleSearch} className="flex flex-col gap-3">
            <input
              type="text"
              placeholder="Search..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              className="flex-1 px-2 py-1 rounded bg-gray-700 border border-gray-600"
            />
            {/* 🎭 Genre dropdown */}
            <select
              value={genre}
              onChange={(e) => setGenre(e.target.value)}
              className="px-2 py-1 rounded bg-gray-800 border border-gray-700"
            >
              <option value="">All Genres</option>
              {genres.map((g) => (
                <option key={g.id} value={g.name}>
                  {g.name}
                </option>
              ))}
            </select>
            <button className="bg-indigo-600 px-3 py-1 rounded">
              Go
            </button>
          </form>

        </div>
      )}
    </nav>
  );
}