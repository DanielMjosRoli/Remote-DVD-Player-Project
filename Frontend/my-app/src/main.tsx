import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { ProfileProvider } from './API/ProfileContext.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ProfileProvider>
      <App />
    </ProfileProvider>
  </StrictMode>,
)
