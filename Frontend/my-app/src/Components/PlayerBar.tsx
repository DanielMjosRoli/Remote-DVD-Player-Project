import { useDrive } from "../API/DriveContext";
import { EjectButton } from "./EjectButton";

export function PlayerBar() {
  const { currentMediaId, movieTitle, posterPath, eject } = useDrive();
  if (!currentMediaId) return null;

  return (
    <div className="w-full bg-zinc-900 border-b border-zinc-800 px-6 py-3 flex items-center justify-between">

      <div className="flex items-center gap-4">
        {posterPath && (
          <img
            src={posterPath}
            className="w-10 h-14 object-cover rounded"
          />
        )}

        <span className="text-green-400 font-semibold">
          ▶ Now Playing: {movieTitle ?? "Unknown"}
        </span>
      </div>

      <EjectButton
        hasMedia={true}
        onEject={eject}
      />
    </div>
  );
}