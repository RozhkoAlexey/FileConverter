import { ItemFile } from "../types";
import { ItemFileStatusResponse } from "../types/item-file";
import http from "./http-common";

export const uploadFile = async (
  file: any,
  fileInfo: ItemFile,
  sessionKey: string
) => {
  const formData = new FormData();
  formData.append("file", file);
  formData.append("fileId", fileInfo.fileId);
  formData.append("sessionKey", sessionKey);

  await http.post("File/UploadFile", formData, {
    headers: {
      "Content-Type": "multipart/form-data",
    },
  });
};

export const GetFileStatus = async (
  fileId: string,
  sessionKey: string
): Promise<ItemFileStatusResponse | undefined> => {
  try {
    const result = await http.get<ItemFileStatusResponse>(
      `File/GetFileStatus/${sessionKey}/${fileId}`
    );
    return result.data;
  } catch (e) {}

  return undefined;
};

export const DeleteFile = async (fileId: string, sessionKey: string) => {
  await http.delete(`File/DeleteFile/${sessionKey}/${fileId}`);
};

export const DownloadFile = async (
  fileId: string,
  sessionKey: string
): Promise<Blob> => {
  const result = await http.get(`File/DownloadFile/${sessionKey}/${fileId}`, {
    responseType: "blob",
  });

  return result.data;
};
