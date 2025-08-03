using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using LibraryManagementApp.Infrastructure.Data;

namespace LibraryManagementApp.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DatabaseController : ControllerBase
{
    private readonly LibraryDbContext _context;
    private readonly ILogger<DatabaseController> _logger;

    public DatabaseController(LibraryDbContext context, ILogger<DatabaseController> logger)
    {
        _context = context;
        _logger = logger;
    }

            [HttpPost("reset")]
        public async Task<IActionResult> ResetDatabase()
        {
            try
            {
                _logger.LogWarning("Database reset requested via API");
                await DatabaseInitializer.ResetDatabaseAsync(_context, _logger);
                return Ok(new { message = "Database reset successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error resetting database");
                return StatusCode(500, new { error = "Failed to reset database", details = ex.Message });
            }
        }

        [HttpPost("force-reset")]
        public async Task<IActionResult> ForceResetDatabase()
        {
            try
            {
                _logger.LogWarning("Force database reset requested via API");
                await DatabaseInitializer.ForceResetDatabaseAsync(_context, _logger);
                return Ok(new { message = "Database force reset successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error force resetting database");
                return StatusCode(500, new { error = "Failed to force reset database", details = ex.Message });
            }
        }

    [HttpGet("status")]
    public async Task<IActionResult> GetDatabaseStatus()
    {
        try
        {
            var canConnect = await _context.Database.CanConnectAsync();
            var pendingMigrations = await _context.Database.GetPendingMigrationsAsync();
            var appliedMigrations = await _context.Database.GetAppliedMigrationsAsync();

            return Ok(new
            {
                canConnect,
                pendingMigrations = pendingMigrations.ToArray(),
                appliedMigrations = appliedMigrations.ToArray(),
                totalAppliedMigrations = appliedMigrations.Count()
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting database status");
            return StatusCode(500, new { error = "Failed to get database status", details = ex.Message });
        }
    }
} 