import axios, { AxiosInstance } from "axios";

// Configuración de la instancia de Axios
const api: AxiosInstance = axios.create({
  baseURL: "https://localhost:7034/api",
  timeout: 5000,
});

export default api;
