import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { authorService } from '../services/api';
import { CreateAuthorRequest } from '../types';

const CreateAuthor: React.FC = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState<CreateAuthorRequest>({
    firstName: '',
    lastName: '',
    biography: '',
    dateOfBirth: ''
  });
  const [loading, setLoading] = useState(false);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement | HTMLTextAreaElement>) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();
    setLoading(true);

    try {
      await authorService.create(formData);
      navigate('/authors');
    } catch (error) {
      console.error('Error creating author:', error);
      alert('Failed to create author. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <div className="page-header">
        <h1 className="page-title">Add New Author</h1>
        <p className="page-subtitle">Create a new author entry</p>
      </div>

      <div className="page-content">
        <div className="data-table">
          <div className="table-header">
            <h3 className="table-title">Author Information</h3>
          </div>
          <div style={{ padding: '24px' }}>
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

              <div style={{ display: 'flex', gap: '12px', justifyContent: 'flex-end', marginTop: '24px' }}>
                <button
                  type="button"
                  className="btn btn-secondary"
                  onClick={() => navigate('/authors')}
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  className="btn btn-primary"
                  disabled={loading}
                >
                  {loading ? 'Creating...' : 'Create Author'}
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CreateAuthor; 