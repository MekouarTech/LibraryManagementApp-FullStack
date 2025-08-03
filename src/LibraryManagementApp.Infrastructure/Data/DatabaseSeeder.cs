using Microsoft.EntityFrameworkCore;
using LibraryManagementApp.Domain.Entities;

namespace LibraryManagementApp.Infrastructure.Data;

public static class DatabaseSeeder
{
    public static async Task SeedAsync(LibraryDbContext context)
    {
        // Check if relationships already exist to make seeding idempotent
        if (await context.Books.Include(b => b.Authors).Include(b => b.Categories).AnyAsync(b => b.Authors.Any() || b.Categories.Any()))
        {
            return;
        }

        // Get the seeded entities
        var books = await context.Books.ToListAsync();
        var authors = await context.Authors.ToListAsync();
        var categories = await context.Categories.ToListAsync();

        // Create Book-Author relationships
        var bookAuthorRelationships = new[]
        {
            // Book 1
            new { Book = books.First(b => b.Id == 1), Author = authors.First(a => a.Id == 1) },
            new { Book = books.First(b => b.Id == 1), Author = authors.First(a => a.Id == 3) },
            
            // Book 2
            new { Book = books.First(b => b.Id == 2), Author = authors.First(a => a.Id == 2) },
            
            // Book 3
            new { Book = books.First(b => b.Id == 3), Author = authors.First(a => a.Id == 1) },
            new { Book = books.First(b => b.Id == 3), Author = authors.First(a => a.Id == 3) },
            
            // Book 4
            new { Book = books.First(b => b.Id == 4), Author = authors.First(a => a.Id == 4) },
            
            // Book 5
            new { Book = books.First(b => b.Id == 5), Author = authors.First(a => a.Id == 5) },
            new { Book = books.First(b => b.Id == 5), Author = authors.First(a => a.Id == 2) },
            
            // Book 6
            new { Book = books.First(b => b.Id == 6), Author = authors.First(a => a.Id == 3) },
            new { Book = books.First(b => b.Id == 6), Author = authors.First(a => a.Id == 4) }
        };

        // Create Book-Category relationships
        var bookCategoryRelationships = new[]
        {
            new { Book = books.First(b => b.Id == 1), Category = categories.First(c => c.Id == 4) },
            new { Book = books.First(b => b.Id == 1), Category = categories.First(c => c.Id == 2) },
            
            // Book 2
            new { Book = books.First(b => b.Id == 2), Category = categories.First(c => c.Id == 3) },
            
            // Book 3
            new { Book = books.First(b => b.Id == 3), Category = categories.First(c => c.Id == 4) },
            new { Book = books.First(b => b.Id == 3), Category = categories.First(c => c.Id == 2) },
            
            // Book 4
            new { Book = books.First(b => b.Id == 4), Category = categories.First(c => c.Id == 1) },
            
            // Book 5
            new { Book = books.First(b => b.Id == 5), Category = categories.First(c => c.Id == 2) },
            new { Book = books.First(b => b.Id == 5), Category = categories.First(c => c.Id == 3) },
            
            // Book 6
            new { Book = books.First(b => b.Id == 6), Category = categories.First(c => c.Id == 4) },
            new { Book = books.First(b => b.Id == 6), Category = categories.First(c => c.Id == 2) }
        };

        // Add relationships to books
        foreach (var relationship in bookAuthorRelationships)
        {
            relationship.Book.Authors.Add(relationship.Author);
        }

        foreach (var relationship in bookCategoryRelationships)
        {
            relationship.Book.Categories.Add(relationship.Category);
        }

        // Save changes
        await context.SaveChangesAsync();
    }
} 