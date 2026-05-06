import axios from "axios";

const BASE_URL = "http://localhost:5280/profile";

// Temporary user handling
const getUserId = (): string => {
  let userId = localStorage.getItem("userId");

  if (!userId) {
    userId = crypto.randomUUID(); // browser-generated UUID
    localStorage.setItem("userId", userId);
  }

  return userId;
};

// ========================
// GET PROFILES
// ========================
export const getProfiles = async () => {
  const userId = getUserId();

  const res = await axios.get(`${BASE_URL}?userId=${userId}`);
  return res.data;
};

// ========================
// CREATE PROFILE
// ========================
export const createProfile = async (data: {
  name: string;
  avatar?: string;
  isKids: boolean;
}) => {
  const userId = getUserId();

  const res = await axios.post(BASE_URL, {
    ...data,
    userId
  });

  return res.data;
};

// ========================
// DELETE PROFILE
// ========================
export const deleteProfile = async (id: string) => {
  await axios.delete(`${BASE_URL}/${id}`);
};