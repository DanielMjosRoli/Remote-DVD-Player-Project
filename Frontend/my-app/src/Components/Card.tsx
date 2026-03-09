import type { MovieDTO } from "../Types/movieDTO";

interface CardProps {
    movie : MovieDTO;
    children?: React.ReactNode;
};

export function Card({movie, children}: CardProps){
    return(
    <>
        <div className="bg-zinc-900 rounded-xl overflow-hidden shadow-md hover:shadow-xl hover:scale-[1.02] transition duration-200 cursor-pointer">
        
            <img
                src={movie.posterPath}
                alt={movie.title}
                className="w-full h-72 object-cover"
            />

            <div className="p-4">
                <h3 className="text-white font-semibold text-lg line-clamp-1">
                {movie.title}
                </h3>

                <p className="text-zinc-400 text-sm mt-1">
                {movie.releaseYear}
                </p>
            </div>
        
        </div>
        {children}
    </>

    );
}