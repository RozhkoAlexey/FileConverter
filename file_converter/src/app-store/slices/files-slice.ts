import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";
import { ItemFile } from "../../types";
import { ItemFileStatusResponse } from "../../types/item-file";

export type FilesState = {
  items: ItemFile[];
};
const initialState: FilesState = {
  items: [],
};

export const filesSlice = createSlice({
  name: "files",
  initialState,
  reducers: {
    add: (state: FilesState, action: PayloadAction<ItemFile>) => {
      state.items.push(action.payload);
    },
    setError: (state: FilesState, action: PayloadAction<string>) => {
      const item = state.items.find((x) => x.fileId == action.payload);

      if (!item) return;

      item.isError = true;
    },
    setConverted: (
      state: FilesState,
      action: PayloadAction<ItemFileStatusResponse>
    ) => {
      const item = state.items.find((x) => x.fileId == action.payload.fileId);

      if (!item) return;

      item.isConverted = true;
      item.resultFileName = action.payload.resultFileName;
    },
    remove: (state: FilesState, action: PayloadAction<string>) => {
      const item = state.items.find((x) => x.fileId == action.payload);

      if (!item) return;

      const index = state.items.indexOf(item);

      state.items.splice(index, 1);
    },
  },
});

export const { actions: filesActions } = filesSlice;
export default filesSlice.reducer;
