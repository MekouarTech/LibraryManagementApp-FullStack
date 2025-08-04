export interface Book {
  id: number;
  title: string;
  publicationYear: number;
  numberOfCopies: number;
  publisherId: number;
  publisherName: string;
  authors: Author[];
  categories: Category[];
}

export interface Author {
  id: number;
  firstName: string;
  lastName: string;
  biography: string;
  dateOfBirth: string;
}

export interface Category {
  id: number;
  name: string;
}

export interface Publisher {
  id: number;
  name: string;
}

export interface CreateBookRequest {
  title: string;
  publicationYear: number;
  numberOfCopies: number;
  publisherId: number;
  authorIds: number[];
  categoryIds: number[];
}

export interface CreateAuthorRequest {
  firstName: string;
  lastName: string;
  biography: string;
  dateOfBirth: string;
}

export interface CreateCategoryRequest {
  name: string;
}

export interface CreatePublisherRequest {
  name: string;
} 