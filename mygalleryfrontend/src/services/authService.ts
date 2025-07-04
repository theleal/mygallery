import api from '../api/axios.ts';

export interface LoginPayload {
  login: string;
  password: string;
}

interface LoginResponse {
  token: string;
}

export const login = async (payload: LoginPayload): Promise<LoginResponse> => {

  const response = await api.post<LoginResponse>('/Auth/login', payload);

  localStorage.setItem('authTokenAPI', response.data.token);

  return response.data;
};
 