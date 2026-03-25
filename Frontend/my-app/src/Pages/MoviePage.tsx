import { useParams } from "react-router-dom";
import { useEffect, useState } from "react";
import { getMovieById } from "../API/movies";
import type { MoviePageDTO } from "../Types/MoviePageDTO";
import { UploadMovie } from "../Components/UploadMovie";
import type { Guid } from "../Types/Guid";
import { PlayButton } from "../Components/PlayButton";
import { useDrive } from "../API/DriveContext";
import { EjectButton } from "../Components/EjectButton";

export function MoviePage() {
  const { currentMediaId, play } = useDrive();
  const { id } = useParams<{ id: Guid }>();
  const [movie, setMovie] = useState<MoviePageDTO | null>(null);
  const [loading, setLoading] = useState(true);

  async function loadMovie() {
      if (!id) return;

      const result = await getMovieById(id);
      setMovie(result);
      setLoading(false);
    }

  useEffect(() => {
    loadMovie();
  }, [id]);

  if (loading) return <p className="p-6 text-white">Loading...</p>;
  if (!movie) return <p className="p-6 text-white">Movie not found</p>;
  
  return (
    <div className="max-w-6xl mx-auto p-6 text-white">
      {/* Top Section */}
      <div className="flex flex-col md:flex-row gap-8">

        {/* Poster */}
        <img
          src={movie.posterPath}
          alt={movie.title}
          className="w-full md:w-72 rounded-xl object-cover"
        />

        {/* Movie Info */}
        <div className="flex-1">
          <h1 className="text-4xl font-bold">{movie.title}</h1>

          {movie.originalTitle && (
            <p className="text-zinc-400 italic">{movie.originalTitle}</p>
          )}

          <p className="mt-4 text-zinc-300">{movie.description}</p>

          <div className="mt-4 space-y-1 text-zinc-300">
            <p><b>Release Year:</b> {movie.releaseYear}</p>
            <p><b>Duration:</b> {movie.durationMinutes} minutes</p>
            <p><b>Age Rating:</b> {movie.ageRating}</p>
          </div>

          {/* Genres */}
          <div className="mt-4 flex flex-wrap gap-2">
            {movie.genres.map((genre) => (
              <span
                key={genre.id}
                className="bg-indigo-600 px-3 py-1 rounded-full text-sm"
              >
                {genre.name}
              </span>
            ))}
          </div>
        </div>
      </div>

      {/* Upload Section */}
      <div className="mt-10">
        <h2 className="text-2xl font-semibold mb-4">Upload Media</h2>

        {id && (
          <UploadMovie
            movieId={id}
            storageVolumeId={"2e078117-f17e-4d38-b2e9-30683b12c9eb"}
            onUploadComplete={() => loadMovie()}
          />
        )}
      </div>

      {/* Media Files Section */}
      <div className="mt-10">
        <h2 className="text-2xl font-semibold mb-4">Media Files</h2>

        <div className="space-y-4">
          {movie.mediaFiles.map((file) => (
            <div
              key={file.id}
              className="bg-zinc-900 border border-zinc-800 rounded-lg p-4"
            >
              <p className="font-medium">{file.filePath}</p>
              <PlayButton
                mediaFileId={file.id}
                isPlaying={currentMediaId === file.id}
                onPlay={currentMediaId === file.id ? () => {} : play}
              />
              <div className="grid grid-cols-2 md:grid-cols-4 gap-2 text-sm text-zinc-400 mt-2">
                <p><b>Container:</b> {file.containerFormat}</p>
                <p><b>Resolution:</b> {file.resolution}</p>
                <p><b>Audio:</b> {file.audioFormat}</p>
                <p><b>Subtitles:</b> {file.subtitleLanguages}</p>
                <p><b>Size:</b> {(file.fileSizeBytes / 1_000_000_000).toFixed(2)} GB</p>
                <p><b>Created:</b> {new Date(file.createdAt).toLocaleDateString()}</p>
              </div>
            </div>
          ))}
        </div>
      </div>
    </div>
  );
}