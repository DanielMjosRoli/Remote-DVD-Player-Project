import { uploadMedia } from "../API/movies";
import type { Guid } from "../Types/Guid";

interface UploadMovieProps {
  movieId: Guid;
  storageVolumeId: Guid;
  onUploadComplete?: () => void;
}

export function UploadMovie({ movieId, storageVolumeId, onUploadComplete }: UploadMovieProps) {
  return (
    <label className="bg-indigo-600 hover:bg-indigo-700 px-4 py-2 rounded cursor-pointer">
        Upload Video
        <input
            type="file"
            className="hidden"
            onChange={async (e) => {
                if (!e.target.files) return;

                await uploadMedia(
                e.target.files[0],
                movieId,
                storageVolumeId
                );

                onUploadComplete?.();
            }}
        />
    </label>
  );
}