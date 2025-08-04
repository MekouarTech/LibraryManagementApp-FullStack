import React, { useState, useEffect } from 'react';
import { authorService } from '../services/api';
import { Author } from '../types';
import Modal from './Modal';
import AuthorForm from './forms/AuthorForm';

const AuthorList: React.FC = () => {
  const [authors, setAuthors] = useState<Author[]>([]);
  const [filteredAuthors, setFilteredAuthors] = useState<Author[]>([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const [isAddModalOpen, setIsAddModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [editingAuthor, setEditingAuthor] = useState<Author | null>(null);
  const [formLoading, setFormLoading] = useState(false);

  useEffect(() => {
    fetchAuthors();
  }, []);

  useEffect(() => {
    filterAuthors();
  }, [authors, searchTerm]);

  const fetchAuthors = async () => {
    try {
      const data = await authorService.getAll();
      setAuthors(data);
    } catch (error) {
      console.error('Error fetching authors:', error);
    } finally {
      setLoading(false);
    }
  };

  const filterAuthors = () => {
    let filtered = authors;

    if (searchTerm) {
      filtered = filtered.filter(author =>
        author.firstName.toLowerCase().includes(searchTerm.toLowerCase()) ||
        author.lastName.toLowerCase().includes(searchTerm.toLowerCase()) ||
        author.biography.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }

    setFilteredAuthors(filtered);
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Are you sure you want to delete this author?')) {
      try {
        await authorService.delete(id);
        fetchAuthors();
      } catch (error) {
        console.error('Error deleting author:', error);
      }
    }
  };

  const handleEdit = (author: Author) => {
    setEditingAuthor(author);
    setIsEditModalOpen(true);
  };

  const handleAddSubmit = async (formData: any) => {
    setFormLoading(true);
    try {
      await authorService.create(formData);
      setIsAddModalOpen(false);
      fetchAuthors();
    } catch (error) {
      console.error('Error creating author:', error);
      alert('Failed to create author. Please try again.');
    } finally {
      setFormLoading(false);
    }
  };

  const handleEditSubmit = async (formData: any) => {
    if (!editingAuthor) return;
    
    setFormLoading(true);
    try {
      await authorService.update(editingAuthor.id, formData);
      setIsEditModalOpen(false);
      setEditingAuthor(null);
      fetchAuthors();
    } catch (error) {
      console.error('Error updating author:', error);
      alert('Failed to update author. Please try again.');
    } finally {
      setFormLoading(false);
    }
  };

  const formatDate = (dateString: string) => {
    return new Date(dateString).toLocaleDateString();
  };

  if (loading) {
    return (
      <div>
        <div className="page-header">
          <h1 className="page-title">Authors</h1>
          <p className="page-subtitle">Manage your library's author collection</p>
        </div>
        <div className="page-content">
          <div className="loading">Loading authors...</div>
        </div>
      </div>
    );
  }

  return (
    <div>
      <div className="page-header">
        <h1 className="page-title">Authors</h1>
        <p className="page-subtitle">Manage your library's author collection</p>
      </div>

      <div className="page-content">
        {/* Search and Filters */}
        <div className="search-filters">
          <input
            type="text"
            placeholder="Search authors by name or biography..."
            className="search-input"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>

        {/* Authors Table */}
        <div className="data-table">
          <div className="table-header">
            <h3 className="table-title">Authors ({filteredAuthors.length})</h3>
            <button
              className="btn btn-primary"
              onClick={() => setIsAddModalOpen(true)}
            >
              Add Author
            </button>
          </div>
          <div className="table-container">
            {filteredAuthors.length > 0 ? (
              <table>
                <thead>
                  <tr>
                    <th>Name</th>
                    <th>Date of Birth</th>
                    <th>Biography</th>
                    <th>Actions</th>
                  </tr>
                </thead>
                <tbody>
                  {filteredAuthors.map((author) => (
                    <tr key={author.id}>
                      <td>
                        <strong>{author.firstName} {author.lastName}</strong>
                      </td>
                      <td>{formatDate(author.dateOfBirth)}</td>
                      <td>
                        {author.biography.length > 100 
                          ? `${author.biography.substring(0, 100)}...` 
                          : author.biography
                        }
                      </td>
                      <td>
                        <div className="action-buttons">
                          <button
                            className="btn btn-secondary btn-small"
                            onClick={() => handleEdit(author)}
                          >
                            ✏️ Edit
                          </button>
                          <button
                            className="btn btn-danger btn-small"
                            onClick={() => handleDelete(author.id)}
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
                <div className="empty-state-icon">✍️</div>
                <p>No authors found</p>
                <button
                  className="btn btn-primary"
                  onClick={() => setIsAddModalOpen(true)}
                  style={{ marginTop: '16px' }}
                >
                  Add Your First Author
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
        title="Add New Author"
      >
        +
      </button>

      {/* Add Author Modal */}
      <Modal
        isOpen={isAddModalOpen}
        onClose={() => setIsAddModalOpen(false)}
        title="Add New Author"
      >
        <AuthorForm
          onSubmit={handleAddSubmit}
          onCancel={() => setIsAddModalOpen(false)}
          loading={formLoading}
        />
      </Modal>

      {/* Edit Author Modal */}
      <Modal
        isOpen={isEditModalOpen}
        onClose={() => {
          setIsEditModalOpen(false);
          setEditingAuthor(null);
        }}
        title="Edit Author"
      >
        {editingAuthor && (
          <AuthorForm
            author={editingAuthor}
            onSubmit={handleEditSubmit}
            onCancel={() => {
              setIsEditModalOpen(false);
              setEditingAuthor(null);
            }}
            loading={formLoading}
          />
        )}
      </Modal>
    </div>
  );
};

export default AuthorList; 