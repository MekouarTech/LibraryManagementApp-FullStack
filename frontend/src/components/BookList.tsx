import React, { useState, useEffect } from 'react';
import { bookService } from '../services/api';
import { Book } from '../types';
import Modal from './Modal';
import BookForm from './forms/BookForm';

const BookList: React.FC = () => {
  const [books, setBooks] = useState<Book[]>([]);
  const [filteredBooks, setFilteredBooks] = useState<Book[]>([]);
  const [loading, setLoading] = useState(true);
  const [searchTerm, setSearchTerm] = useState('');
  const [selectedYear, setSelectedYear] = useState('');
  const [isAddModalOpen, setIsAddModalOpen] = useState(false);
  const [isEditModalOpen, setIsEditModalOpen] = useState(false);
  const [editingBook, setEditingBook] = useState<Book | null>(null);
  const [formLoading, setFormLoading] = useState(false);

  useEffect(() => {
    fetchBooks();
  }, []);

  useEffect(() => {
    filterBooks();
  }, [books, searchTerm, selectedYear]);

  const fetchBooks = async () => {
    try {
      const data = await bookService.getAll();
      setBooks(data);
    } catch (error) {
      console.error('Error fetching books:', error);
    } finally {
      setLoading(false);
    }
  };

  const filterBooks = () => {
    let filtered = books;

    if (searchTerm) {
      filtered = filtered.filter(book =>
        book.title.toLowerCase().includes(searchTerm.toLowerCase()) ||
        book.publisherName.toLowerCase().includes(searchTerm.toLowerCase())
      );
    }

    if (selectedYear) {
      filtered = filtered.filter(book => book.publicationYear.toString() === selectedYear);
    }

    setFilteredBooks(filtered);
  };

  const handleDelete = async (id: number) => {
    if (window.confirm('Are you sure you want to delete this book?')) {
      try {
        await bookService.delete(id);
        fetchBooks();
      } catch (error) {
        console.error('Error deleting book:', error);
      }
    }
  };

  const handleEdit = (book: Book) => {
    setEditingBook(book);
    setIsEditModalOpen(true);
  };

  const handleAddSubmit = async (formData: any) => {
    setFormLoading(true);
    try {
      await bookService.create(formData);
      setIsAddModalOpen(false);
      fetchBooks();
    } catch (error) {
      console.error('Error creating book:', error);
      alert('Failed to create book. Please try again.');
    } finally {
      setFormLoading(false);
    }
  };

  const handleEditSubmit = async (formData: any) => {
    if (!editingBook) return;
    
    setFormLoading(true);
    try {
      await bookService.update(editingBook.id, formData);
      setIsEditModalOpen(false);
      setEditingBook(null);
      fetchBooks();
    } catch (error) {
      console.error('Error updating book:', error);
      alert('Failed to update book. Please try again.');
    } finally {
      setFormLoading(false);
    }
  };

  const years = Array.from(new Set(books.map(book => book.publicationYear))).sort((a, b) => b - a);

  if (loading) {
    return (
      <div>
        <div className="page-header">
          <h1 className="page-title">Books</h1>
          <p className="page-subtitle">Manage your library's book collection</p>
        </div>
        <div className="page-content">
          <div className="loading">Loading books...</div>
        </div>
      </div>
    );
  }

  return (
    <div>
      <div className="page-header">
        <h1 className="page-title">Books</h1>
        <p className="page-subtitle">Manage your library's book collection</p>
      </div>

      <div className="page-content">
        {/* Search and Filters */}
        <div className="search-filters">
          <input
            type="text"
            placeholder="Search books by title or publisher..."
            className="search-input"
            value={searchTerm}
            onChange={(e) => setSearchTerm(e.target.value)}
          />
          <select
            className="filter-select"
            value={selectedYear}
            onChange={(e) => setSelectedYear(e.target.value)}
          >
            <option value="">All Years</option>
            {years.map(year => (
              <option key={year} value={year}>{year}</option>
            ))}
          </select>
        </div>

        {/* Books Table */}
        <div className="data-table">
          <div className="table-header">
            <h3 className="table-title">Books ({filteredBooks.length})</h3>
            <button
              className="btn btn-primary"
              onClick={() => setIsAddModalOpen(true)}
            >
              📚 Add Book
            </button>
          </div>
          <div className="table-container">
            {filteredBooks.length > 0 ? (
              <table>
                <thead>
                  <tr>
                    <th>Title</th>
                    <th>Publication Year</th>
                    <th>Publisher</th>
                    <th>Copies</th>
                    <th>Authors</th>
                    <th>Categories</th>
                    <th>Actions</th>
                  </tr>
                </thead>
                <tbody>
                  {filteredBooks.map((book) => (
                    <tr key={book.id}>
                      <td>{book.title}</td>
                      <td>{book.publicationYear}</td>
                      <td>{book.publisherName}</td>
                      <td>{book.numberOfCopies}</td>
                      <td>
                        {book.authors.map(author => 
                          `${author.firstName} ${author.lastName}`
                        ).join(', ')}
                      </td>
                      <td>
                        {book.categories.map(category => 
                          category.name
                        ).join(', ')}
                      </td>
                      <td>
                        <div className="action-buttons">
                          <button
                            className="btn btn-secondary btn-small"
                            onClick={() => handleEdit(book)}
                          >
                            ✏️ Edit
                          </button>
                          <button
                            className="btn btn-danger btn-small"
                            onClick={() => handleDelete(book.id)}
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
                <div className="empty-state-icon">📚</div>
                <p>No books found</p>
                <button
                  className="btn btn-primary"
                  onClick={() => setIsAddModalOpen(true)}
                  style={{ marginTop: '16px' }}
                >
                  Add Your First Book
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
        title="Add New Book"
      >
        +
      </button>

      {/* Add Book Modal */}
      <Modal
        isOpen={isAddModalOpen}
        onClose={() => setIsAddModalOpen(false)}
        title="Add New Book"
      >
        <BookForm
          onSubmit={handleAddSubmit}
          onCancel={() => setIsAddModalOpen(false)}
          loading={formLoading}
        />
      </Modal>

      {/* Edit Book Modal */}
      <Modal
        isOpen={isEditModalOpen}
        onClose={() => {
          setIsEditModalOpen(false);
          setEditingBook(null);
        }}
        title="Edit Book"
      >
        {editingBook && (
          <BookForm
            book={editingBook}
            onSubmit={handleEditSubmit}
            onCancel={() => {
              setIsEditModalOpen(false);
              setEditingBook(null);
            }}
            loading={formLoading}
          />
        )}
      </Modal>
    </div>
  );
};

export default BookList; 