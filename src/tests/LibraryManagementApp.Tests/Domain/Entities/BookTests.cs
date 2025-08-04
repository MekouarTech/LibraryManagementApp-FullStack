using LibraryManagementApp.Domain.Entities;
using FluentAssertions;

namespace LibraryManagementApp.Tests.Domain.Entities;

public class BookTests
{
    [Fact]
    public void Constructor_ShouldInitializeCollections_WhenNewBookIsCreated()
    {
        // Arrange & Act
        var book = new Book();

        // Assert
        book.Authors.Should().NotBeNull();
        book.Categories.Should().NotBeNull();
        book.Authors.Should().BeEmpty();
        book.Categories.Should().BeEmpty();
    }

    [Fact]
    public void Properties_ShouldBeSetCorrectly_WhenBookIsCreated()
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
        book.Id.Should().Be(1);
        book.Title.Should().Be("Test Book");
        book.PublicationYear.Should().Be(2023);
        book.NumberOfCopies.Should().Be(5);
        book.PublisherId.Should().Be(1);
    }

    [Fact]
    public void Authors_ShouldBeAddedCorrectly_WhenAuthorIsAddedToBook()
    {
        // Arrange
        var book = new Book();
        var author = new Author { Id = 1, FirstName = "John", LastName = "Doe" };

        // Act
        book.Authors.Add(author);

        // Assert
        book.Authors.Should().HaveCount(1);
        book.Authors.First().Should().Be(author);
    }

    [Fact]
    public void Categories_ShouldBeAddedCorrectly_WhenCategoryIsAddedToBook()
    {
        // Arrange
        var book = new Book();
        var category = new Category { Id = 1, Name = "Fiction" };

        // Act
        book.Categories.Add(category);

        // Assert
        book.Categories.Should().HaveCount(1);
        book.Categories.First().Should().Be(category);
    }

    [Fact]
    public void MultipleAuthors_ShouldBeAddedCorrectly_WhenMultipleAuthorsAreAddedToBook()
    {
        // Arrange
        var book = new Book();
        var author1 = new Author { Id = 1, FirstName = "John", LastName = "Doe" };
        var author2 = new Author { Id = 2, FirstName = "Jane", LastName = "Smith" };

        // Act
        book.Authors.Add(author1);
        book.Authors.Add(author2);

        // Assert
        book.Authors.Should().HaveCount(2);
        book.Authors.Should().Contain(author1);
        book.Authors.Should().Contain(author2);
    }

    [Fact]
    public void MultipleCategories_ShouldBeAddedCorrectly_WhenMultipleCategoriesAreAddedToBook()
    {
        // Arrange
        var book = new Book();
        var category1 = new Category { Id = 1, Name = "Fiction" };
        var category2 = new Category { Id = 2, Name = "Science Fiction" };

        // Act
        book.Categories.Add(category1);
        book.Categories.Add(category2);

        // Assert
        book.Categories.Should().HaveCount(2);
        book.Categories.Should().Contain(category1);
        book.Categories.Should().Contain(category2);
    }

    [Fact]
    public void Publisher_ShouldBeSetCorrectly_WhenPublisherIsAssignedToBook()
    {
        // Arrange
        var book = new Book();
        var publisher = new Publisher { Id = 1, Name = "Test Publisher" };

        // Act
        book.Publisher = publisher;
        book.PublisherId = publisher.Id;

        // Assert
        book.Publisher.Should().Be(publisher);
        book.PublisherId.Should().Be(publisher.Id);
    }
} 