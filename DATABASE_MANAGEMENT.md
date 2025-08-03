# Database Management Guide

This guide explains how to manage the database in the Library Management App Docker environment.

## Overview

The application uses SQL Server in a Docker container with automatic database initialization, migrations, and seeding.

## Database Initialization Process

### Automatic Startup Process

When the application starts, it follows this sequence:

1. **Health Check**: Waits for SQL Server to be healthy
2. **Connection Test**: Checks if the database can be connected to
3. **Database Creation**: Creates the database if it doesn't exist
4. **Migration Application**: Applies all pending EF Core migrations
5. **Data Seeding**: Populates the database with initial data

### Key Files

- `DatabaseInitializer.cs`: Handles database initialization logic
- `DatabaseSeeder.cs`: Contains seeding logic for initial data
- `Program.cs`: Orchestrates the startup process

## Common Issues and Solutions

### Issue: "Database 'LibraryManagementDb' already exists"

**Cause**: The database already exists but EF Core is trying to create it again.

**Solution**: The updated `DatabaseInitializer` now properly checks if the database exists before attempting to create it.

### Issue: Migration conflicts

**Cause**: Database schema is out of sync with migrations.

**Solution**: Use the database reset functionality (see below).

## Database Management Commands

### Check Database Status

```bash
# Via API
curl http://localhost:5000/api/database/status

# Via Docker logs
docker-compose logs api
```

### Reset Database (Complete Reset)

#### Option 1: Using PowerShell Script (Windows)

```powershell
.\scripts\reset-database.ps1
```

#### Option 2: Using Bash Script (Linux/Mac)

```bash
chmod +x scripts/reset-database.sh
./scripts/reset-database.sh
```

#### Option 3: Manual Commands

```bash
# Stop containers
docker-compose down

# Remove the SQL Server data volume
docker volume rm librarymanagementapp_sqlserver_data

# Start containers
docker-compose up -d
```

### Reset Database via API

```bash
curl -X POST http://localhost:5000/api/database/reset
```

**Warning**: This will delete all data and recreate the database with seeded data.

### View Database Logs

```bash
# View API logs
docker-compose logs -f api

# View SQL Server logs
docker-compose logs -f sqlserver
```

## Best Practices

### 1. Development Environment

- Use the automatic database initialization for development
- Reset the database when schema changes are made
- Monitor logs during startup to ensure proper initialization

### 2. Production Environment

- **Never** use automatic database creation in production
- Apply migrations manually using `dotnet ef database update`
- Use proper backup and restore procedures
- Disable the database reset API endpoints

### 3. Migration Management

- Always create migrations for schema changes: `dotnet ef migrations add MigrationName`
- Test migrations in development before applying to production
- Use `dotnet ef migrations script` to generate SQL scripts for manual review

### 4. Seeding Strategy

- Use `HasData` for static reference data
- Use `DatabaseSeeder` for complex relationships and dynamic data
- Make seeding idempotent (safe to run multiple times)
- Consider environment-specific seeding

## Troubleshooting

### Database Connection Issues

1. Check if SQL Server container is running:
   ```bash
   docker-compose ps
   ```

2. Check SQL Server logs:
   ```bash
   docker-compose logs sqlserver
   ```

3. Verify connection string in `docker-compose.yml`

### Migration Issues

1. Check pending migrations:
   ```bash
   curl http://localhost:5000/api/database/status
   ```

2. Reset database if migrations are corrupted:
   ```bash
   ./scripts/reset-database.sh
   ```

### Performance Issues

1. Monitor database performance:
   ```bash
   docker stats
   ```

2. Check for connection pool exhaustion in logs

## Environment Variables

Key environment variables for database configuration:

```yaml
# docker-compose.yml
environment:
  - ConnectionStrings__DefaultConnection=Server=sqlserver,1433;Database=LibraryManagementDb;User Id=sa;Password=Hamza@1234;TrustServerCertificate=true;
```

## Security Considerations

1. **Never** use the default SA password in production
2. **Never** expose database ports in production
3. Use proper authentication and authorization
4. Regularly update SQL Server container
5. Use secrets management for sensitive data

## Monitoring

### Health Checks

The application includes health checks for:
- SQL Server connectivity
- Database migrations status
- API endpoints availability

### Logging

Key log messages to monitor:
- Database connection status
- Migration application progress
- Seeding completion
- Error messages during initialization

## Support

If you encounter issues:

1. Check the logs: `docker-compose logs -f api`
2. Verify database status: `curl http://localhost:5000/api/database/status`
3. Reset database if needed: `./scripts/reset-database.sh`
4. Check this documentation for common solutions 