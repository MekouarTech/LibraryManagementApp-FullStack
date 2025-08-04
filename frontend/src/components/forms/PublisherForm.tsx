import React, { useState } from 'react';
import { CreatePublisherRequest, Publisher } from '../../types';

interface PublisherFormProps {
  publisher?: Publisher;
  onSubmit: (data: CreatePublisherRequest) => Promise<void>;
  onCancel: () => void;
  loading?: boolean;
}

const PublisherForm: React.FC<PublisherFormProps> = ({ publisher, onSubmit, onCancel, loading = false }) => {
  const [formData, setFormData] = useState<CreatePublisherRequest>({
    name: publisher?.name || ''
  });

  const handleInputChange = (e: React.ChangeEvent<HTMLInputElement>) => {
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
          {loading ? 'Saving...' : (publisher ? 'Update Publisher' : 'Create Publisher')}
        </button>
      </div>
    </form>
  );
};

export default PublisherForm; 