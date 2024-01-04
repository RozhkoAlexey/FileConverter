import axios from "axios";

const defaultOptions = {
  baseURL: "https://localhost:7130/",
};

const instance = axios.create(defaultOptions);

//Here you can configure basic client settings

export default instance;
