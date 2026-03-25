import axios from "axios";
import type { DriveStatus } from "../Types/DriveStatus";

const api = axios.create({
  baseURL: "http://localhost:5280",
  validateStatus: (status) => status < 500
});

export async function playMedia(mediaFileId: string): Promise<void> {
  await api.post(`/api/drive/play/${mediaFileId}`, null);
}

export async function ejectMedia(): Promise<void> {
  await api.post(`/api/drive/eject`, null);
}

export async function getDriveStatus(): Promise<DriveStatus> {
  const res = await api.get("/api/drive/status");
  return res.data;
}