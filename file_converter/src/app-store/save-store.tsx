import { AppReducers } from "./store-types";

export const saveState = (state: AppReducers) => {
  try {
    console.log(state);
    const serializedState = JSON.stringify(state);
    localStorage.setItem("REACT_APP_STORE_KEY", serializedState);
  } catch {
    // We'll just ignore write errors
  }
};

// Loads the state and returns an object that can be provided as the
// preloadedState parameter of store.js's call to configureStore
export const loadState = (): AppReducers | undefined => {
  try {
    const serializedState = localStorage.getItem("REACT_APP_STORE_KEY");

    if (serializedState === null) {
      return undefined;
    }
    return JSON.parse(serializedState);
  } catch (error) {
    return undefined;
  }
};
