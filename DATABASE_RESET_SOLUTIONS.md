# Database Reset Solutions for Library Management App

This document provides multiple solutions for resetting the database in your development environment to resolve schema conflicts and table existence issues.

## 🚨 **Problem: "There is already an object named 'Authors' in the database"**

This error occurs when:
- Database tables exist but migration history is missing
- Schema mismatch between existing tables and current migrations
- Previous database state conflicts with new migrations

## 🔧 **Solution 1: Complete Docker Reset (Recommended)**

### **PowerShell Script (Windows)**
```powershell
# Run the complete reset script
.\scripts\reset-database-complete.ps1
```

### **Manual Commands**
```bash
# Stop containers and remove volumes
docker-compose down -v

# Clean up any remaining volumes
docker volume prune -f

# Remove any existing containers
docker container prune -f

# Clean up dangling images (optional)
docker image prune -f

# Rebuild the API image
docker-compose build api

# Start containers fresh
docker-compose up -d
```

## 🔧 **Solution 2: Enhanced DatabaseInitializer (Automatic)**

The application now includes an enhanced `DatabaseInitializer` that automatically detects schema conflicts and performs a force reset when needed.

### **Features:**
- ✅ **Automatic Detection**: Detects when tables exist but migrations aren't recorded
- ✅ **Force Reset**: Automatically drops and recreates the database
- ✅ **Safe Operation**: Only triggers in development scenarios
- ✅ **Comprehensive Logging**: Detailed logs for troubleshooting

### **How It Works:**
1. **Connection Check**: Verifies database connectivity
2. **Migration Analysis**: Checks pending and applied migrations
3. **Conflict Detection**: Identifies schema mismatches
4. **Automatic Reset**: Performs force reset if conflicts detected
5. **Seeding**: Populates with fresh data

## 🔧 **Solution 3: API Endpoints for Database Management**

### **Check Database Status**
```bash
curl http://localhost:5000/api/database/status
```

**Response:**
```json
{
  "canConnect": true,
  "pendingMigrations": [],
  "appliedMigrations": ["20250803170558_InitialCreate"],
  "totalAppliedMigrations": 1
}
```

### **Reset Database (Safe)**
```bash
curl -X POST http://localhost:5000/api/database/reset
```

### **Force Reset Database (Development Only)**
```bash
curl -X POST http://localhost:5000/api/database/force-reset
```

## 🔧 **Solution 4: Manual SQL Commands**

### **Connect to SQL Server Container**
```bash
docker exec -it librarymanagementapp-sqlserver-1 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P Hamza@1234 -C
```

### **Drop and Recreate Database**
```sql
-- Drop the database
USE master;
GO
DROP DATABASE IF EXISTS LibraryManagementDb;
GO

-- Create fresh database
CREATE DATABASE LibraryManagementDb;
GO
```

### **Exit SQL Server**
```sql
EXIT
```

## 🔧 **Solution 5: EF Core Commands**

### **Remove All Migrations**
```bash
# Remove existing migrations
Remove-Item -Recurse -Force src/LibraryManagementApp.Infrastructure/Migrations

# Create fresh migration
dotnet ef migrations add InitialCreate --project src/LibraryManagementApp.Infrastructure --startup-project src/LibraryManagementApp.API
```

### **Update Database**
```bash
dotnet ef database update --project src/LibraryManagementApp.Infrastructure --startup-project src/LibraryManagementApp.API
```

## 🛡️ **Safety Features**

### **Development vs Production**
- **Development**: Automatic reset enabled
- **Production**: Automatic reset disabled
- **Environment Detection**: Uses `ASPNETCORE_ENVIRONMENT`

### **Backup Before Reset**
```bash
# Create backup before reset
docker exec librarymanagementapp-sqlserver-1 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P Hamza@1234 -C -Q "BACKUP DATABASE LibraryManagementDb TO DISK = '/var/opt/mssql/backup.bak'"
```

## 📊 **Monitoring and Troubleshooting**

### **Check Container Logs**
```bash
# API logs
docker-compose logs -f api

# SQL Server logs
docker-compose logs -f sqlserver
```

### **Database Health Check**
```bash
# Check if database is accessible
docker exec librarymanagementapp-sqlserver-1 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P Hamza@1234 -C -Q "SELECT 1"
```

### **Migration Status**
```bash
# Check migration history
docker exec librarymanagementapp-sqlserver-1 /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P Hamza@1234 -C -Q "SELECT * FROM LibraryManagementDb.dbo.__EFMigrationsHistory"
```

## 🚀 **Quick Start Commands**

### **For Immediate Reset**
```powershell
# Windows
.\scripts\reset-database-complete.ps1

# Linux/Mac
chmod +x scripts/reset-database-complete.sh
./scripts/reset-database-complete.sh
```

### **For API-Based Reset**
```bash
# Check status first
curl http://localhost:5000/api/database/status

# Force reset if needed
curl -X POST http://localhost:5000/api/database/force-reset
```

## 🔍 **Verification Steps**

After any reset operation, verify:

1. **API Accessibility**: `http://localhost:5000/swagger`
2. **Database Status**: `http://localhost:5000/api/database/status`
3. **Sample Data**: Check if books, authors, categories, publishers are populated
4. **Migration History**: Verify `__EFMigrationsHistory` table has records

## ⚠️ **Important Notes**

- **Data Loss**: All reset operations will delete existing data
- **Development Only**: Force reset should only be used in development
- **Backup**: Always backup important data before reset operations
- **Environment**: Ensure you're in the correct environment (Development vs Production)

## 🆘 **Troubleshooting**

### **Container Won't Start**
```bash
# Check container status
docker-compose ps

# Check logs
docker-compose logs

# Restart containers
docker-compose restart
```

### **Port Conflicts**
```bash
# Check port usage
netstat -ano | findstr :5000
netstat -ano | findstr :1434

# Kill conflicting processes
taskkill /PID <PID> /F
```

### **Volume Issues**
```bash
# List volumes
docker volume ls

# Remove specific volume
docker volume rm librarymanagementapp_sqlserver_data

# Clean up all unused volumes
docker volume prune
```

## 📞 **Support**

If you encounter issues:
1. Check the logs: `docker-compose logs -f api`
2. Verify database status: `curl http://localhost:5000/api/database/status`
3. Use the complete reset script: `.\scripts\reset-database-complete.ps1`
4. Check this documentation for specific solutions 