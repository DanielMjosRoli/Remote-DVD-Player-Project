type Props = {
  mediaFileId: string;
  isPlaying: boolean;
  onPlay: (id: string) => void;
};

export function PlayButton({ mediaFileId, isPlaying, onPlay }: Props) {
  return (
    <button
      onClick={() => onPlay(mediaFileId)}
      className={`mt-2 px-3 py-1 rounded font-medium ${
        isPlaying
          ? "bg-green-800 text-green-300"
          : "bg-green-600 hover:bg-green-700"
      }`}
    >
      {isPlaying ? "▶ Now Playing" : "▶ Play"}
    </button>
  );
}
