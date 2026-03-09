import { useEffect, useState } from "react";
import { getMovies } from "../API/movies";
import type { PagedResult } from "../Types/pagedResult";
import type { MovieDTO } from "../Types/movieDTO";

interface ButtonProps {
  children?: React.ReactNode;
}

export function Button({ children }: ButtonProps) {

  return (
    <button className="bg-indigo-600 text-white px-4 py-2 rounded-lg hover:bg-indigo-700 transition">
      {children}
    </button>
  );
}