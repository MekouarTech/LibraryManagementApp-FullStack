import axios from 'axios';
import { Book, Author, Category, Publisher, CreateBookRequest, CreateAuthorRequest, CreateCategoryRequest, CreatePublisherRequest } from '../types';

const API_BASE_URL = 'http://localhost:5000/api';

const api = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

export const bookService = {
  getAll: async (): Promise<Book[]> => {
    const response = await api.get<Book[]>('/books');
    return response.data;
  },

  getById: async (id: number): Promise<Book> => {
    const response = await api.get<Book>(`/books/${id}`);
    return response.data;
  },

  create: async (book: CreateBookRequest): Promise<Book> => {
    const response = await api.post<Book>('/books', book);
    return response.data;
  },

  update: async (id: number, book: CreateBookRequest): Promise<Book> => {
    const response = await api.put<Book>(`/books/${id}`, { ...book, id });
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/books/${id}`);
  },
};

export const authorService = {
  getAll: async (): Promise<Author[]> => {
    const response = await api.get<Author[]>('/authors');
    return response.data;
  },

  getById: async (id: number): Promise<Author> => {
    const response = await api.get<Author>(`/authors/${id}`);
    return response.data;
  },

  create: async (author: CreateAuthorRequest): Promise<Author> => {
    const response = await api.post<Author>('/authors', author);
    return response.data;
  },

  update: async (id: number, author: CreateAuthorRequest): Promise<Author> => {
    const response = await api.put<Author>(`/authors/${id}`, { ...author, id });
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/authors/${id}`);
  },
};

export const categoryService = {
  getAll: async (): Promise<Category[]> => {
    const response = await api.get<Category[]>('/categories');
    return response.data;
  },

  getById: async (id: number): Promise<Category> => {
    const response = await api.get<Category>(`/categories/${id}`);
    return response.data;
  },

  create: async (category: CreateCategoryRequest): Promise<Category> => {
    const response = await api.post<Category>('/categories', category);
    return response.data;
  },

  update: async (id: number, category: CreateCategoryRequest): Promise<Category> => {
    const response = await api.put<Category>(`/categories/${id}`, { ...category, id });
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/categories/${id}`);
  },
};

export const publisherService = {
  getAll: async (): Promise<Publisher[]> => {
    const response = await api.get<Publisher[]>('/publishers');
    return response.data;
  },

  getById: async (id: number): Promise<Publisher> => {
    const response = await api.get<Publisher>(`/publishers/${id}`);
    return response.data;
  },

  create: async (publisher: CreatePublisherRequest): Promise<Publisher> => {
    const response = await api.post<Publisher>('/publishers', publisher);
    return response.data;
  },

  update: async (id: number, publisher: CreatePublisherRequest): Promise<Publisher> => {
    const response = await api.put<Publisher>(`/publishers/${id}`, { ...publisher, id });
    return response.data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/publishers/${id}`);
  },
}; 