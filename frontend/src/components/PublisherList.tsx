import React, { useState, useEffect } from 'react';
import { publisherService } from '../services/api';
import { Publisher } from '../types';
import Modal from './Modal';
import PublisherForm from './forms/PublisherForm';

const PublisherList: React.FC = () => {
  const [publishers, setPublishers] = useState<Publisher[]>([]);
  const [filteredPublishers, setFilteredPublishers] = useState<Publisher[]>([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const [isAddModalOpen, setIsAddModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [editingPublisher, setEditingPublisher] = useState<Publisher | null>(null);
  const [formLoading, setFormLoading] = useState(false);

  useEffect(() => {
    fetchPublishers();
  }, []);

  useEffect(() => {
    filterPublishers();
  }, [publishers, searchTerm]);

  const fetchPublishers = async () => {
    try {
      const data = await publisherService.getAll();
      setPublishers(data);
    } catch (error) {
      console.error('Error fetching publishers:', error);
    } finally {
      setLoading(false);
    }
  };

  const filterPublishers = () => {
    let filtered = publishers;

    if (searchTerm) {
      filtered = filtered.filter(publisher =>
        publisher.name.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }

    setFilteredPublishers(filtered);
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Are you sure you want to delete this publisher?')) {
      try {
        await publisherService.delete(id);
        fetchPublishers();
      } catch (error) {
        console.error('Error deleting publisher:', error);
      }
    }
  };

  const handleEdit = (publisher: Publisher) => {
    setEditingPublisher(publisher);
    setIsEditModalOpen(true);
  };

  const handleAddSubmit = async (formData: any) => {
    setFormLoading(true);
    try {
      await publisherService.create(formData);
      setIsAddModalOpen(false);
      fetchPublishers();
    } catch (error) {
      console.error('Error creating publisher:', error);
      alert('Failed to create publisher. Please try again.');
    } finally {
      setFormLoading(false);
    }
  };

  const handleEditSubmit = async (formData: any) => {
    if (!editingPublisher) return;
    
    setFormLoading(true);
    try {
      await publisherService.update(editingPublisher.id, formData);
      setIsEditModalOpen(false);
      setEditingPublisher(null);
      fetchPublishers();
    } catch (error) {
      console.error('Error updating publisher:', error);
      alert('Failed to update publisher. Please try again.');
    } finally {
      setFormLoading(false);
    }
  };

  if (loading) {
    return (
      <div>
        <div className="page-header">
          <h1 className="page-title">Publishers</h1>
          <p className="page-subtitle">Manage your library's publishers</p>
        </div>
        <div className="page-content">
          <div className="loading">Loading publishers...</div>
        </div>
      </div>
    );
  }

  return (
    <div>
      <div className="page-header">
        <h1 className="page-title">Publishers</h1>
        <p className="page-subtitle">Manage your library's publishers</p>
      </div>

      <div className="page-content">
        {/* Search and Filters */}
        <div className="search-filters">
          <input
            type="text"
            placeholder="Search publishers by name..."
            className="search-input"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>

        {/* Publishers Table */}
        <div className="data-table">
          <div className="table-header">
            <h3 className="table-title">Publishers ({filteredPublishers.length})</h3>
            <button
              className="btn btn-primary"
              onClick={() => setIsAddModalOpen(true)}
            >
              🏢 Add Publisher
            </button>
          </div>
          <div className="table-container">
            {filteredPublishers.length > 0 ? (
              <table>
                <thead>
                  <tr>
                    <th>Name</th>
                    <th>Actions</th>
                  </tr>
                </thead>
                <tbody>
                  {filteredPublishers.map((publisher) => (
                    <tr key={publisher.id}>
                      <td>
                        <strong>{publisher.name}</strong>
                      </td>
                      <td>
                        <div className="action-buttons">
                          <button
                            className="btn btn-secondary btn-small"
                            onClick={() => handleEdit(publisher)}
                          >
                            ✏️ Edit
                          </button>
                          <button
                            className="btn btn-danger btn-small"
                            onClick={() => handleDelete(publisher.id)}
                          >
                            🗑️ Delete
                          </button>
                        </div>
                      </td>
                    </tr>
                  ))}
                </tbody>
              </table>
            ) : (
              <div className="empty-state">
                <div className="empty-state-icon">🏢</div>
                <p>No publishers found</p>
                <button
                  className="btn btn-primary"
                  onClick={() => setIsAddModalOpen(true)}
                  style={{ marginTop: '16px' }}
                >
                  Add Your First Publisher
                </button>
              </div>
            )}
          </div>
        </div>
      </div>

      {/* Floating Add Button */}
      <button
        className="floating-add-btn"
        onClick={() => setIsAddModalOpen(true)}
        title="Add New Publisher"
      >
        +
      </button>

      {/* Add Publisher Modal */}
      <Modal
        isOpen={isAddModalOpen}
        onClose={() => setIsAddModalOpen(false)}
        title="Add New Publisher"
      >
        <PublisherForm
          onSubmit={handleAddSubmit}
          onCancel={() => setIsAddModalOpen(false)}
          loading={formLoading}
        />
      </Modal>

      {/* Edit Publisher Modal */}
      <Modal
        isOpen={isEditModalOpen}
        onClose={() => {
          setIsEditModalOpen(false);
          setEditingPublisher(null);
        }}
        title="Edit Publisher"
      >
        {editingPublisher && (
          <PublisherForm
            publisher={editingPublisher}
            onSubmit={handleEditSubmit}
            onCancel={() => {
              setIsEditModalOpen(false);
              setEditingPublisher(null);
            }}
            loading={formLoading}
          />
        )}
      </Modal>
    </div>
  );
};

export default PublisherList; 