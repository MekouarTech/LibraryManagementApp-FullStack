using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LibraryManagementApp.Infrastructure.Data;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(LibraryDbContext context, ILogger logger)
    {
        try
        {
            var canConnect = await context.Database.CanConnectAsync();
            logger.LogInformation("Database connection check: {CanConnect}", canConnect);

            if (!canConnect)
            {
                logger.LogInformation("Database does not exist. Creating database...");
                // Use MigrateAsync instead of EnsureCreated to avoid conflicts
                await context.Database.MigrateAsync();
                logger.LogInformation("Database created successfully.");
            }
            else
            {
                // Check if migrations table exists and has records
                var pendingMigrations = await context.Database.GetPendingMigrationsAsync();
                var appliedMigrations = await context.Database.GetAppliedMigrationsAsync();
                
                logger.LogInformation("Pending migrations: {PendingCount}, Applied migrations: {AppliedCount}", 
                    pendingMigrations.Count(), appliedMigrations.Count());

                if (pendingMigrations.Any())
                {
                    logger.LogInformation("Applying pending migrations...");
                    await context.Database.MigrateAsync();
                    logger.LogInformation("Migrations applied successfully.");
                }
                else if (!appliedMigrations.Any())
                {
                    // Database exists but no migrations recorded - this indicates a schema mismatch
                    logger.LogWarning("Database exists but no migrations are recorded. This indicates a schema mismatch.");
                    logger.LogInformation("Performing automatic database reset to resolve schema conflicts...");
                    
                    try
                    {
                        await ForceResetDatabaseAsync(context, logger);
                        logger.LogInformation("Database reset and reinitialization completed successfully.");
                    }
                    catch (Exception resetEx)
                    {
                        logger.LogError(resetEx, "Failed to reset database automatically.");
                        logger.LogWarning("Please use manual database reset functionality.");
                        throw;
                    }
                }
                else
                {
                    logger.LogInformation("Database is up to date with all migrations.");
                }
            }

            await DatabaseSeeder.SeedAsync(context);
            logger.LogInformation("Database seeding completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during database initialization");
            throw;
        }
    }

    public static async Task ForceResetDatabaseAsync(LibraryDbContext context, ILogger logger)
    {
        try
        {
            logger.LogWarning("Force resetting database due to schema conflicts...");
            
            // Drop the database completely
            await context.Database.EnsureDeletedAsync();
            logger.LogInformation("Database dropped successfully.");
            
            // Create the database and apply migrations (this will create tables)
            await context.Database.MigrateAsync();
            logger.LogInformation("Database created and migrations applied successfully.");
            
            // Seed the database
            await DatabaseSeeder.SeedAsync(context);
            logger.LogInformation("Database seeding completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during force database reset");
            throw;
        }
    }


    public static async Task ResetDatabaseAsync(LibraryDbContext context, ILogger logger)
    {
        try
        {
            logger.LogWarning("Resetting database...");
            
            // Drop the database completely
            await context.Database.EnsureDeletedAsync();
            logger.LogInformation("Database dropped successfully.");
            
            // Create the database and apply migrations
            await context.Database.MigrateAsync();
            logger.LogInformation("Database created and migrations applied successfully.");
            
            // Seed the database
            await DatabaseSeeder.SeedAsync(context);
            logger.LogInformation("Database seeding completed successfully.");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred during database reset");
            throw;
        }
    }
} 