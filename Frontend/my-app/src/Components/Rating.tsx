import { useEffect, useState } from "react";
import { postRating} from "../API/RatingAPI";
import { getRating } from "../API/movies";
import { useProfile } from "../API/ProfileContext";
import type { RatingSummary } from "../Types/RatingSummary";

type Props = {
  movieId: string;
};

export default function Rating({ movieId }: Props) {
  const { profile } = useProfile();

  const [rating, setRating] = useState({averageRating: 0, ratingCount: 0});
  const [hover, setHover] = useState(0);

  const updateRating = () => {
    getRating(movieId)
      .then(setRating)
      .catch(() => setRating({averageRating: 0, ratingCount: 0}));
  }

  useEffect(() => {
    if (!profile) return;
    updateRating();
  }, [profile, movieId]);

  if (!profile) return null;
  const currentProfile = profile;
  async function handleRate(value: number) {
    /*setRating(value);*/

    try {
      await postRating(currentProfile.id, movieId, value);
      updateRating();
    } catch (err) {
      console.error(err);
    }
  }

  return (
    <div className="flex gap-1">
      {[1, 2, 3, 4, 5].map((star) => (
        <span
          key={star}
          className={`cursor-pointer text-2xl transition ${
            (hover || rating.averageRating) >= star
              ? "text-yellow-400"
              : "text-gray-500"
          }`}
          onClick={() => handleRate(star)}
          onMouseEnter={() => setHover(star)}
          onMouseLeave={() => setHover(0)}
        >
          ★
        </span>
      ))}
    </div>
  );
}