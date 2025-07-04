import axios from 'axios';

const api = axios.create({
  baseURL: import.meta.env.VITE_ENDPOINT_NET,
});

api.interceptors.request.use((config) => {
  const token = localStorage.getItem('authTokenAPI');

  if (token && config.url !== '/Auth/login') {
    config.headers.Authorization = `Bearer ${token}`;
  }

  return config;
});

export default api;