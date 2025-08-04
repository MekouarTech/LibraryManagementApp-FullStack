import React, { useState, useEffect } from 'react';
import { CreateBookRequest, Book, Author, Category, Publisher } from '../../types';
import { authorService, categoryService, publisherService } from '../../services/api';

interface BookFormProps {
  book?: Book;
  onSubmit: (data: CreateBookRequest) => Promise<void>;
  onCancel: () => void;
  loading?: boolean;
}

const BookForm: React.FC<BookFormProps> = ({ book, onSubmit, onCancel, loading = false }) => {
  const [formData, setFormData] = useState<CreateBookRequest>({
    title: book?.title || '',
    publicationYear: book?.publicationYear || new Date().getFullYear(),
    numberOfCopies: book?.numberOfCopies || 1,
    publisherId: book?.publisherId || 0,
    authorIds: book?.authors.map(a => a.id) || [],
    categoryIds: book?.categories.map(c => c.id) || []
  });

  const [authors, setAuthors] = useState<Author[]>([]);
  const [categories, setCategories] = useState<Category[]>([]);
  const [publishers, setPublishers] = useState<Publisher[]>([]);
  const [formLoading, setFormLoading] = useState(true);

  useEffect(() => {
    fetchFormData();
  }, []);

  const fetchFormData = async () => {
    try {
      const [authorsData, categoriesData, publishersData] = await Promise.all([
        authorService.getAll(),
        categoryService.getAll(),
        publisherService.getAll()
      ]);
      setAuthors(authorsData);
      setCategories(categoriesData);
      setPublishers(publishersData);
    } catch (error) {
      console.error('Error fetching form data:', error);
    } finally {
      setFormLoading(false);
    }
  };

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: name === 'publicationYear' || name === 'numberOfCopies' ? parseInt(value) : value
    }));
  };

  const handleMultiSelectChange = (e: React.ChangeEvent<HTMLSelectElement>, field: 'authorIds' | 'categoryIds') => {
    const selectedOptions = Array.from(e.target.selectedOptions, option => parseInt(option.value));
    setFormData(prev => ({
      ...prev,
      [field]: selectedOptions
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await onSubmit(formData);
  };

  if (formLoading) {
    return <div className="loading">Loading form data...</div>;
  }

  return (
    <form onSubmit={handleSubmit}>
      <div className="form-group">
        <label className="form-label">Title *</label>
        <input
          type="text"
          name="title"
          className="form-input"
          value={formData.title}
          onChange={handleInputChange}
          required
          placeholder="Enter book title"
        />
      </div>

      <div className="form-group">
        <label className="form-label">Publication Year *</label>
        <input
          type="number"
          name="publicationYear"
          className="form-input"
          value={formData.publicationYear}
          onChange={handleInputChange}
          required
          min="1800"
          max={new Date().getFullYear() + 1}
        />
      </div>

      <div className="form-group">
        <label className="form-label">Number of Copies *</label>
        <input
          type="number"
          name="numberOfCopies"
          className="form-input"
          value={formData.numberOfCopies}
          onChange={handleInputChange}
          required
          min="1"
        />
      </div>

      <div className="form-group">
        <label className="form-label">Publisher *</label>
        <select
          name="publisherId"
          className="form-select"
          value={formData.publisherId}
          onChange={handleInputChange}
          required
        >
          <option value="">Select a publisher</option>
          {publishers.map(publisher => (
            <option key={publisher.id} value={publisher.id}>
              {publisher.name}
            </option>
          ))}
        </select>
      </div>

      <div className="form-group">
        <label className="form-label">Authors *</label>
        <select
          multiple
          className="form-select"
          value={formData.authorIds.map(String)}
          onChange={(e) => handleMultiSelectChange(e, 'authorIds')}
          required
          style={{ minHeight: '120px' }}
        >
          {authors.map(author => (
            <option key={author.id} value={author.id}>
              {author.firstName} {author.lastName}
            </option>
          ))}
        </select>
        <small style={{ color: '#757575', fontSize: '12px' }}>
          Hold Ctrl (or Cmd on Mac) to select multiple authors
        </small>
      </div>

      <div className="form-group">
        <label className="form-label">Categories *</label>
        <select
          multiple
          className="form-select"
          value={formData.categoryIds.map(String)}
          onChange={(e) => handleMultiSelectChange(e, 'categoryIds')}
          required
          style={{ minHeight: '120px' }}
        >
          {categories.map(category => (
            <option key={category.id} value={category.id}>
              {category.name}
            </option>
          ))}
        </select>
        <small style={{ color: '#757575', fontSize: '12px' }}>
          Hold Ctrl (or Cmd on Mac) to select multiple categories
        </small>
      </div>

      <div className="modal-footer">
        <button
          type="button"
          className="btn btn-secondary"
          onClick={onCancel}
        >
          Cancel
        </button>
        <button
          type="submit"
          className="btn btn-primary"
          disabled={loading}
        >
          {loading ? 'Saving...' : (book ? 'Update Book' : 'Create Book')}
        </button>
      </div>
    </form>
  );
};

export default BookForm; 