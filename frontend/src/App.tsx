import React from 'react';
import { BrowserRouter as Router, Routes, Route, Link, useLocation } from 'react-router-dom';
import './App.css';
import BookList from './components/BookList';
import AuthorList from './components/AuthorList';
import CategoryList from './components/CategoryList';
import PublisherList from './components/PublisherList';
import Dashboard from './components/Dashboard';

const Sidebar = () => {
  const location = useLocation();
  
  const navItems = [
    { path: '/', label: 'Dashboard', icon: '📊' },
    { path: '/books', label: 'Books', icon: '📚' },
    { path: '/authors', label: 'Authors', icon: '✍️' },
    { path: '/categories', label: 'Categories', icon: '🏷️' },
    { path: '/publishers', label: 'Publishers', icon: '🏢' }
  ];

  return (
    <div className="sidebar">
      <div className="sidebar-header">
        <h2>Library Management</h2>
      </div>
      <nav className="sidebar-nav">
        {navItems.map((item) => (
          <Link
            key={item.path}
            to={item.path}
            className={`nav-item ${location.pathname === item.path ? 'active' : ''}`}
          >
            <span className="nav-icon">{item.icon}</span>
            <span className="nav-label">{item.label}</span>
          </Link>
        ))}
      </nav>
    </div>
  );
};

const MainContent = () => {
  return (
    <div className="main-content">
      <Routes>
        <Route path="/" element={<Dashboard />} />
        <Route path="/books" element={<BookList />} />
        <Route path="/authors" element={<AuthorList />} />
        <Route path="/categories" element={<CategoryList />} />
        <Route path="/publishers" element={<PublisherList />} />
      </Routes>
    </div>
  );
};

function App() {
  return (
    <Router>
      <div className="app">
        <Sidebar />
        <MainContent />
      </div>
    </Router>
  );
}

export default App;
