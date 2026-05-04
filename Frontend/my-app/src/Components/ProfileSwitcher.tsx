import { useProfile } from "../API/ProfileContext";

export default function ProfileSwitcher() {
  const { profile, setProfile } = useProfile();

  return (
    <div className="absolute top-4 right-4">
      <button onClick={() => setProfile(null)}>
        {profile?.name} ▼
      </button>
    </div>
  );
}