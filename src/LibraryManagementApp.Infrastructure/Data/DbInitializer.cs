using Microsoft.EntityFrameworkCore;
using LibraryManagementApp.Domain.Entities;

namespace LibraryManagementApp.Infrastructure.Data;

public static class DbInitializer
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        // Seed Publishers
        modelBuilder.Entity<Publisher>().HasData(
            new Publisher { Id = 1, Name = "Publisher 1" },
            new Publisher { Id = 2, Name = "Publisher 2" },
            new Publisher { Id = 3, Name = "Publisher 3" }
        );

        // Seed Categories
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Fiction" },
            new Category { Id = 2, Name = "Science" },
            new Category { Id = 3, Name = "History" },
            new Category { Id = 4, Name = "Technology" }
        );

        // Seed Authors
        modelBuilder.Entity<Author>().HasData(
            new Author 
            { 
                Id = 1, 
                FirstName = "AutherName1", 
                LastName = "AutherLastName1", 
                Biography = "AutherBiography1", 
                DateOfBirth = new DateTime(1985, 3, 15) 
            },
            new Author 
            { 
                Id = 2, 
                FirstName = "AutherName2", 
                LastName = "AutherLastName2", 
                Biography = "AutherBiography2", 
                DateOfBirth = new DateTime(1978, 7, 22) 
            },
            new Author 
            { 
                Id = 3, 
                FirstName = "AutherName3", 
                LastName = "AutherLastName3", 
                Biography = "AutherBiography3", 
                DateOfBirth = new DateTime(1982, 11, 8) 
            },
            new Author 
            { 
                Id = 4, 
                FirstName = "AutherName4", 
                LastName = "AutherLastName4", 
                Biography = "AutherBiography4", 
                DateOfBirth = new DateTime(1990, 4, 12) 
            },
            new Author 
            { 
                Id = 5, 
                FirstName = "AutherName5", 
                LastName = "AutherLastName5", 
                Biography = "AutherBiography5", 
                DateOfBirth = new DateTime(1975, 9, 30) 
            }
        );

        // Seed Books
        modelBuilder.Entity<Book>().HasData(
            new Book 
            { 
                Id = 1, 
                Title = "BookTitle1", 
                PublicationYear = 2022, 
                NumberOfCopies = 15, 
                PublisherId = 1 
            },
            new Book 
            { 
                Id = 2, 
                Title = "BookTitle2", 
                PublicationYear = 2021, 
                NumberOfCopies = 12, 
                PublisherId = 2 
            },
            new Book 
            { 
                Id = 3, 
                Title = "BookTitle3", 
                PublicationYear = 2023, 
                NumberOfCopies = 8, 
                PublisherId = 3 
            },
            new Book 
            { 
                Id = 4, 
                Title = "BookTitle4", 
                PublicationYear = 2020, 
                NumberOfCopies = 20, 
                PublisherId = 1 
            },
            new Book 
            { 
                Id = 5, 
                Title = "BookTitle5", 
                PublicationYear = 2022, 
                NumberOfCopies = 10, 
                PublisherId = 2 
            },
            new Book 
            { 
                Id = 6, 
                Title = "BookTitle6", 
                PublicationYear = 2021, 
                NumberOfCopies = 18, 
                PublisherId = 3 
            }
        );


    }
} 