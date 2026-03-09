import { useState } from 'react'
import reactLogo from './assets/react.svg'
import viteLogo from '/vite.svg'
import { NavBar } from './Components/NavBar'
import './App.css'
import { FrontPage } from './Pages/FrontPage'

function App() {
  return (
    /**<div className="min-h-screen bg-black flex items-center justify-center">
      <h1 className="text-4xl font-bold text-white">
        Tailwind v4 is working 🚀
      </h1>
      <NavBar User = "John"></NavBar>
    </div>**/
    <FrontPage></FrontPage>
  )
}

export default App
