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
        <>
            {movies.map((m) =>{
                return(<Card key={m.id} movie={m}></Card>)
            })}
        </>
    );
};