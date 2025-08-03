# Database Reset Script for Library Management App
# This script safely removes the SQL Server data volume to reset the database

Write-Host "=== Library Management App - Database Reset Script ===" -ForegroundColor Yellow
Write-Host ""

# Stop containers
Write-Host "Stopping containers..." -ForegroundColor Green
docker-compose down

# Remove the SQL Server data volume
Write-Host "Removing SQL Server data volume..." -ForegroundColor Green
docker volume rm librarymanagementapp_sqlserver_data

if ($LASTEXITCODE -eq 0) {
    Write-Host "SQL Server data volume removed successfully." -ForegroundColor Green
} else {
    Write-Host "Warning: Could not remove volume (it might not exist)." -ForegroundColor Yellow
}

# Start containers
Write-Host "Starting containers..." -ForegroundColor Green
docker-compose up -d

Write-Host ""
Write-Host "Database reset completed!" -ForegroundColor Green
Write-Host "The application will now create a fresh database with seeded data." -ForegroundColor Cyan
Write-Host ""
Write-Host "You can monitor the startup process with:" -ForegroundColor White
Write-Host "  docker-compose logs -f api" -ForegroundColor Gray
Write-Host ""
Write-Host "Or check the database status at:" -ForegroundColor White
Write-Host "  http://localhost:5000/api/database/status" -ForegroundColor Gray 