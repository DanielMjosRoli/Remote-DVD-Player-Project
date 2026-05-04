import { useEffect, useState } from "react";
import { useProfile } from "../API/ProfileContext";
import axios from "axios";

export default function ProfileSelector() {
  const [profiles, setProfiles] = useState([]);
  const { setProfile } = useProfile();

  useEffect(() => {
    axios.get("/api/profiles").then(res => setProfiles(res.data));
  }, []);

  return (
    <div className="h-screen flex flex-col items-center justify-center bg-black text-white">
      <h1 className="text-3xl mb-8">Who's watching?</h1>

      <div className="flex gap-6">
        {profiles.map((p: any) => (
          <div
            key={p.id}
            className="cursor-pointer text-center"
            onClick={() => setProfile(p)}
          >
            <img
              src={p.avatar || "/default-avatar.png"}
              className="w-24 h-24 rounded-md hover:scale-110 transition"
            />
            <p className="mt-2">{p.name}</p>
          </div>
        ))}
      </div>
    </div>
  );
}