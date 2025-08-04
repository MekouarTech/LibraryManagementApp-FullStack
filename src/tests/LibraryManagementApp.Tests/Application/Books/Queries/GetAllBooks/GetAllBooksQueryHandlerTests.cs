using MediatR;
using LibraryManagementApp.Application.Books.Queries.GetAllBooks;
using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace LibraryManagementApp.Tests.Application.Books.Queries.GetAllBooks;

public class GetAllBooksQueryHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly GetAllBooksQueryHandler _handler;

    public GetAllBooksQueryHandlerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new GetAllBooksQueryHandler(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnAllBooks_WhenBooksExist()
    {
        // Arrange
        var books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Book 1",
                PublicationYear = 2023,
                NumberOfCopies = 5,
                PublisherId = 1,
                Publisher = new Publisher { Id = 1, Name = "Publisher 1" },
                Authors = new List<Author>
                {
                    new Author { Id = 1, FirstName = "John", LastName = "Doe" }
                },
                Categories = new List<Category>
                {
                    new Category { Id = 1, Name = "Fiction" }
                }
            },
            new Book
            {
                Id = 2,
                Title = "Book 2",
                PublicationYear = 2022,
                NumberOfCopies = 3,
                PublisherId = 2,
                Publisher = new Publisher { Id = 2, Name = "Publisher 2" },
                Authors = new List<Author>(),
                Categories = new List<Category>()
            }
        };

        var query = new GetAllBooksQuery();

        _mockUnitOfWork.Setup(x => x.Books.GetAllAsync()).ReturnsAsync(books);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);

        var firstBook = result.First();
        firstBook.Id.Should().Be(1);
        firstBook.Title.Should().Be("Book 1");
        firstBook.PublicationYear.Should().Be(2023);
        firstBook.NumberOfCopies.Should().Be(5);
        firstBook.PublisherId.Should().Be(1);
        firstBook.PublisherName.Should().Be("Publisher 1");
        firstBook.Authors.Should().HaveCount(1);
        firstBook.Categories.Should().HaveCount(1);

        var secondBook = result.Last();
        secondBook.Id.Should().Be(2);
        secondBook.Title.Should().Be("Book 2");
        secondBook.PublicationYear.Should().Be(2022);
        secondBook.NumberOfCopies.Should().Be(3);
        secondBook.PublisherId.Should().Be(2);
        secondBook.PublisherName.Should().Be("Publisher 2");
        secondBook.Authors.Should().BeEmpty();
        secondBook.Categories.Should().BeEmpty();

        _mockUnitOfWork.Verify(x => x.Books.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnEmptyList_WhenNoBooksExist()
    {
        // Arrange
        var books = new List<Book>();
        var query = new GetAllBooksQuery();

        _mockUnitOfWork.Setup(x => x.Books.GetAllAsync()).ReturnsAsync(books);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();

        _mockUnitOfWork.Verify(x => x.Books.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnBookWithNullPublisher_WhenBookHasNoPublisher()
    {
        // Arrange
        var books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Book 1",
                PublicationYear = 2023,
                NumberOfCopies = 5,
                PublisherId = 1,
                Publisher = null!,
                Authors = new List<Author>(),
                Categories = new List<Category>()
            }
        };

        var query = new GetAllBooksQuery();

        _mockUnitOfWork.Setup(x => x.Books.GetAllAsync()).ReturnsAsync(books);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);

        var book = result.First();
        book.PublisherName.Should().BeEmpty();

        _mockUnitOfWork.Verify(x => x.Books.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnBookWithMultipleAuthors_WhenBookHasMultipleAuthors()
    {
        // Arrange
        var books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Book 1",
                PublicationYear = 2023,
                NumberOfCopies = 5,
                PublisherId = 1,
                Publisher = new Publisher { Id = 1, Name = "Publisher 1" },
                Authors = new List<Author>
                {
                    new Author { Id = 1, FirstName = "John", LastName = "Doe", Biography = "Bio 1", DateOfBirth = new DateTime(1980, 1, 1) },
                    new Author { Id = 2, FirstName = "Jane", LastName = "Smith", Biography = "Bio 2", DateOfBirth = new DateTime(1985, 2, 2) }
                },
                Categories = new List<Category>()
            }
        };

        var query = new GetAllBooksQuery();

        _mockUnitOfWork.Setup(x => x.Books.GetAllAsync()).ReturnsAsync(books);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);

        var book = result.First();
        book.Authors.Should().HaveCount(2);
        book.Authors.First().Id.Should().Be(1);
        book.Authors.First().FirstName.Should().Be("John");
        book.Authors.First().LastName.Should().Be("Doe");
        book.Authors.First().Biography.Should().Be("Bio 1");
        book.Authors.First().DateOfBirth.Should().Be(new DateTime(1980, 1, 1));

        book.Authors.Last().Id.Should().Be(2);
        book.Authors.Last().FirstName.Should().Be("Jane");
        book.Authors.Last().LastName.Should().Be("Smith");

        _mockUnitOfWork.Verify(x => x.Books.GetAllAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnBookWithMultipleCategories_WhenBookHasMultipleCategories()
    {
        // Arrange
        var books = new List<Book>
        {
            new Book
            {
                Id = 1,
                Title = "Book 1",
                PublicationYear = 2023,
                NumberOfCopies = 5,
                PublisherId = 1,
                Publisher = new Publisher { Id = 1, Name = "Publisher 1" },
                Authors = new List<Author>(),
                Categories = new List<Category>
                {
                    new Category { Id = 1, Name = "Fiction" },
                    new Category { Id = 2, Name = "Science Fiction" },
                    new Category { Id = 3, Name = "Adventure" }
                }
            }
        };

        var query = new GetAllBooksQuery();

        _mockUnitOfWork.Setup(x => x.Books.GetAllAsync()).ReturnsAsync(books);

        // Act
        var result = await _handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);

        var book = result.First();
        book.Categories.Should().HaveCount(3);
        book.Categories.First().Id.Should().Be(1);
        book.Categories.First().Name.Should().Be("Fiction");
        book.Categories.Last().Id.Should().Be(3);
        book.Categories.Last().Name.Should().Be("Adventure");

        _mockUnitOfWork.Verify(x => x.Books.GetAllAsync(), Times.Once);
    }
} 