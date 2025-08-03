using Xunit;
using LibraryManagementApp.Domain.Entities;

namespace LibraryManagementApp.Tests;

public class BookTests
{
    [Fact]
    public void Book_ShouldHaveValidProperties()
    {
        // Arrange
        var book = new Book
        {
            Id = 1,
            Title = "Test Book",
            PublicationYear = 2023,
            NumberOfCopies = 5,
            PublisherId = 1
        };

        // Assert
        Assert.Equal(1, book.Id);
        Assert.Equal("Test Book", book.Title);
        Assert.Equal(2023, book.PublicationYear);
        Assert.Equal(5, book.NumberOfCopies);
        Assert.Equal(1, book.PublisherId);
        Assert.NotNull(book.Authors);
        Assert.NotNull(book.Categories);
    }

    [Fact]
    public void Book_ShouldInitializeCollections()
    {
        // Arrange
        var book = new Book();

        // Assert
        Assert.NotNull(book.Authors);
        Assert.NotNull(book.Categories);
        Assert.Empty(book.Authors);
        Assert.Empty(book.Categories);
    }
} 