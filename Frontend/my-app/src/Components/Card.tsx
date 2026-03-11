import { Link } from "react-router-dom";
import type { MovieDTO } from "../Types/movieDTO";

interface CardProps {
    movie : MovieDTO;
    children?: React.ReactNode;
};

export function Card({movie, children}: CardProps){
    return(
    <>
        <Link to={`/movies/${movie.id}`}>
            <div className="bg-zinc-900 rounded-xl overflow-hidden shadow-md hover:shadow-xl hover:scale-[1.02] transition duration-200 cursor-pointer">
            
                <div className="aspect-[2/3] w-full">
                    <img
                        src={movie.posterPath}
                        alt={movie.title}
                        className="w-full h-full object-cover"
                    />
                </div>

                <div className="p-2">
                    <h3 className="text-sm font-medium text-white truncate">
                        {movie.title}
                    </h3>

                    <p className="text-xs text-zinc-400">
                        {movie.releaseYear}
                    </p>
                </div>
            </div>
        </Link>
        {children}
    </>

    );
}