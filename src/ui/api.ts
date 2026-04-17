import axios from 'axios';

const api = axios.create({ baseURL: 'http://localhost:5274/api' });

export const customerApi = {
  getAll: () => api.get('/customers'),
  create: (data: any) => api.post('/customers', data),
  update: (id: number, data: any) => api.put(`/customers/${id}`, data),
  delete: (id: number) => api.delete(`/customers/${id}`),
  commit: (customers: any[]) => api.post('/customers/commit', customers),
};