import React, { useState, useEffect } from 'react';
import { categoryService } from '../services/api';
import { Category } from '../types';
import Modal from './Modal';
import CategoryForm from './forms/CategoryForm';

const CategoryList: React.FC = () => {
  const [categories, setCategories] = useState<Category[]>([]);
  const [filteredCategories, setFilteredCategories] = useState<Category[]>([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const [isAddModalOpen, setIsAddModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [editingCategory, setEditingCategory] = useState<Category | null>(null);
  const [formLoading, setFormLoading] = useState(false);

  useEffect(() => {
    fetchCategories();
  }, []);

  useEffect(() => {
    filterCategories();
  }, [categories, searchTerm]);

  const fetchCategories = async () => {
    try {
      const data = await categoryService.getAll();
      setCategories(data);
    } catch (error) {
      console.error('Error fetching categories:', error);
    } finally {
      setLoading(false);
    }
  };

  const filterCategories = () => {
    let filtered = categories;

    if (searchTerm) {
      filtered = filtered.filter(category =>
        category.name.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }

    setFilteredCategories(filtered);
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Are you sure you want to delete this category?')) {
      try {
        await categoryService.delete(id);
        fetchCategories();
      } catch (error) {
        console.error('Error deleting category:', error);
      }
    }
  };

  const handleEdit = (category: Category) => {
    setEditingCategory(category);
    setIsEditModalOpen(true);
  };

  const handleAddSubmit = async (formData: any) => {
    setFormLoading(true);
    try {
      await categoryService.create(formData);
      setIsAddModalOpen(false);
      fetchCategories();
    } catch (error) {
      console.error('Error creating category:', error);
      alert('Failed to create category. Please try again.');
    } finally {
      setFormLoading(false);
    }
  };

  const handleEditSubmit = async (formData: any) => {
    if (!editingCategory) return;
    
    setFormLoading(true);
    try {
      await categoryService.update(editingCategory.id, formData);
      setIsEditModalOpen(false);
      setEditingCategory(null);
      fetchCategories();
    } catch (error) {
      console.error('Error updating category:', error);
      alert('Failed to update category. Please try again.');
    } finally {
      setFormLoading(false);
    }
  };

  if (loading) {
    return (
      <div>
        <div className="page-header">
          <h1 className="page-title">Categories</h1>
          <p className="page-subtitle">Manage your library's book categories</p>
        </div>
        <div className="page-content">
          <div className="loading">Loading categories...</div>
        </div>
      </div>
    );
  }

  return (
    <div>
      <div className="page-header">
        <h1 className="page-title">Categories</h1>
        <p className="page-subtitle">Manage your library's book categories</p>
      </div>

      <div className="page-content">
        {/* Search and Filters */}
        <div className="search-filters">
          <input
            type="text"
            placeholder="Search categories by name..."
            className="search-input"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
        </div>

        {/* Categories Table */}
        <div className="data-table">
          <div className="table-header">
            <h3 className="table-title">Categories ({filteredCategories.length})</h3>
            <button
              className="btn btn-primary"
              onClick={() => setIsAddModalOpen(true)}
            >
              Add Category
            </button>
          </div>
          <div className="table-container">
            {filteredCategories.length > 0 ? (
              <table>
                <thead>
                  <tr>
                    <th>Name</th>
                    <th>Actions</th>
                  </tr>
                </thead>
                <tbody>
                  {filteredCategories.map((category) => (
                    <tr key={category.id}>
                      <td>
                        <strong>{category.name}</strong>
                      </td>
                      <td>
                        <div className="action-buttons">
                          <button
                            className="btn btn-secondary btn-small"
                            onClick={() => handleEdit(category)}
                          >
                            ✏️ Edit
                          </button>
                          <button
                            className="btn btn-danger btn-small"
                            onClick={() => handleDelete(category.id)}
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
                <div className="empty-state-icon">🏷️</div>
                <p>No categories found</p>
                <button
                  className="btn btn-primary"
                  onClick={() => setIsAddModalOpen(true)}
                  style={{ marginTop: '16px' }}
                >
                  Add Your First Category
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
        title="Add New Category"
      >
        +
      </button>

      {/* Add Category Modal */}
      <Modal
        isOpen={isAddModalOpen}
        onClose={() => setIsAddModalOpen(false)}
        title="Add New Category"
      >
        <CategoryForm
          onSubmit={handleAddSubmit}
          onCancel={() => setIsAddModalOpen(false)}
          loading={formLoading}
        />
      </Modal>

      {/* Edit Category Modal */}
      <Modal
        isOpen={isEditModalOpen}
        onClose={() => {
          setIsEditModalOpen(false);
          setEditingCategory(null);
        }}
        title="Edit Category"
      >
        {editingCategory && (
          <CategoryForm
            category={editingCategory}
            onSubmit={handleEditSubmit}
            onCancel={() => {
              setIsEditModalOpen(false);
              setEditingCategory(null);
            }}
            loading={formLoading}
          />
        )}
      </Modal>
    </div>
  );
};

export default CategoryList; 