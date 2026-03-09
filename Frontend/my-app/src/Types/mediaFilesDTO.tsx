import type { Guid } from "./Guid";

export interface MediaFilesDTO {
  id: Guid;
  storageVolumeId: Guid;
  filePath: string;
  fileSizeBytes: number;
  checksum?: string;
  containerFormat?: string;
  resolution?: string;
  audioFormat?: string;
  subtitleLanguages?: string;
  createdAt: string;
}