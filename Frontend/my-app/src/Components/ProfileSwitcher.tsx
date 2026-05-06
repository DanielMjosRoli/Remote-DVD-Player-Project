import { useProfile } from "../API/ProfileContext";

export default function ProfileSwitcher() {

  const { profile, setProfile } = useProfile();

  return (
    <div className="relative">
      {/* Selected Profile */}
      <button
        onClick={() => setProfile(null)}
        className="flex items-center gap-2 bg-gray-800 px-3 py-1 rounded hover:bg-gray-700"
      >
        <div className="w-8 h-8 bg-gray-600 rounded" />
        <span>{profile?.name || "Select Profile"}</span>
      </button>
    </div>
  );
}
{/*  const { profile, setProfile } = useProfile();

  return (
    <div className="relative top-4 right-4">
      <button onClick={() => setProfile(null)}>
        {profile?.name} ▼
      </button>
    </div>
  );
*/}