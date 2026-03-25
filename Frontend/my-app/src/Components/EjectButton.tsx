type Props = {
  hasMedia: boolean;
  onEject: () => void;
};

export function EjectButton({ hasMedia, onEject }: Props) {
  return (
    <button
      onClick={onEject}
      disabled={!hasMedia}
      className={`px-4 py-2 rounded font-medium ${
        hasMedia
          ? "bg-red-600 hover:bg-red-700"
          : "bg-zinc-700 text-zinc-400 cursor-not-allowed"
      }`}
    >
      ⏏ Eject
    </button>
  );
}