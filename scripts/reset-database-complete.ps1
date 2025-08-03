# Complete Database Reset Script for Library Management App
# This script performs a complete reset of the database environment

Write-Host "=== Library Management App - Complete Database Reset ===" -ForegroundColor Yellow
Write-Host ""

# Stop all containers and remove volumes
Write-Host "Stopping containers and removing volumes..." -ForegroundColor Green
docker-compose down -v

# Remove any remaining volumes (in case they weren't removed by down -v)
Write-Host "Cleaning up any remaining volumes..." -ForegroundColor Green
docker volume prune -f

# Remove any existing containers
Write-Host "Removing any existing containers..." -ForegroundColor Green
docker container prune -f

# Clean up any dangling images (optional)
Write-Host "Cleaning up dangling images..." -ForegroundColor Green
docker image prune -f

# Rebuild the API image to ensure latest code
Write-Host "Rebuilding API image..." -ForegroundColor Green
docker-compose build api

# Start containers fresh
Write-Host "Starting containers with fresh database..." -ForegroundColor Green
docker-compose up -d

Write-Host ""
Write-Host "=== Reset Complete! ===" -ForegroundColor Green
Write-Host ""
Write-Host "The application will now:"
Write-Host "1. Create a fresh SQL Server instance" -ForegroundColor Cyan
Write-Host "2. Create the database from scratch" -ForegroundColor Cyan
Write-Host "3. Apply all migrations" -ForegroundColor Cyan
Write-Host "4. Seed with initial data" -ForegroundColor Cyan
Write-Host ""
Write-Host "Monitor the startup process:" -ForegroundColor White
Write-Host "  docker-compose logs -f api" -ForegroundColor Gray
Write-Host ""
Write-Host "Check database status:" -ForegroundColor White
Write-Host "  http://localhost:5000/api/database/status" -ForegroundColor Gray
Write-Host ""
Write-Host "Access Swagger UI:" -ForegroundColor White
Write-Host "  http://localhost:5000/swagger" -ForegroundColor Gray 