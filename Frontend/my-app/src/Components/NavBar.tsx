import { Link } from "react-router-dom";
import { useState } from "react";

export function NavBar() {
  const [menuOpen, setMenuOpen] = useState(false);
  const [search, setSearch] = useState("");

  function handleSearch(e: React.FormEvent) {
    e.preventDefault();
    console.log(search);
  }

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

          <form onSubmit={handleSearch} className="flex gap-2">
            <input
              type="text"
              placeholder="Search..."
              value={search}
              onChange={(e) => setSearch(e.target.value)}
              className="flex-1 px-2 py-1 rounded bg-gray-700 border border-gray-600"
            />

            <button className="bg-indigo-600 px-3 py-1 rounded">
              Go
            </button>
          </form>

        </div>
      )}
    </nav>
  );
}