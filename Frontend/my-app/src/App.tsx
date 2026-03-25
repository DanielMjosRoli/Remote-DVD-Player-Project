import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { FrontPage } from "./Pages/FrontPage";
import { MoviePage } from "./Pages/MoviePage";
import { AddMoviePage } from "./Pages/AddMoviePage";
import { NavBar } from "./Components/NavBar";
import { DriveProvider } from "./API/DriveContext";
import { PlayerBar } from "./Components/PlayerBar";

export function App() {
  return (
    <div className="min-h-screen bg-zinc-800 text-white">
      <DriveProvider>
        <Router>
          <NavBar/>
          <PlayerBar/>
          <Routes>
            <Route path="/" element={<FrontPage />} />
            <Route path="/movies/:id" element={<MoviePage/>} />
            <Route path="/movies/add" element={<AddMoviePage/>}/>
          </Routes>
        </Router>
      </DriveProvider>
    </div>
  );
}

export default App
