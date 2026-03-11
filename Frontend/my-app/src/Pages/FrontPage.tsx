import { useEffect, useState } from "react";
import type { MovieDTO } from "../Types/movieDTO";
import type { PagedResult } from "../Types/pagedResult";
import { getMovies } from "../API/movies";
import { Card } from "../Components/Card";

export function FrontPage() {
    const [movies, setMovies] = useState<MovieDTO[]>([]); 

    useEffect(() => {
    async function loadMovies(page: number) {
        const result: PagedResult<MovieDTO> = await getMovies(page);
        setMovies(result.items);
    }

    loadMovies(1);
    }, []);
    return(
        <div className="p-6 grid grid-cols-2 sm:grid-cols-3 md:grid-cols-4 lg:grid-cols-6 gap-6">
            {movies.map((m) => (
                <Card key={m.id} movie={m} />
            ))}
        </div>
    );
};