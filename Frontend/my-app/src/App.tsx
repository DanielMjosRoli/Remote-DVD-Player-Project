import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import { FrontPage } from "./Pages/FrontPage";
import { MoviePage } from "./Pages/MoviePage";

export function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<FrontPage />} />
        <Route path="/movies/:id" element={<MoviePage />} />
      </Routes>
    </Router>
  );
}

export default App
