# Library Management Application

A clean architecture application built with .NET 9 and TypeScript React for managing books, authors, categories, and publishers in a library.

## 🏗️ Architecture

This application follows Clean Architecture principles with Domain-Driven Design (DDD):

- **Domain Layer**: Contains entities, domain events, and repository interfaces
- **Application Layer**: Contains CQRS commands/queries, DTOs, and business logic
- **Infrastructure Layer**: Contains Entity Framework implementation and external services
- **API Layer**: Contains REST API controllers and configuration

## 🚀 Features

- **CQRS Pattern**: Separate Commands (Create/Update/Delete) and Queries (Read)
- **MediatR**: For handling commands and queries
- **Entity Framework Core**: For data persistence with SQL Server
- **FluentValidation**: For input validation
- **Domain Events**: For handling domain events like BookCreated
- **Swagger/OpenAPI**: For API documentation
- **Docker Compose**: For easy deployment with SQL Server
- **Unit Tests**: For testing business logic

## 📋 Prerequisites

- .NET 9 SDK
- Docker Desktop
- SQL Server (via Docker)

## 🛠️ Setup Instructions

### 1. Clone and Navigate
```bash
cd LibraryManagementApp
```

### 2. Run Backend with Docker Compose
```bash
docker-compose up -d
```

This will start:
- SQL Server on port 1433
- API on ports 5000 (HTTP) and 5001 (HTTPS)

### 3. Run Frontend (React)
```bash
cd frontend
npm start
```

This will start the React frontend on http://localhost:3000

### 4. Run Database Migrations
```bash
cd src/LibraryManagementApp.API
dotnet ef database update
```

### 5. Run the Application Locally (Alternative)
```bash
# Restore packages
dotnet restore

# Build the solution
dotnet build

# Run the API
cd src/LibraryManagementApp.API
dotnet run
```

### 6. Run Tests
```bash
dotnet test
```

## 📚 API Endpoints

### Books
- `GET /api/books` - Get all books
- `POST /api/books` - Create a new book

### Authors
- `GET /api/authors` - Get all authors
- `POST /api/authors` - Create a new author

## 🎨 Frontend Features

- **Modern UI**: Clean and responsive design with gradient backgrounds
- **Book Management**: View all books with details including authors and categories
- **Author Management**: View all authors with biographies
- **Add Books**: Form to create new books with multiple authors and categories
- **Add Authors**: Form to create new authors
- **Responsive Design**: Works on desktop and mobile devices

## 🔧 Configuration

The application uses the following configuration:

- **Database**: SQL Server with connection string in `appsettings.json`
- **CORS**: Configured to allow all origins in development
- **Swagger**: Available at `/swagger` in development mode

## 🏗️ Project Structure

```
LibraryManagementApp/
├── src/
│   ├── LibraryManagementApp.Domain/          # Domain entities and interfaces
│   ├── LibraryManagementApp.Application/     # CQRS commands/queries and DTOs
│   ├── LibraryManagementApp.Infrastructure/  # EF Core and repository implementations
│   ├── LibraryManagementApp.API/             # REST API controllers
│   └── tests/
│       └── LibraryManagementApp.Tests/       # Unit tests
├── docker-compose.yml                        # Docker services configuration
└── README.md                                 # This file
```

## 🧪 Testing

Run the unit tests:
```bash
dotnet test
```

## 🐳 Docker

The application includes Docker support with:
- Multi-stage Dockerfile for the API
- Docker Compose for SQL Server and API services
- Volume persistence for SQL Server data

## 📖 API Documentation

Once the application is running, visit:
- Frontend: `http://localhost:3000`
- Swagger UI: `https://localhost:5001/swagger`
- API Base URL: `https://localhost:5001/api`

## 🔄 Database Migrations

To create a new migration:
```bash
cd src/LibraryManagementApp.API
dotnet ef migrations add MigrationName
dotnet ef database update
```

## 🚀 Next Steps

1. Add more CRUD operations for all entities
2. Implement authentication and authorization
3. Add the React frontend
4. Add integration tests
5. Implement caching
6. Add logging and monitoring

## 📝 License

This project is for educational purposes. 