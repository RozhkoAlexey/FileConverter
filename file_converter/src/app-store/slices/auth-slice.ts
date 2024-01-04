import { createSlice } from "@reduxjs/toolkit";
import type { PayloadAction } from "@reduxjs/toolkit";

export type AuthState = {
  key: string;
};
const initialState: AuthState = {
  key: "",
};

export const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    auth: (state: AuthState, action: PayloadAction<string>) => {
      state.key = action.payload;
    },
  },
});

export const { actions: authAction } = authSlice;
export default authSlice.reducer;
