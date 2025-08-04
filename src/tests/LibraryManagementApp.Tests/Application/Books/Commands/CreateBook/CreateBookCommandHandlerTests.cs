using MediatR;
using LibraryManagementApp.Application.Books.Commands.CreateBook;
using LibraryManagementApp.Application.DTOs;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Repositories;
using LibraryManagementApp.Domain.Events;
using FluentAssertions;
using Moq;

namespace LibraryManagementApp.Tests.Application.Books.Commands.CreateBook;

public class CreateBookCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly Mock<IPublisher> _mockMediator;
    private readonly CreateBookCommandHandler _handler;

    public CreateBookCommandHandlerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _mockMediator = new Mock<IPublisher>();
        _handler = new CreateBookCommandHandler(_mockUnitOfWork.Object, _mockMediator.Object);
    }

    [Fact]
    public async Task Handle_ShouldCreateBookSuccessfully_WhenValidCommandIsProvided()
    {
        // Arrange
        var command = new CreateBookCommand
        {
            Title = "Test Book",
            PublicationYear = 2023,
            NumberOfCopies = 5,
            PublisherId = 1,
            AuthorIds = new List<int> { 1, 2 },
            CategoryIds = new List<int> { 1, 3 }
        };

        var author1 = new Author { Id = 1, FirstName = "John", LastName = "Doe" };
        var author2 = new Author { Id = 2, FirstName = "Jane", LastName = "Smith" };
        var category1 = new Category { Id = 1, Name = "Fiction" };
        var category3 = new Category { Id = 3, Name = "Science Fiction" };
        var publisher = new Publisher { Id = 1, Name = "Test Publisher" };

        var createdBook = new Book
        {
            Id = 1,
            Title = command.Title,
            PublicationYear = command.PublicationYear,
            NumberOfCopies = command.NumberOfCopies,
            PublisherId = command.PublisherId,
            Publisher = publisher,
            Authors = new List<Author> { author1, author2 },
            Categories = new List<Category> { category1, category3 }
        };

        _mockUnitOfWork.Setup(x => x.Authors.GetByIdAsync(1)).ReturnsAsync(author1);
        _mockUnitOfWork.Setup(x => x.Authors.GetByIdAsync(2)).ReturnsAsync(author2);
        _mockUnitOfWork.Setup(x => x.Categories.GetByIdAsync(1)).ReturnsAsync(category1);
        _mockUnitOfWork.Setup(x => x.Categories.GetByIdAsync(3)).ReturnsAsync(category3);
        _mockUnitOfWork.Setup(x => x.Books.AddAsync(It.IsAny<Book>())).ReturnsAsync(createdBook);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
        _mockMediator.Setup(x => x.Publish(It.IsAny<BookCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Title.Should().Be("Test Book");
        result.PublicationYear.Should().Be(2023);
        result.NumberOfCopies.Should().Be(5);
        result.PublisherId.Should().Be(1);
        result.PublisherName.Should().Be("Test Publisher");
        result.Authors.Should().HaveCount(2);
        result.Categories.Should().HaveCount(2);

        _mockUnitOfWork.Verify(x => x.Books.AddAsync(It.IsAny<Book>()), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        _mockMediator.Verify(x => x.Publish(It.IsAny<BookCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldCreateBookWithoutAuthors_WhenNoAuthorIdsAreProvided()
    {
        // Arrange
        var command = new CreateBookCommand
        {
            Title = "Test Book",
            PublicationYear = 2023,
            NumberOfCopies = 5,
            PublisherId = 1,
            AuthorIds = new List<int>(),
            CategoryIds = new List<int> { 1 }
        };

        var category1 = new Category { Id = 1, Name = "Fiction" };
        var publisher = new Publisher { Id = 1, Name = "Test Publisher" };

        var createdBook = new Book
        {
            Id = 1,
            Title = command.Title,
            PublicationYear = command.PublicationYear,
            NumberOfCopies = command.NumberOfCopies,
            PublisherId = command.PublisherId,
            Publisher = publisher,
            Authors = new List<Author>(),
            Categories = new List<Category> { category1 }
        };

        _mockUnitOfWork.Setup(x => x.Categories.GetByIdAsync(1)).ReturnsAsync(category1);
        _mockUnitOfWork.Setup(x => x.Books.AddAsync(It.IsAny<Book>())).ReturnsAsync(createdBook);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
        _mockMediator.Setup(x => x.Publish(It.IsAny<BookCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Authors.Should().BeEmpty();
        result.Categories.Should().HaveCount(1);

        _mockUnitOfWork.Verify(x => x.Authors.GetByIdAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldCreateBookWithoutCategories_WhenNoCategoryIdsAreProvided()
    {
        // Arrange
        var command = new CreateBookCommand
        {
            Title = "Test Book",
            PublicationYear = 2023,
            NumberOfCopies = 5,
            PublisherId = 1,
            AuthorIds = new List<int> { 1 },
            CategoryIds = new List<int>()
        };

        var author1 = new Author { Id = 1, FirstName = "John", LastName = "Doe" };
        var publisher = new Publisher { Id = 1, Name = "Test Publisher" };

        var createdBook = new Book
        {
            Id = 1,
            Title = command.Title,
            PublicationYear = command.PublicationYear,
            NumberOfCopies = command.NumberOfCopies,
            PublisherId = command.PublisherId,
            Publisher = publisher,
            Authors = new List<Author> { author1 },
            Categories = new List<Category>()
        };

        _mockUnitOfWork.Setup(x => x.Authors.GetByIdAsync(1)).ReturnsAsync(author1);
        _mockUnitOfWork.Setup(x => x.Books.AddAsync(It.IsAny<Book>())).ReturnsAsync(createdBook);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
        _mockMediator.Setup(x => x.Publish(It.IsAny<BookCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Authors.Should().HaveCount(1);
        result.Categories.Should().BeEmpty();

        _mockUnitOfWork.Verify(x => x.Categories.GetByIdAsync(It.IsAny<int>()), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldSkipNonExistentAuthors_WhenAuthorIdsDoNotExist()
    {
        // Arrange
        var command = new CreateBookCommand
        {
            Title = "Test Book",
            PublicationYear = 2023,
            NumberOfCopies = 5,
            PublisherId = 1,
            AuthorIds = new List<int> { 1, 999 },
            CategoryIds = new List<int> { 1 }
        };

        var author1 = new Author { Id = 1, FirstName = "John", LastName = "Doe" };
        var category1 = new Category { Id = 1, Name = "Fiction" };
        var publisher = new Publisher { Id = 1, Name = "Test Publisher" };

        var createdBook = new Book
        {
            Id = 1,
            Title = command.Title,
            PublicationYear = command.PublicationYear,
            NumberOfCopies = command.NumberOfCopies,
            PublisherId = command.PublisherId,
            Publisher = publisher,
            Authors = new List<Author> { author1 },
            Categories = new List<Category> { category1 }
        };

        _mockUnitOfWork.Setup(x => x.Authors.GetByIdAsync(1)).ReturnsAsync(author1);
        _mockUnitOfWork.Setup(x => x.Authors.GetByIdAsync(999)).ReturnsAsync((Author?)null);
        _mockUnitOfWork.Setup(x => x.Categories.GetByIdAsync(1)).ReturnsAsync(category1);
        _mockUnitOfWork.Setup(x => x.Books.AddAsync(It.IsAny<Book>())).ReturnsAsync(createdBook);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
        _mockMediator.Setup(x => x.Publish(It.IsAny<BookCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Authors.Should().HaveCount(1);
        result.Authors.First().Id.Should().Be(1);

        _mockUnitOfWork.Verify(x => x.Authors.GetByIdAsync(1), Times.Once);
        _mockUnitOfWork.Verify(x => x.Authors.GetByIdAsync(999), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldSkipNonExistentCategories_WhenCategoryIdsDoNotExist()
    {
        // Arrange
        var command = new CreateBookCommand
        {
            Title = "Test Book",
            PublicationYear = 2023,
            NumberOfCopies = 5,
            PublisherId = 1,
            AuthorIds = new List<int> { 1 },
            CategoryIds = new List<int> { 1, 999 }
        };

        var author1 = new Author { Id = 1, FirstName = "John", LastName = "Doe" };
        var category1 = new Category { Id = 1, Name = "Fiction" };
        var publisher = new Publisher { Id = 1, Name = "Test Publisher" };

        var createdBook = new Book
        {
            Id = 1,
            Title = command.Title,
            PublicationYear = command.PublicationYear,
            NumberOfCopies = command.NumberOfCopies,
            PublisherId = command.PublisherId,
            Publisher = publisher,
            Authors = new List<Author> { author1 },
            Categories = new List<Category> { category1 }
        };

        _mockUnitOfWork.Setup(x => x.Authors.GetByIdAsync(1)).ReturnsAsync(author1);
        _mockUnitOfWork.Setup(x => x.Categories.GetByIdAsync(1)).ReturnsAsync(category1);
        _mockUnitOfWork.Setup(x => x.Categories.GetByIdAsync(999)).ReturnsAsync((Category?)null);
        _mockUnitOfWork.Setup(x => x.Books.AddAsync(It.IsAny<Book>())).ReturnsAsync(createdBook);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
        _mockMediator.Setup(x => x.Publish(It.IsAny<BookCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Categories.Should().HaveCount(1);
        result.Categories.First().Id.Should().Be(1);

        _mockUnitOfWork.Verify(x => x.Categories.GetByIdAsync(1), Times.Once);
        _mockUnitOfWork.Verify(x => x.Categories.GetByIdAsync(999), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldPublishBookCreatedEvent_WhenBookIsSuccessfullyCreated()
    {
        // Arrange
        var command = new CreateBookCommand
        {
            Title = "Test Book",
            PublicationYear = 2023,
            NumberOfCopies = 5,
            PublisherId = 1,
            AuthorIds = new List<int>(),
            CategoryIds = new List<int>()
        };

        var createdBook = new Book
        {
            Id = 1,
            Title = command.Title,
            PublicationYear = command.PublicationYear,
            NumberOfCopies = command.NumberOfCopies,
            PublisherId = command.PublisherId
        };

        _mockUnitOfWork.Setup(x => x.Books.AddAsync(It.IsAny<Book>())).ReturnsAsync(createdBook);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);
        _mockMediator.Setup(x => x.Publish(It.IsAny<BookCreatedEvent>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        // Act
        await _handler.Handle(command, CancellationToken.None);

        // Assert
        _mockMediator.Verify(x => x.Publish(It.Is<BookCreatedEvent>(e => e.Book == createdBook), It.IsAny<CancellationToken>()), Times.Once);
    }
} 