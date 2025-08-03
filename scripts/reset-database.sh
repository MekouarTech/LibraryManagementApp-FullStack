#!/bin/bash

# Database Reset Script for Library Management App
# This script safely removes the SQL Server data volume to reset the database

echo "=== Library Management App - Database Reset Script ==="
echo ""

# Stop containers
echo "Stopping containers..."
docker-compose down

# Remove the SQL Server data volume
echo "Removing SQL Server data volume..."
docker volume rm librarymanagementapp_sqlserver_data

if [ $? -eq 0 ]; then
    echo "SQL Server data volume removed successfully."
else
    echo "Warning: Could not remove volume (it might not exist)."
fi

# Start containers
echo "Starting containers..."
docker-compose up -d

echo ""
echo "Database reset completed!"
echo "The application will now create a fresh database with seeded data."
echo ""
echo "You can monitor the startup process with:"
echo "  docker-compose logs -f api"
echo ""
echo "Or check the database status at:"
echo "  http://localhost:5000/api/database/status" 