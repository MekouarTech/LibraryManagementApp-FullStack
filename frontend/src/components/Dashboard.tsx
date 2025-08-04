import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { bookService, authorService, categoryService, publisherService } from '../services/api';
import { Book, Author, Category, Publisher } from '../types';

const Dashboard: React.FC = () => {
  const [stats, setStats] = useState({
    books: 0,
    authors: 0,
    categories: 0,
    publishers: 0
  });
  const [loading, setLoading] = useState(true);
  const [recentBooks, setRecentBooks] = useState<Book[]>([]);

  useEffect(() => {
    const fetchStats = async () => {
      try {
        const [books, authors, categories, publishers] = await Promise.all([
          bookService.getAll(),
          authorService.getAll(),
          categoryService.getAll(),
          publisherService.getAll()
        ]);

        setStats({
          books: books.length,
          authors: authors.length,
          categories: categories.length,
          publishers: publishers.length
        });

        // Get recent books (last 5)
        setRecentBooks(books.slice(0, 5));
      } catch (error) {
        console.error('Error fetching dashboard data:', error);
      } finally {
        setLoading(false);
      }
    };

    fetchStats();
  }, []);

  if (loading) {
    return (
      <div>
        <div className="page-header">
          <h1 className="page-title">Dashboard</h1>
          <p className="page-subtitle">library management system</p>
        </div>
        <div className="page-content">
          <div className="loading">Loading dashboard...</div>
        </div>
      </div>
    );
  }

  return (
    <div>
      <div className="page-header">
        <h1 className="page-title">Dashboard</h1>
        <p className="page-subtitle">library management system</p>
      </div>
      
      <div className="page-content">
        {/* Statistics Cards */}
        <div className="dashboard-stats">
          <div className="stat-card">
            <div className="stat-number">{stats.books}</div>
            <div className="stat-label">Total Books</div>
          </div>
          <div className="stat-card">
            <div className="stat-number">{stats.authors}</div>
            <div className="stat-label">Total Authors</div>
          </div>
          <div className="stat-card">
            <div className="stat-number">{stats.categories}</div>
            <div className="stat-label">Total Categories</div>
          </div>
          <div className="stat-card">
            <div className="stat-number">{stats.publishers}</div>
            <div className="stat-label">Total Publishers</div>
          </div>
        </div>

        {/* Recent Books */}
        <div className="data-table">
          <div className="table-header">
            <h3 className="table-title">Recent Books</h3>
            <Link to="/books" className="btn btn-secondary">
              View All Books
            </Link>
          </div>
          <div className="table-container">
            {recentBooks.length > 0 ? (
              <table>
                <thead>
                  <tr>
                    <th>Title</th>
                    <th>Publication Year</th>
                    <th>Publisher</th>
                    <th>Copies</th>
                  </tr>
                </thead>
                <tbody>
                  {recentBooks.map((book) => (
                    <tr key={book.id}>
                      <td>{book.title}</td>
                      <td>{book.publicationYear}</td>
                      <td>{book.publisherName || 'N/A'}</td>
                      <td>{book.numberOfCopies}</td>
                    </tr>
                  ))}
                </tbody>
              </table>
            ) : (
              <div className="empty-state">
                <div className="empty-state-icon">📚</div>
                <p>No books found</p>
              </div>
            )}
          </div>
        </div>

        {/* Quick Actions */}
        <div className="data-table" style={{ marginTop: '24px' }}>
          <div className="table-header">
            <h3 className="table-title">Quick Actions</h3>
          </div>
          <div style={{ padding: '24px' }}>
            <div style={{ display: 'grid', gridTemplateColumns: 'repeat(auto-fit, minmax(200px, 1fr))', gap: '16px' }}>
              <Link to="/books" className="btn btn-primary" style={{ justifyContent: 'center' }}>
                📚 Manage Books
              </Link>
              <Link to="/authors" className="btn btn-primary" style={{ justifyContent: 'center' }}>
                ✍️ Manage Authors
              </Link>
              <Link to="/categories" className="btn btn-primary" style={{ justifyContent: 'center' }}>
                🏷️ Manage Categories
              </Link>
              <Link to="/publishers" className="btn btn-primary" style={{ justifyContent: 'center' }}>
                🏢 Manage Publishers
              </Link>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default Dashboard; 