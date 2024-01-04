export type ItemFile = {
  fileName: string;
  fileSize: number;
  fileId: string;
  isConverted: boolean;
  isError: boolean;
  resultFileName?: string;
};

export enum ItemFileConvertStatus {
  New,
  Completed,
}

export type ItemFileStatusResponse = {
  fileId: string;
  resultFileName: string;
  status: ItemFileConvertStatus;
};
