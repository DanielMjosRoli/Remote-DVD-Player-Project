import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import App from './App.tsx'
import { ProfileProvider } from './API/ProfileContext.tsx'

const HARDCODED_USER_ID = "31e69792-2aa8-450d-8bad-9dea2eb298cd";

if (!localStorage.getItem("userId")) {
  localStorage.setItem("userId", HARDCODED_USER_ID);
}

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <ProfileProvider>
      <App />
    </ProfileProvider>
  </StrictMode>,
)
