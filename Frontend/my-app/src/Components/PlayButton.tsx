type Props = {
  mediaFileId: string;
  isPlaying: boolean;
  isDisabled: boolean;
  onPlay: (id: string) => void;
};

export function PlayButton({ mediaFileId, isPlaying, isDisabled, onPlay }: Props) {
  return (
    <button
      onClick={() => onPlay(mediaFileId)}
      disabled={isDisabled}
      className={`mt-2 px-3 py-1 rounded font-medium ${
        isPlaying
          ? "bg-green-800 text-green-300"
          : isDisabled
          ? "bg-zinc-700 text-zinc-400 cursor-not-allowed"
          : "bg-green-600 hover:bg-green-700"
      }`}
    >
      {isPlaying
      ? "▶ Now Playing"
      : isDisabled
      ? "🔒 In Use"
      : "▶ Play"}
    </button>
  );
}
