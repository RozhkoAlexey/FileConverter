import { configureStore, combineReducers, Reducer } from "@reduxjs/toolkit";
import throttle from "lodash.throttle";

import FilesReducer from "./slices/files-slice";
import AuthReducer from "./slices/auth-slice";

import { loadState, saveState } from "./save-store";

const rootReducer = combineReducers({
  files: FilesReducer,
  auth: AuthReducer,
});

export const store = configureStore({
  reducer: rootReducer,
  preloadedState: loadState(),
});

store.subscribe(throttle(() => saveState(store.getState()), 1000));

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof rootReducer>;
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch;
