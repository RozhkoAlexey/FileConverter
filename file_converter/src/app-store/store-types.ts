import { AuthState } from "./slices/auth-slice";
import { FilesState } from "./slices/files-slice";

export type AppReducers = {
  files: FilesState;
  auth: AuthState;
};
