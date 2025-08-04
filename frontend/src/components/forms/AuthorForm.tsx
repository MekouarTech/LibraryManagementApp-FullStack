import React, { useState } from 'react';
import { CreateAuthorRequest, Author } from '../../types';

interface AuthorFormProps {
  author?: Author;
  onSubmit: (data: CreateAuthorRequest) => Promise<void>;
  onCancel: () => void;
  loading?: boolean;
}

const AuthorForm: React.FC<AuthorFormProps> = ({ author, onSubmit, onCancel, loading = false }) => {
  const [formData, setFormData] = useState<CreateAuthorRequest>({
    firstName: author?.firstName || '',
    lastName: author?.lastName || '',
    biography: author?.biography || '',
    dateOfBirth: author?.dateOfBirth || ''
  });

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    await onSubmit(formData);
  };

  return (
    <form onSubmit={handleSubmit}>
      <div className="form-group">
        <label className="form-label">First Name *</label>
        <input
          type="text"
          name="firstName"
          className="form-input"
          value={formData.firstName}
          onChange={handleInputChange}
          required
          placeholder="Enter first name"
        />
      </div>

      <div className="form-group">
        <label className="form-label">Last Name *</label>
        <input
          type="text"
          name="lastName"
          className="form-input"
          value={formData.lastName}
          onChange={handleInputChange}
          required
          placeholder="Enter last name"
        />
      </div>

      <div className="form-group">
        <label className="form-label">Date of Birth *</label>
        <input
          type="date"
          name="dateOfBirth"
          className="form-input"
          value={formData.dateOfBirth}
          onChange={handleInputChange}
          required
        />
      </div>

      <div className="form-group">
        <label className="form-label">Biography *</label>
        <textarea
          name="biography"
          className="form-textarea"
          value={formData.biography}
          onChange={handleInputChange}
          required
          placeholder="Enter author biography"
        />
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
          {loading ? 'Saving...' : (author ? 'Update Author' : 'Create Author')}
        </button>
      </div>
    </form>
  );
};

export default AuthorForm;  