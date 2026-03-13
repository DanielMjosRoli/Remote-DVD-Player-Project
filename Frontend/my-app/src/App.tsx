import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { FrontPage } from "./Pages/FrontPage";
import { MoviePage } from "./Pages/MoviePage";
import { AddMoviePage } from "./Pages/AddMoviePage";
import { NavBar } from "./Components/NavBar";

export function App() {
  return (
    <Router>
      <NavBar/>
      <Routes>
        <Route path="/" element={<FrontPage />} />
        <Route path="/movies/:id" element={<MoviePage/>} />
        <Route path="/movies/add" element={<AddMoviePage/>}/>
      </Routes>
    </Router>
  );
}

export default App
