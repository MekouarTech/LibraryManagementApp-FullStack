using MediatR;
using LibraryManagementApp.Application.Books.Commands.DeleteBook;
using LibraryManagementApp.Domain.Entities;
using LibraryManagementApp.Domain.Repositories;
using FluentAssertions;
using Moq;

namespace LibraryManagementApp.Tests.Application.Books.Commands.DeleteBook;

public class DeleteBookCommandHandlerTests
{
    private readonly Mock<IUnitOfWork> _mockUnitOfWork;
    private readonly DeleteBookCommandHandler _handler;

    public DeleteBookCommandHandlerTests()
    {
        _mockUnitOfWork = new Mock<IUnitOfWork>();
        _handler = new DeleteBookCommandHandler(_mockUnitOfWork.Object);
    }

    [Fact]
    public async Task Handle_ShouldDeleteBookSuccessfully_WhenBookExists()
    {
        // Arrange
        var bookId = 1;
        var command = new DeleteBookCommand { Id = bookId };
        var existingBook = new Book
        {
            Id = bookId,
            Title = "Test Book",
            PublicationYear = 2023,
            NumberOfCopies = 5,
            PublisherId = 1
        };

        _mockUnitOfWork.Setup(x => x.Books.GetByIdAsync(bookId)).ReturnsAsync(existingBook);
        _mockUnitOfWork.Setup(x => x.Books.DeleteAsync(bookId)).Returns(Task.CompletedTask);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();

        _mockUnitOfWork.Verify(x => x.Books.GetByIdAsync(bookId), Times.Once);
        _mockUnitOfWork.Verify(x => x.Books.DeleteAsync(bookId), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Fact]
    public async Task Handle_ShouldReturnFalse_WhenBookDoesNotExist()
    {
        // Arrange
        var bookId = 999;
        var command = new DeleteBookCommand { Id = bookId };

        _mockUnitOfWork.Setup(x => x.Books.GetByIdAsync(bookId)).ReturnsAsync((Book?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();

        _mockUnitOfWork.Verify(x => x.Books.GetByIdAsync(bookId), Times.Once);
        _mockUnitOfWork.Verify(x => x.Books.DeleteAsync(It.IsAny<int>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldDeleteBookWithRelationships_WhenBookHasAuthorsAndCategories()
    {
        // Arrange
        var bookId = 1;
        var command = new DeleteBookCommand { Id = bookId };
        var existingBook = new Book
        {
            Id = bookId,
            Title = "Test Book",
            PublicationYear = 2023,
            NumberOfCopies = 5,
            PublisherId = 1,
            Authors = new List<Author>
            {
                new Author { Id = 1, FirstName = "John", LastName = "Doe" }
            },
            Categories = new List<Category>
            {
                new Category { Id = 1, Name = "Fiction" }
            }
        };

        _mockUnitOfWork.Setup(x => x.Books.GetByIdAsync(bookId)).ReturnsAsync(existingBook);
        _mockUnitOfWork.Setup(x => x.Books.DeleteAsync(bookId)).Returns(Task.CompletedTask);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();

        _mockUnitOfWork.Verify(x => x.Books.GetByIdAsync(bookId), Times.Once);
        _mockUnitOfWork.Verify(x => x.Books.DeleteAsync(bookId), Times.Once);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    [InlineData(int.MinValue)]
    public async Task Handle_ShouldReturnFalse_WhenInvalidBookIdIsProvided(int invalidId)
    {
        // Arrange
        var command = new DeleteBookCommand { Id = invalidId };

        _mockUnitOfWork.Setup(x => x.Books.GetByIdAsync(invalidId)).ReturnsAsync((Book?)null);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeFalse();

        _mockUnitOfWork.Verify(x => x.Books.GetByIdAsync(invalidId), Times.Once);
        _mockUnitOfWork.Verify(x => x.Books.DeleteAsync(It.IsAny<int>()), Times.Never);
        _mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Never);
    }

    [Fact]
    public async Task Handle_ShouldReturnTrue_WhenBookExistsAndDeleteOperationSucceeds()
    {
        // Arrange
        var bookId = 1;
        var command = new DeleteBookCommand { Id = bookId };
        var existingBook = new Book
        {
            Id = bookId,
            Title = "Test Book",
            PublicationYear = 2023,
            NumberOfCopies = 5,
            PublisherId = 1
        };

        _mockUnitOfWork.Setup(x => x.Books.GetByIdAsync(bookId)).ReturnsAsync(existingBook);
        _mockUnitOfWork.Setup(x => x.Books.DeleteAsync(bookId)).Returns(Task.CompletedTask);
        _mockUnitOfWork.Setup(x => x.SaveChangesAsync()).ReturnsAsync(1);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().BeTrue();
    }
} 