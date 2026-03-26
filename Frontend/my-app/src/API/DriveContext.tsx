import { createContext, useContext, useState, useEffect } from "react";
import { playMedia, ejectMedia, getDriveStatus } from "../API/drive";

type DriveState = {
  currentMediaId: string | null;
  movieTitle: string | null;
  posterPath: string | null;
  refresh: () => Promise<void>;
  play: (id: string) => Promise<void>;
  eject: () => Promise<void>;
};

const DriveContext = createContext<DriveState | null>(null);

export function DriveProvider({ children }: { children: React.ReactNode }) {
  const [currentMediaId, setCurrentMediaId] = useState<string | null>(null);
  const [movieTitle, setMovieTitle] = useState<string | null>(null);
  const [posterPath, setPosterPath] = useState<string | null>(null);

  async function refresh() {
    const data = await getDriveStatus();

    setCurrentMediaId(data.id ?? null);
    setMovieTitle(data.title ?? null);
    setPosterPath(data.posterPath ?? null);
  }

  async function play(id: string) {
    await playMedia(id);
    await refresh();
  }

  async function eject() {
  await ejectMedia();
  await refresh();
}

  useEffect(() => {
    refresh();
  }, []);

  return (
    <DriveContext.Provider
        value={{
          currentMediaId,
          movieTitle,
          posterPath,
          refresh,
          play,
          eject
        }}
      >
      {children}
    </DriveContext.Provider>
  );
}

export function useDrive() {
  const ctx = useContext(DriveContext);
  if (!ctx) throw new Error("useDrive must be used inside DriveProvider");
  return ctx;
}