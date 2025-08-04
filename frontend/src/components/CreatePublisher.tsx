import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import { publisherService } from '../services/api';
import { CreatePublisherRequest } from '../types';

const CreatePublisher: React.FC = () => {
  const navigate = useNavigate();
  const [formData, setFormData] = useState<CreatePublisherRequest>({
    name: ''
  });
  const [loading, setLoading] = useState(false);

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
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
      await publisherService.create(formData);
      navigate('/publishers');
    } catch (error) {
      console.error('Error creating publisher:', error);
      alert('Failed to create publisher. Please try again.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <div>
      <div className="page-header">
        <h1 className="page-title">Add New Publisher</h1>
        <p className="page-subtitle">Create a new publisher entry</p>
      </div>

      <div className="page-content">
        <div className="data-table">
          <div className="table-header">
            <h3 className="table-title">Publisher Information</h3>
          </div>
          <div style={{ padding: '24px' }}>
            <form onSubmit={handleSubmit}>
              <div className="form-group">
                <label className="form-label">Publisher Name *</label>
                <input
                  type="text"
                  name="name"
                  className="form-input"
                  value={formData.name}
                  onChange={handleInputChange}
                  required
                  placeholder="Enter publisher name"
                />
              </div>

              <div style={{ display: 'flex', gap: '12px', justifyContent: 'flex-end', marginTop: '24px' }}>
                <button
                  type="button"
                  className="btn btn-secondary"
                  onClick={() => navigate('/publishers')}
                >
                  Cancel
                </button>
                <button
                  type="submit"
                  className="btn btn-primary"
                  disabled={loading}
                >
                  {loading ? 'Creating...' : 'Create Publisher'}
                </button>
              </div>
            </form>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CreatePublisher; 