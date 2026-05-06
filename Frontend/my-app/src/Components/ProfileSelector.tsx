import { useEffect, useState } from "react";
import { useProfile } from "../API/ProfileContext";
import { getProfiles, createProfile } from "../API/ProfileAPI";
import type { Profile } from "../API/ProfileContext";

export default function ProfileSelector() {
  const { setProfile } = useProfile();
  const [profiles, setProfiles] = useState<Profile[]>([]);

  const [showModal, setShowModal] = useState(false);
  const [name, setName] = useState("");
  const [isKids, setIsKids] = useState(false);

  useEffect(() => {
    loadProfiles();
  }, []);

  async function loadProfiles() {
    const data = await getProfiles();
    setProfiles(data);
  }

  async function handleCreateProfile() {
    if (!name.trim()) return;

    const newProfile = await createProfile({
      name,
      isKids,
      avatar: "",
    });

    setShowModal(false);
    setName("");
    setIsKids(false);

    await loadProfiles();

    // Optional: auto-select new profile
    setProfile(newProfile);
  }

  return (
    <div className="min-h-screen flex flex-col items-center justify-center bg-gray-900 text-white px-4">

      <h1 className="text-4xl font-semibold mb-12">
        Who's watching?
      </h1>

      <div className="flex flex-wrap justify-center gap-8">
        {profiles.map((p) => (
          <div
            key={p.id}
            onClick={() => setProfile(p)}
            className="flex flex-col items-center cursor-pointer group"
          >
            <img
              src={p.avatar || "/default-avatar.png"}
              className="w-28 h-28 rounded-lg object-cover border-2 border-transparent group-hover:scale-110 group-hover:border-white transition"
            />
            <p className="mt-3 text-gray-400 group-hover:text-white transition">
              {p.name}
            </p>
          </div>
        ))}

        {/* ADD PROFILE */}
        <div
          onClick={() => setShowModal(true)}
          className="flex flex-col items-center cursor-pointer group"
        >
          <div className="w-28 h-28 rounded-lg bg-gray-800 flex items-center justify-center text-4xl text-gray-400 group-hover:bg-gray-700 group-hover:text-white transition">
            +
          </div>
          <p className="mt-3 text-gray-400 group-hover:text-white transition">
            Add Profile
          </p>
        </div>
      </div>

      {/* MODAL */}
      {showModal && (
        <div className="fixed inset-0 bg-black bg-opacity-70 flex items-center justify-center">
          <div className="bg-gray-800 p-6 rounded-lg w-80">

            <h2 className="text-xl mb-4">Create Profile</h2>

            <input
              type="text"
              placeholder="Profile name"
              value={name}
              onChange={(e) => setName(e.target.value)}
              className="w-full mb-4 px-3 py-2 rounded bg-gray-700 border border-gray-600"
            />

            <label className="flex items-center gap-2 mb-4">
              <input
                type="checkbox"
                checked={isKids}
                onChange={(e) => setIsKids(e.target.checked)}
              />
              Kids profile
            </label>

            <div className="flex justify-end gap-3">
              <button
                onClick={() => setShowModal(false)}
                className="px-3 py-1 bg-gray-600 rounded hover:bg-gray-500"
              >
                Cancel
              </button>

              <button
                onClick={handleCreateProfile}
                className="px-3 py-1 bg-indigo-600 rounded hover:bg-indigo-700"
              >
                Create
              </button>
            </div>

          </div>
        </div>
      )}
    </div>
  );
}