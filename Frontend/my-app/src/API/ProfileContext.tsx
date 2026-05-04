import { createContext, useContext, useState } from "react";

type Profile = {
  id: string;
  name: string;
  avatar?: string;
};

type ProfileContextType = {
  profile: Profile | null;
  setProfile: (p: Profile | null) => void;
};

const ProfileContext = createContext<ProfileContextType | undefined>(undefined);

export const ProfileProvider = ({ children }: { children: React.ReactNode }) => {
  const [profile, setProfile] = useState<Profile | null>(() => {
    const saved = localStorage.getItem("profile");
    return saved ? JSON.parse(saved) : null;
  });

  const updateProfile = (p: Profile | null) => {
    localStorage.setItem("profile", JSON.stringify(p));
    setProfile(p);
  };

  return (
    <ProfileContext.Provider value={{ profile, setProfile: updateProfile }}>
      {children}
    </ProfileContext.Provider>
  );
};

export const useProfile = () => {
  const ctx = useContext(ProfileContext);
  if (!ctx) throw new Error("useProfile must be used inside provider");
  return ctx;
};